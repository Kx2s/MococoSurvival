using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 화염수류탄 : Skill_Active
{
    private void Awake()
    {
        skill = Skill.GetList()[15];
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
            int total = skill.sk_count * level + GameManager.instance.Count;
            float angle = 360 / total;

            for (int i = 1; i <= total; i++)
            {
                Transform grenade = GameManager.instance.pool.Get(4).transform;
                grenade.position = transform.position;
                grenade.GetComponent<Grenade>().dir = Quaternion.Euler(0, 0, angle * i) * dir;
                grenade.GetComponent<Grenade>().level = level;
            }

            yield return new WaitForSeconds(coolTime());
        }
    }
    
}
