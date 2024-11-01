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

        //레벨 세팅
        int level = 0;
        if (skill.sk_type == SkillType.진화)
            textLevel.text = "";
        else
        {
            if (skill.sk_type == SkillType.패시브 && GameManager.instance.passive.ContainsKey(skill.index))
                level = GameManager.instance.passive[skill.index];
            else if (GameManager.instance.active.ContainsKey(skill.index))
                level = GameManager.instance.active[skill.index];
            textLevel.text = "Lv." + (level + 1);
        }

        //스킬 이름
        textName.text = skill.sk_name;

        //스킬 설명
        textDesc.text = string.Format(skill.sk_desc,
            skill.sk_time - skill.sk_time * level / 6, skill.sk_damage * (level + 1), skill.sk_count * (level + 1));
    }

    public void OnClick()
    {
        GameObject data = SkillContainer.container.GetChild(skill.index).gameObject;

        if (!data.activeSelf)
        {
            data.GetComponent<SkillData>().On();
            if (skill.sk_type == SkillType.진화)
                levelUp.skills.Remove(skill);
        }
        else
        {
            data.GetComponent<SkillData>().levelUp();
            if (data.GetComponent<SkillData>().level == 5)
                levelUp.skills.Remove(skill);
        }
    }
}
