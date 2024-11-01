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

        if (skill.sk_type == SkillType.�нú�)
            GameManager.instance.passive[skill.index] = 0;
        else
            GameManager.instance.active[skill.index] = 0;
        levelUp();
    }

    public void levelUp()
    {
        level++;
        wait = new WaitForSeconds(skill.sk_time - skill.sk_time / 6);

        if (skill.sk_type == SkillType.�нú�)
            GameManager.instance.passive[skill.index]++;
        else
            GameManager.instance.active[skill.index]++;
        Debug.Log(skill.sk_name + " : Lv" + level);

        switch (skill.sk_type)
        {
            case SkillType.�нú�:
                int value = skill.sk_damage * GameManager.instance.passive[skill.index];
                switch (skill.sk_name)
                {
                    case "ġ��":
                        GameManager.instance.Critical = value;
                        break;
                    case "Ưȭ":
                        GameManager.instance.Range = value;
                        break;
                    case "�ż�":
                        GameManager.instance.Speed = GameManager.instance.baseSpeed * (value + 100)/100;
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
                        float tmp = GameManager.instance.health / GameManager.instance.baseHealth;
                        GameManager.instance.baseHealth = Character.Health * (value + 100) / 100;
                        GameManager.instance.health = tmp * GameManager.instance.baseHealth;
                        break;
                    case "���ݷ�":
                        GameManager.instance.Attack = GameManager.instance.baseAttack * (value + 100) / 100;
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
