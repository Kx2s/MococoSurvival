using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData : MonoBehaviour
{
    public int level=0;
    Skill skill;
    WaitForSeconds wait;

    public void init(Skill data)
    {
        skill = data;
    }

    public void On()
    {
        gameObject.SetActive(true);
        print(skill.sk_name + " ON");

        if (skill.sk_type == SkillType.패시브)
            GameManager.instance.passive[skill.index] = 0;
        else
            GameManager.instance.active[skill.index] = 0;
        levelUp();
    }

    public void levelUp()
    {
        level++;
        wait = new WaitForSeconds(skill.sk_time - skill.sk_time / 6);

        if (skill.sk_type == SkillType.패시브)
            GameManager.instance.passive[skill.index]++;
        else
            GameManager.instance.active[skill.index]++;
        Debug.Log(skill.sk_name + " : Lv" + level);

        switch (skill.sk_type)
        {
            case SkillType.패시브:
                int value = skill.sk_damage * GameManager.instance.passive[skill.index];
                switch (skill.sk_name)
                {
                    case "치명":
                        GameManager.instance.Critical = value;
                        break;
                    case "특화":
                        GameManager.instance.Range = value;
                        break;
                    case "신속":
                        GameManager.instance.Speed = GameManager.instance.baseSpeed * (value + 100)/100;
                        break;
                    case "인내":
                        GameManager.instance.Reduces = value;
                        break;
                    case "숙련":
                        GameManager.instance.Count = value;
                        break;
                    case "제압":
                        GameManager.instance.Slow = value;
                        break;
                    case "생명력":
                        float tmp = GameManager.instance.health / GameManager.instance.baseHealth;
                        GameManager.instance.baseHealth = Character.Health * (value + 100) / 100;
                        GameManager.instance.health = tmp * GameManager.instance.baseHealth;
                        break;
                    case "공격력":
                        GameManager.instance.Attack = GameManager.instance.baseAttack * (value + 100) / 100;
                        break;
                    case "금괴 더미":
                        GameManager.instance.GoldBoost = value;
                        break;
                    case "꼬치구이":
                        GameManager.instance.GoldBoost = value;
                        break;
                }
                break;

            case SkillType.기본:
            case SkillType.액티브:
                break;
        }
    }
}
