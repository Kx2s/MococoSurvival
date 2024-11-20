using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 타격의대가 : Skill_Active
{
    private void Awake()
    {
        skill = Skill.GetList()[19];
    }

    public override void init()
    {
        GameManager.instance.AttackRateUp(skill.sk_bagic);
    }

    public override void func()
    {
    }
}