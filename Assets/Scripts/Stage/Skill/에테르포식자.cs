using DataTable;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class 에테르포식자 : Skill_Active
{
    private void Awake()
    {
        skill = Skill.GetList()[20];
    }

    public override void init()
    {
    }

    public override void func()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            StartCoroutine(coroutine());
        }

        else if (collision.tag == "Enemy" && transform.GetChild(0).gameObject.activeSelf)
        {
            collision.GetComponent<Enemy>().OnDamage(skill.sk_bagic);
        }

    }

    IEnumerator coroutine()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}