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

        if (skill.sk_type == SkillType.�нú�)
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

        if (skill.sk_type == SkillType.�нú�)
            GameManager.instance.passive[skill]++;
        else
            GameManager.instance.active[skill]++;
        Debug.Log(skill.sk_name + " : Lv" + level);

        switch (skill.sk_type)
        {
            case SkillType.�нú�:
                int value = skill.sk_damage * GameManager.instance.passive[skill];
                switch (skill.sk_name)
                {
                    case "ġ��":
                        GameManager.instance.Critical = value;
                        break;
                    case "Ưȭ":
                        GameManager.instance.Range = value;
                        break;
                    case "�ż�":
                        GameManager.instance.Speed = value;   //�����ʿ�
                        break;
                    case "�γ�":
                        GameManager.instance.Reduces = value;
                        break;
                    case "����":
                        GameManager.instance.Count = value;
                        break;
                    case "����":
                        GameManager.instance.Slow = value;
                        break;
                    case "�����":
                        float tmp = GameManager.instance.health / GameManager.instance.maxHealth;
                        print(tmp);
                        print(GameManager.instance.maxHealth);
                        GameManager.instance.maxHealth = Character.Health * (value + 100) / 100;
                        GameManager.instance.health = tmp * GameManager.instance.maxHealth;
                        print(GameManager.instance.maxHealth);
                        break;
                    case "���ݷ�":
                        GameManager.instance.Damage = Character.Damage * (value + 100) / 100;
                        break;
                    case "�ݱ� ����":
                        GameManager.instance.GoldBoost = value;
                        break;
                    case "��ġ����":
                        GameManager.instance.GoldBoost = value;
                        break;
                }
                break;

            case SkillType.�⺻:
            case SkillType.��Ƽ��:
                break;
        }
    }
}
