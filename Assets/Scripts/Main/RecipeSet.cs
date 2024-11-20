using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSet : MonoBehaviour
{
    Image[] icons;
    Text[] names;

    public GameObject Prefab;


    private void Start()
    {
        foreach(var skill in Skill.GetList())
        {
            if (skill.sk_type != SkillType.ÁøÈ­)
                continue;
            print(skill.sk_name);

            GameObject g = Instantiate(Prefab, transform);
            icons = g.GetComponentsInChildren<Image>();
            names = g.GetComponentsInChildren<Text>();

            int cnt = 0;
            foreach (int idx in skill.sk_need) {
                Skill need = Skill.GetList()[idx];
                icons[cnt+1].sprite = Resources.Load<Sprite>($"sk_icon/{need.sk_name}");
                names[cnt * 2].text = need.sk_name;
                cnt++;
            }

            icons[3].sprite = Resources.Load<Sprite>($"sk_icon/{skill.sk_name}");
            names[4].text = skill.sk_name;
        }
    }
}
