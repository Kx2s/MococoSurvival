using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfo : MonoBehaviour
{
    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;
    LevelUp levelUp;

    public Skill skill;
    public int level;


    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        levelUp = GetComponentInParent<LevelUp>();

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void init(Skill data)
    {
        this.skill = data;

        //아이콘 변경
        this.icon.sprite = Resources.Load<Sprite>($"sk_icon/{skill.sk_name}");

        //스킬 이름
        textName.text = skill.sk_name;

        if (skill.index == -1)
        {
            textLevel.text = "";
            if (skill.sk_name.Equals("정령의 회복약"))
            {
                textDesc.text = "생명력 50% 회복";
            }
            else
            {
                textDesc.text = "1000 골드 획득";
            }
            return;
        }

        //레벨 세팅
        int level = 0;
        if (skill.sk_type == SkillType.진화)
            textLevel.text = "Lv.MAX";
        else
        {
            if (skill.sk_type == SkillType.패시브 && GameManager.instance.passive.ContainsKey(skill.index))
                level = GameManager.instance.passive[skill.index];
            else if (GameManager.instance.active.ContainsKey(skill.index))
                level = GameManager.instance.active[skill.index];
            textLevel.text = "Lv." + (level + 1);
        }

        //스킬 설명
        textDesc.text = string.Format(skill.sk_desc, skill.sk_increase);
    }

    public void OnClick()
    {
        if (skill.index == -1)
        {
            if (skill.sk_name.Equals("정령의 회복약"))
            {
                GameManager.instance.addhealth(GameManager.instance.baseHealth / 2);
            }
            else
            {
                GameManager.instance.addGold(1000);
            }
            return;
        }

        GameObject data = SkillContainer.container.GetChild(skill.index).gameObject;

        if (!data.activeSelf)
        {
            data.SetActive(true);
            if (skill.sk_type == SkillType.진화)
            {
                foreach(int idx in skill.sk_need)
                    if (Skill.GetList()[idx].sk_type != SkillType.패시브)
                        GameManager.instance.active.Remove(idx);
            }
        }
        else
        {
            data.GetComponent<Skill_Basic>().levelUp();
        }
    }
}
