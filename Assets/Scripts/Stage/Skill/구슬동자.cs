using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 구슬동자 : Skill_Active
{
    private void Awake()
    {
        skill = Skill.GetList()[12];
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
            int ran = Random.Range(0, 10);

            GameObject item = null;

            //폭탄
            if (ran < 1)
                item = GameManager.instance.pool.Get(5);

            //회복
            else if (ran < 5)
                item = GameManager.instance.pool.Get(6);

            //골드
            else
                item = GameManager.instance.pool.Get(7);

            item.transform.position = transform.position + dir * 5;

            yield return new WaitForSeconds(coolTime());
        }
    }
}
