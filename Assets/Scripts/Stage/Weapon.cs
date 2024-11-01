using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Player player;
    WaitForSeconds wait;

    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;


    void Awake()
    {
        player = GameManager.instance.player;
    }

    private void Start()
    {
        print(id);
        switch (id)
        {
            case 0:
                StartCoroutine(Shovel());
                break;
            default:
                StartCoroutine(Gun());
                break;
        }
    }

    IEnumerator Shovel()
    {
        WaitForSeconds waitShovel = new WaitForSeconds(.01f);
        while (true)
        {
            transform.Rotate(Vector3.back * speed * Time.deltaTime);
            yield return waitShovel;
        }
    }

    IEnumerator Gun()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(speed);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Attack;
        this.count += count;

        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
        wait = new WaitForSeconds(speed);
    }

    public void Init(ItemData data)
    {
        // 기본
        name = "Weapon " + data.itemName;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // data set
        id = data.itemId;
        damage = data.baseDamage * Character.Attack;
        count = data.baseCount + Character.Count;

        for (int i = 0; i < GameManager.instance.pool.prefabs.Length; i++)
            if (data.projectile == GameManager.instance.pool.prefabs[i])
            {
                prefabId = i;
                break;
            }

        switch (id)
        {
            case 0:
                speed = 150 * Character.WeaponSpeed;
                Batch();
                break;

            default:
                speed = 0.5f * Character.WeaponRate;
                break;
        }
        // Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;
            if (i < transform.childCount)
                bullet = transform.GetChild(i);
            else
                bullet = GameManager.instance.pool.Get(prefabId).transform;

            bullet.parent = transform;
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -100 is Infinity Per
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
