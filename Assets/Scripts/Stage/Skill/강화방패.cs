using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 강화방패 : Skill_Active
{
    private void Awake()
    {
        skill = Skill.GetList()[10];
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
            float total = GameManager.instance.baseHealth * (skill.sk_bagic + skill.sk_increase * (level-1)) / 100;
            GameManager.instance.ShieldReset(total);
            print("shield reset");

            yield return GameManager.instance.Shield < total;
            yield return new WaitForSeconds(coolTime());
        }
    }
}
