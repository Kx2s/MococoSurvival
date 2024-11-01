using DataTable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
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
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }

    public void Select(int idx)
    {
        print("Select " + idx);
        items[idx].OnClick();
    }

    public void Next()
    {
        check();
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, skills.Count);
            ran[1] = Random.Range(0, skills.Count);
            ran[2] = Random.Range(0, skills.Count);

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int i = 0; i < ran.Length; i++)
        {
            SkillInfo ranSkill = choices[i];
            ranSkill.init(skills[ran[i]]);
        }
    }

    public void check()
    {
        List<Skill> list = new List<Skill>(checkList);
        //진화스킬 체크
        foreach (Skill skill in list)
        {
            bool tf = true;
            foreach (int need in skill.sk_need)
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
            {
                skills.Add(skill);
                checkList.Remove(skill);
            }
        }

        list = new List<Skill>(skills);
        if (GameManager.instance.passive.Count == 5)
        {
            foreach (Skill skill in list)
                if (skill.sk_type == SkillType.패시브 && !GameManager.instance.passive.ContainsKey(skill.index))
                    skills.Remove(skill);
            GameManager.instance.passive.Add(-1, 0);
        }

        if (GameManager.instance.active.Count == 5)
        {
            foreach (Skill skill in list)
                if (skill.sk_type == SkillType.액티브 && !GameManager.instance.active.ContainsKey(skill.index))
                    skills.Remove(skill);
            GameManager.instance.active.Add(-1, 0);
        }

        string str = "";
        foreach(Skill skill in skills)
            str += skill.sk_name+" ";
        
        print(str);
    }
}