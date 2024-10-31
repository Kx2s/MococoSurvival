using DataTable;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkillData : MonoBehaviour
{
    int level=0;
    Skill skill;
    WaitForSeconds wait;

    public void init(Skill data)
    {
        GameManager.instance = GameManager.instance;
        skill = data;
    }

    public void On()
    {
        gameObject.SetActive(true);
        print(skill.sk_name + " ON");

        if (skill.sk_type == SkillType.패시브)
            GameManager.instance.passive[skill] = 0;
        else
            GameManager.instance.active[skill] = 0;
        levelUp();
    }

    public void levelUp()
    {
        level++;
        wait = new WaitForSeconds(skill.sk_time - skill.sk_time / 6);
        print(GameManager.instance);

        if (skill.sk_type == SkillType.패시브)
            GameManager.instance.passive[skill]++;
        else
            GameManager.instance.active[skill]++;
        Debug.Log(skill.sk_name + " : Lv" + level);

        switch (skill.sk_type)
        {
            case SkillType.패시브:
                int value = skill.sk_damage * GameManager.instance.passive[skill];
                switch (skill.sk_name)
                {
                    case "치명":
                        GameManager.instance.Critical = value;
                        break;
                    case "특화":
                        GameManager.instance.Range = value;
                        break;
                    case "신속":
                        GameManager.instance.Speed = value;   //수정필요
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
                        float tmp = GameManager.instance.health / GameManager.instance.maxHealth;
                        print(tmp);
                        print(GameManager.instance.maxHealth);
                        GameManager.instance.maxHealth = Character.Health * (value + 100) / 100;
                        GameManager.instance.health = tmp * GameManager.instance.maxHealth;
                        print(GameManager.instance.maxHealth);
                        break;
                    case "공격력":
                        GameManager.instance.Damage = Character.Damage * (value + 100) / 100;
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
