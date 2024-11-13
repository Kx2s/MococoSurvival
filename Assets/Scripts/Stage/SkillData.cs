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

        if (skill.sk_type != SkillType.패시브)
        {
            switch (skill.sk_name)
            {
                case "강화방패":
                    StopCoroutine(강화방패());
                    StartCoroutine(강화방패());
                    break;
                case "구슬동자":
                    break;
                case "전극":
                    break;
                case "회오리 수류탄":
                    StartCoroutine(회오리수류탄());
                    break;
                case "화염 수류탄":
                    StartCoroutine(화염수류탄());
                    break;
                case "강령술":
                    break;
                case "바리케이드":
                    StartCoroutine(바리케이드());
                    break;
                case "에테르 포식자":
                    break;
                case "실페리온의 도움":
                    break;
                case "폭발물 전문가":
                    break;
                case "저주받은 인형":
                    break;
                case "돌격대장":
                    break;
            }
        }
    }

    public void levelUp()
    {
        level++;

        if (skill.sk_type == SkillType.패시브)
        {
            GameManager.instance.passive[skill.index] = level;
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
                    GameManager.instance.Speed = GameManager.instance.baseSpeed * (value + 100) / 100;
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
                    GameManager.instance.Attack = GameManager.instance.baseAttack * (value+100)/100;
                    break;
                case "금괴 더미":
                    GameManager.instance.GoldBoost = value;
                    break;
                case "꼬치구이":
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
                case "결투의 대가":
                    GameManager.instance.AttackRate += skill.sk_damage;
                    break;
                case "정기흡수":
                    GameManager.instance.AttackSpeed += skill.sk_damage;
                    break;
                case "타격의 대가":
                    GameManager.instance.AttackRate += skill.sk_damage - Skill.GetList()[11].sk_damage * 5;
                    break;
            }
        }
        Debug.Log(skill.sk_name + " : Lv" + level);
    }

    IEnumerator 강화방패()
    {
        while (true)
        {
            GameManager.instance.Shield = GameManager.instance.baseHealth * skill.sk_damage/100;
            print("shield reset");

            yield return GameManager.instance.Shield < GameManager.instance.baseHealth * skill.sk_damage/100;
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator 구슬동자()
    {
        while (true)
        {
            //아이템 생성

            yield return null;
        }
    }

    IEnumerator 회오리수류탄()
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
                
                //임시
                trans.position += Quaternion.Euler(0, 0, angle * i) * ran * 2;
            }

            yield return new WaitForSeconds(time - time * GameManager.instance.AttackSpeed / 100);
        }
    }

    IEnumerator 화염수류탄()
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

                //임시
                trans.position += Quaternion.Euler(0, 0, angle * i) * ran * 2;
            }
            print(time - time * GameManager.instance.AttackSpeed / 100);
            yield return new WaitForSeconds(time - time * GameManager.instance.AttackSpeed / 100);
        }
    }

    IEnumerator 바리케이드()
    {
        while (true)
        {
            yield return GameManager.instance.Shield > 0;
            GameManager.instance.AttackRate += skill.sk_damage;

            yield return GameManager.instance.Shield == 0;
            GameManager.instance.AttackRate -= skill.sk_damage;
        }
    }

    IEnumerator 전극 ()
    {
        yield break;
    }

    private void OnDisable()
    {
        GameManager.instance.active.Remove(skill.index);
        StopAllCoroutines();
    }
}
