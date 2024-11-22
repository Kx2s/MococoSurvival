using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    Collider2D coll;

    public int type;
    public int level;
    public Vector3 dir;
    public Skill skill;

    private void Awake()
    {
        coll = GetComponentInChildren<Collider2D>(true);
    }

    private void OnEnable()
    {
        dir = Vector3.left;

        Transform child = transform.GetChild(1);
        child.parent = null;
        child.localScale = Vector3.one * (1 + 0.5f * (level + GameManager.instance.Range));
        child.parent = transform;
        skill = type==0? Skill.GetList()[14] : Skill.GetList()[15];

        StartCoroutine(move());
    }
    
    IEnumerator move()
    {
        int cnt = 0;
        while (cnt++ < 30)
        {
            transform.position += dir * 0.1f;
            yield return new WaitForEndOfFrame();
        }
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);

        int sec = type == 0 ? 1 : (3 + level)*2;
        for (int i =0; i <sec; i++)
        {
            coll.enabled = true;
            yield return new WaitForSeconds(.1f);
            coll.enabled = false;
            yield return new WaitForSeconds(.3f);
        }

        gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
            return;

        collision.GetComponent<Enemy>().OnDamage(skill.sk_bagic + skill.sk_increase * (level - 1));
    }
}
