using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 바리케이드 : Skill_Active
{
    private void Awake()
    {
        skill = Skill.GetList()[18];
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
            yield return GameManager.instance.Shield > 0;
            GameManager.instance.AttackRateUp(skill.sk_bagic);

            yield return GameManager.instance.Shield == 0;
            GameManager.instance.AttackRateUp(-skill.sk_bagic);
        }
    }
}