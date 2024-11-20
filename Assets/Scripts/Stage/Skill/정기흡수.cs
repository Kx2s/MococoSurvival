using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 정기흡수 : Skill_Active
{
    private void Awake()
    {
        skill = Skill.GetList()[17];
    }

    public override void init()
    {
        GameManager.instance.AttackSpeed += skill.sk_increase;
    }

    public override void func()
    {

    }
}