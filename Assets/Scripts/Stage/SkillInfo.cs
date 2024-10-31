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
    GameManager GM;

    public Skill skill;
    public int level;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];

        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    private void Start()
    {
        GM = GameManager.instance;
    }

    public void init(Skill data)
    {
        this.skill = data;

        //������ ����
        this.icon.sprite = Resources.Load<Sprite>($"sk_icon/{skill.sk_name}");

        //���� ����
        int level = 0;
        if (skill.sk_type == SkillType.�нú� && GM.passive.ContainsKey(skill))
            level = GM.passive[skill];
        else if (GM.active.ContainsKey(skill))
            level = GM.active[skill];
        textLevel.text = "Lv."+ (level+1);

        //��ų �̸�
        textName.text = skill.sk_name;

        //��ų ����
        textDesc.text = string.Format(skill.sk_desc,
            skill.sk_time - skill.sk_time * level / 6, skill.sk_damage * (level + 1), skill.sk_count * (level + 1));
    }

    public void OnClick()
    {
        GameObject data = SkillContainer.container.GetChild(skill.index).gameObject;

        if (!data.activeSelf)
            data.GetComponent<SkillData>().On();
        else
            data.GetComponent<SkillData>().levelUp();
    }
}
