using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 돌격대장 : Skill_Active
{
    WaitForSeconds wait = new WaitForSeconds(1);

    private void Awake()
    {
        skill = Skill.GetList()[24];
        level = 5;
    }

    public override void init()
    {
        GameManager.instance.AttackRateUp(50);
    }

    public override void func()
    {
        
    }
}