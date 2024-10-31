using DataTable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;
    SkillInfo[] choices;
    public List<Skill> skills;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
        choices = GetComponentsInChildren<SkillInfo>();

        skills = new List<Skill>();
        foreach (Skill s in Skill.GetList())
            if (s.sk_type == SkillType.패시브 || s.sk_type == SkillType.액티브)
                skills.Add(s);
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
}