using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill_Passive : MonoBehaviour, Skill_Basic
{
    public Skill skill;
    public int level = 0;

    public void OnEnable()
    {
        print(skill.sk_name + " ON");
        levelUp();
    }

    public void levelUp()
    {
        level++;
        GameManager.instance.passive[skill.index] = level;
        init();
    }

    public abstract void init();
}