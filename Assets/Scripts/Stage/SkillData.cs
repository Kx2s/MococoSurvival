using DataTable;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillData : MonoBehaviour
{
    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;
    GameManager GM;

    public Skill skill;
    public int level;
    public ItemData data;
    public Weapon weapon;
    public Gear gear;

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
        print(skill.index);
        print(skill.sk_name);

        //아이콘 변경
        this.icon.sprite = Resources.Load<Sprite>($"sk_icon/{skill.sk_name}");
        print(icon.sprite);
        print(GM);
        print(GM.passive.Count);
        //레벨 세팅
        int level = 0;
        if (skill.sk_type == SkillType.패시브 && GM.passive.ContainsKey(skill))
            level = GM.passive[skill];
        else if (GM.active.ContainsKey(skill))
            level = GM.active[skill];
        textLevel.text = "Lv."+ level+1;

        //스킬 이름
        textName.text = skill.sk_name;

        //스킬 설명
        textDesc.text = string.Format(skill.sk_desc,
            skill.sk_time - skill.sk_time * level / 6, skill.sk_damage * (level + 1), skill.sk_count * (level + 1));
    }

    public void OnClick()
    {
        int level = 0;
        if (skill.sk_type == SkillType.패시브)
        {
            if (GM.passive.ContainsKey(skill))
                level = ++GM.passive[skill];
        }
        else
        {
            if (GM.active.ContainsKey(skill))
                level = ++GM.active[skill];
        }

        //레벨당 변화 추가 예정

        /*
        switch(data.itemType) {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (level == 0){
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }

                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (level == 0) {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }

                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                return;
        }
        */
    }
}
