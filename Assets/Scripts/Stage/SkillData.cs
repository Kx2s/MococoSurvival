using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData : MonoBehaviour
{
    public int level=0;
    Skill skill;
    float time;

    public void init(Skill data)
    {
        skill = data;
    }

    public void On()
    {
        gameObject.SetActive(true);
        print(skill.sk_name + " ON");
        levelUp();

        if (skill.sk_type != SkillType.�нú�)
        {
            switch (skill.sk_name)
            {
                case "��ȭ����":
                    StopCoroutine(��ȭ����());
                    StartCoroutine(��ȭ����());
                    break;
                case "��������":
                    break;
                case "����":
                    break;
                case "ȸ���� ����ź":
                    StartCoroutine(ȸ��������ź());
                    break;
                case "ȭ�� ����ź":
                    StartCoroutine(ȭ������ź());
                    break;
                case "���ɼ�":
                    break;
                case "�ٸ����̵�":
                    StartCoroutine(�ٸ����̵�());
                    break;
                case "���׸� ������":
                    break;
                case "���丮���� ����":
                    break;
                case "���߹� ������":
                    break;
                case "���ֹ��� ����":
                    break;
                case "���ݴ���":
                    break;
            }
        }
    }

    public void levelUp()
    {
        level++;

        if (skill.sk_type == SkillType.�нú�)
        {
            GameManager.instance.passive[skill.index] = level;
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
                    GameManager.instance.Speed = GameManager.instance.baseSpeed * (value + 100) / 100;
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
                    GameManager.instance.Attack = GameManager.instance.baseAttack * (value+100)/100;
                    break;
                case "�ݱ� ����":
                    GameManager.instance.GoldBoost = value;
                    break;
                case "��ġ����":
                    GameManager.instance.GoldBoost = value;
                    break;
            }
        }
        else
        {
            GameManager.instance.active[skill.index] = level;
            time = skill.sk_time - skill.sk_time / 6;
            switch (skill.sk_name)
            {
                case "������ �밡":
                    GameManager.instance.AttackRate += skill.sk_damage;
                    break;
                case "�������":
                    GameManager.instance.AttackSpeed += skill.sk_damage;
                    break;
                case "Ÿ���� �밡":
                    GameManager.instance.AttackRate += skill.sk_damage - Skill.GetList()[11].sk_damage * 5;
                    break;
            }
        }
        Debug.Log(skill.sk_name + " : Lv" + level);
    }

    IEnumerator ��ȭ����()
    {
        while (true)
        {
            GameManager.instance.Shield = GameManager.instance.baseHealth * skill.sk_damage/100;
            print("shield reset");

            yield return GameManager.instance.Shield < GameManager.instance.baseHealth * skill.sk_damage/100;
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator ��������()
    {
        while (true)
        {
            //������ ����

            yield return null;
        }
    }

    IEnumerator ȸ��������ź()
    {
        while (true)
        {

            Vector2 ran = Random.insideUnitCircle.normalized;
            int total = skill.sk_count*level + GameManager.instance.Count;
            float angle = 360 / total;

            for (int i = 1; i <= total; i++)
            {
                RectTransform trans = GameManager.instance.pool.Get(3).transform.gameObject.GetComponent<RectTransform>();
                trans.position = transform.position;
                
                //�ӽ�
                trans.position += Quaternion.Euler(0, 0, angle * i) * ran * 2;
            }

            yield return new WaitForSeconds(time - time * GameManager.instance.AttackSpeed / 100);
        }
    }

    IEnumerator ȭ������ź()
    {
        while (true)
        {

            Vector2 ran = Random.insideUnitCircle.normalized;
            int total = skill.sk_count * level + GameManager.instance.Count;
            float angle = 360 / total;

            for (int i = 1; i <= total; i++)
            {
                RectTransform trans = GameManager.instance.pool.Get(4).transform.gameObject.GetComponent<RectTransform>();
                trans.position = transform.position;

                //�ӽ�
                trans.position += Quaternion.Euler(0, 0, angle * i) * ran * 2;
            }
            print(time - time * GameManager.instance.AttackSpeed / 100);
            yield return new WaitForSeconds(time - time * GameManager.instance.AttackSpeed / 100);
        }
    }

    IEnumerator �ٸ����̵�()
    {
        while (true)
        {
            yield return GameManager.instance.Shield > 0;
            GameManager.instance.AttackRate += skill.sk_damage;

            yield return GameManager.instance.Shield == 0;
            GameManager.instance.AttackRate -= skill.sk_damage;
        }
    }

    IEnumerator ���� ()
    {
        yield break;
    }

    private void OnDisable()
    {
        GameManager.instance.active.Remove(skill.index);
        StopAllCoroutines();
    }
}
