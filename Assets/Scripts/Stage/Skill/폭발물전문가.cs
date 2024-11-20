using DataTable;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class 폭발물전문가 : Skill_Active
{
    private void Awake()
    {
        skill = Skill.GetList()[22];
        level = 5;
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
            Vector3 dir = Random.insideUnitCircle.normalized;
            int total = 5;
            float angle = 360 / total;

            for (int i = 1; i <= total; i++)
            {
                Transform grenade = GameManager.instance.pool.Get(Random.Range(3, 5)).transform;
                grenade.position = transform.position;
                grenade.GetComponent<Grenade>().dir = Quaternion.Euler(0, 0, angle * i) * dir;
                grenade.GetComponent<Grenade>().level = 10;
            }

            yield return new WaitForSeconds(coolTime());
        }
    }
}