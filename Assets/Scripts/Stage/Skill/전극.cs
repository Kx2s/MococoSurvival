using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 전극 : Skill_Active
{
    Collider2D coll;

    WaitForSeconds on = new WaitForSeconds(0.1f);
    WaitForSeconds off = new WaitForSeconds(1f);

    private void Awake()
    {
        skill = Skill.GetList()[13];
        coll = GetComponentInChildren<Collider2D>();
    }
    public override void init()
    {

    }

    public override void func()
    {
        StartCoroutine(coroutine());
    }

    IEnumerator coroutine()
    {
        while (true)
        {
            Transform child = transform.GetChild(0);
            child.parent = null;
            child.localScale = Vector3.one * (100 + ((level + GameManager.instance.Range) * 20))/100;
            child.parent = transform;


            coll.enabled = true;
            yield return off;
            coll.enabled = false;
            yield return on;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
            return;

        collision.GetComponent<Enemy>().OnDamage(skill.sk_bagic + skill.sk_increase * (level-1));
    }
}
