using DataTable;
using EnumManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    bool isDestroy;

    RectTransform rect;
    Item[] items;
    SkillInfo[] choices;
    [SerializeField]
    public List<Skill> skills;
    public List<Skill> checkList;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
        choices = GetComponentsInChildren<SkillInfo>();

        checkList = new List<Skill>();
        skills = new List<Skill>();
        foreach (Skill s in Skill.GetList())
            if (s.sk_type == SkillType.패시브 || s.sk_type == SkillType.액티브)
                skills.Add(s);
            else if (s.sk_type == SkillType.진화)
                checkList.Add(s);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }

    public void Next()
    {
        skillSet();
        HashSet<int> ran = new HashSet<int>();

        if (skills.Count < 3 && !isDestroy)
        {
            isDestroy = true;
            StartCoroutine(destroyCoroutine());
        }

        while (ran.Count < Mathf.Min(3, skills.Count))
            ran.Add(Random.Range(0, skills.Count));

        int cnt = 0;
        foreach (int r in ran) {       
            SkillInfo ranSkill = choices[cnt++];
            ranSkill.init(skills[r]);
        }
    }

    public void skillSet()
    {
        skills.Clear();

        foreach (Skill sk in Skill.GetList())
        {
            int level = 0;
            switch (sk.sk_type)
            {
                case SkillType.기본:
                    if (GameManager.instance.active.TryGetValue(sk.index, out level) && level < 5)
                        skills.Add(sk);
                    break;

                case SkillType.패시브:
                    if ((GameManager.instance.passive.TryGetValue(sk.index, out level) && level < 5)
                        || level == 0 && GameManager.instance.passive.Count < 5)
                        skills.Add(sk);
                    break;

                case SkillType.액티브:
                    if (!GameManager.instance.active.ContainsKey(sk.sk_upper[0]) &&
                        ((GameManager.instance.active.TryGetValue(sk.index, out level) && level < 5)
                        || level == 0 && GameManager.instance.active.Count < 5))
                        skills.Add(sk);
                    break;

                case SkillType.진화:
                    if (!GameManager.instance.active.TryGetValue(sk.index, out level))
                    {
                        bool tf = true;
                        foreach (int need in sk.sk_need)
                        {
                            Skill tmp = Skill.GetList()[need];
                            if (tmp.sk_type == SkillType.패시브)
                            {
                                if (!GameManager.instance.passive.ContainsKey(need))
                                    tf = false;
                            }
                            else
                            {
                                if (!GameManager.instance.active.ContainsKey(need)
                                    || GameManager.instance.active[need] != 5)
                                    tf = false;
                            }
                        }
                        if (tf)                        
                            skills.Add(sk);                        
                    }
                    break;
            }
        }
        if (skills.Count < 3)
        {
            Skill tmp1 = new Skill();
            Skill tmp2 = new Skill();
            tmp1.index = -1;
            tmp2.index = -1;
            tmp1.sk_name = "정령의 회복약";
            tmp2.sk_name = "골드 상자";
            skills.Add(tmp1);
            skills.Add(tmp2);
        }

        string str = "랜덤 스킬 목록 : ";
        foreach(Skill skill in skills)
            str += skill.sk_name+" ";
        
        print(str);
    }

    IEnumerator destroyCoroutine()
    {
        Destroy(choices[choices.Length - 1].gameObject);
        yield return new WaitForSeconds(0.5f);
        choices = GetComponentsInChildren<SkillInfo>();
    }
}