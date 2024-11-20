using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 배틀마스터 : Skill_Active
{
    public Scanner scanner;

    private void Awake()
    {
        skill = Skill.GetList()[25];
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
        Vector3 dir;

        while (true)
        {
            Transform target = scanner.nearestTarget;
            if (target != null)
                dir = (target.position - transform.position).normalized;
            else
                dir = Vector3.zero;

            Transform child = transform.GetChild(0);
            child.parent = null;
            child.localScale = Vector2.one * (0.5f + 0.5f * (level + GameManager.instance.Range));
            child.parent = transform;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            child.rotation = Quaternion.Euler(0, 0, angle);

            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            child.gameObject.SetActive(false);

            yield return new WaitForSeconds(coolTime());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
            return;

        collision.GetComponent<Enemy>().OnDamage(skill.sk_bagic + skill.sk_increase * (level-1));
    }
}