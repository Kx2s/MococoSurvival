using DataTable;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SuicideBomb : MonoBehaviour
{
    bool tf;
    public int level;

    WaitForSeconds wait;
    public Transform target;

    Skill skill;
    Scanner scanner;
    Rigidbody2D rigid;
    SpriteRenderer render;

    private void Awake()
    {
        wait = new WaitForSeconds(1);
        skill = Skill.GetList()[16];
        scanner = GetComponent<Scanner>();
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (target == null || tf)
            return;

        Vector3 targetPos = target.position;
        Vector3 dir = targetPos - transform.position;

        if (Vector2.Distance(targetPos, transform.position) <= 1)
            return;

        if (dir.x > 0)
            render.flipX = false;
        else 
            render.flipX = true;

        rigid.velocity = dir.normalized * 5;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
            return;

        if (!tf)
        {
            tf = true;
            rigid.velocity = Vector2.zero;

            Transform child = transform.GetChild(1);
            child.parent = null;
            child.localScale = Vector3.one * (level + 1);
            child.parent = transform;

            transform.GetChild(0).gameObject.SetActive(false);
            child.gameObject.SetActive(true);
        }
        else
        {
            collision.GetComponent<Enemy>().OnDamage(skill.sk_bagic + skill.sk_increase * (level - 1));
            StartCoroutine(End());
        }
    }

    IEnumerator End()
    {
        yield return wait;

        tf = false;
        gameObject.SetActive(false);
        rigid.simulated = true;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
