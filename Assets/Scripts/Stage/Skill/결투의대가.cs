using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 결투의대가 : Skill_Active
{
    private void Awake()
    {
        skill = Skill.GetList()[11];
    }
    public override void init()
    {
        GameManager.instance.AttackRateUp(skill.sk_increase);
    }

    public override void func()
    {
    }
}
