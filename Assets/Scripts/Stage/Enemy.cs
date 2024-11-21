using DataTable;
using EnumManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool isLive = false;
    Animator anim;
    Rigidbody2D target;
    Rigidbody2D rigid;
    Collider2D coll;
    ShowDamage show;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    float chageTime;
    float strongRate;

    [Header("# Enemy Info")]
    public float attack;
    public float speed;
    public float health;
    public float maxHealth;
    public float exp;

    public RuntimeAnimatorController[] animCon;

    void Awake()
    {
        chageTime = GameManager.instance.maxGameTime / 3;

        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        show = GetComponentInChildren<ShowDamage>();
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        rigid.simulated = true;
        coll.enabled = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;

        Init();
    }
    
    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    public void Init()
    {
        if (GameManager.instance.gameTime < chageTime)
        {
            anim.runtimeAnimatorController = animCon[GameManager.instance.stage / 3];
        }
        else if (GameManager.instance.gameTime < 2 * chageTime)
        {
            anim.runtimeAnimatorController = animCon[GameManager.instance.stage / 3 + 1];
        }
        else
        {
            anim.runtimeAnimatorController = animCon[GameManager.instance.stage / 3 + 2];
        }

        attack = 5;
        speed = 1.5f;
        maxHealth = 50;
        exp = 1;
        maxHealth *= GameManager.instance.stage * 5 + Mathf.Pow(1.08f, GameManager.instance.gameTime / 10);
        attack *= GameManager.instance.stage * 5 + Mathf.Pow(1.05f, GameManager.instance.gameTime / 10);

        int ran = Random.Range(0, 100);

        Color color = Color.white;
        if (ran < 3)
        {
            ColorUtility.TryParseHtmlString("#444444", out color);
            maxHealth *= 2;
            exp = 2;
        }
        else if (ran < 6)
        {
            ColorUtility.TryParseHtmlString("#6071FF", out color);
            speed *= 1.5f;
            exp = 2;
        }
        else if (ran < 9)
        {
            ColorUtility.TryParseHtmlString("#FF6460", out color);
            attack *= 2;
            exp = 2;
        }
        spriter.color = color;
        health = maxHealth;

        speed -= speed * GameManager.instance.Slow/100;
    }

    public void OnDamage (int damage)
    {
        //µ¥¹ÌÁö Àû¿ë
        int ran = Random.Range(0, 100);
        float cri = ran <= GameManager.instance.Critical ? 1.5f : 1;
        int total = (int)(GameManager.instance.Attack * damage/100 * cri);
        health -= total;
        
        if (total > 999999999)
        {
            cri = 1;
            total = 999999999;
        }
        show.Hit(total, cri != 1);

        StartCoroutine(KnockBack());

        if (health > 0)
        {
            anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(Sfx.Hit);
        }
        else
        {
            isLive = false;
            rigid.simulated = false;
            coll.enabled = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.exp = 1 * (GameManager.instance.ExpBoost+100)/100;

            if (GameManager.instance.isLive)
                AudioManager.instance.PlaySfx(Sfx.Dead);
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);

        int ran = Random.Range(0, 10000);
        GameObject item = null;

        //ÆøÅº
        if (ran < 10)
            item = GameManager.instance.pool.Get(5);

        //Æ÷¼Ç
        else if (ran < 60)
            item = GameManager.instance.pool.Get(6);

        //°ñµå
        else if (ran < 110)
            item = GameManager.instance.pool.Get(7);

        if (item != null)
            item.transform.position = transform.position;
    }
}
