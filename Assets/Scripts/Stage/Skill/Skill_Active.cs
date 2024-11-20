using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill_Active : MonoBehaviour, Skill_Basic
{
    public Skill skill;
    public int level = 0;

    private void OnEnable()
    {
        gameObject.SetActive(true);
        print(skill.sk_name + " ON");
        levelUp();

        func();
    }

    public void levelUp()
    {
        level++;
        GameManager.instance.active[skill.index] = level;
        init();
    }
    public float coolTime()
    {
        float levelTime = 1 - (level - 1) / 5;
        float SpeedTime = 1 - GameManager.instance.AttackSpeed / 100;
        return skill.sk_time - (levelTime * SpeedTime);
    }

    public abstract void init();

    public abstract void func();
}