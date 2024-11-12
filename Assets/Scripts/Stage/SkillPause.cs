using DataTable;
using UnityEngine;
using UnityEngine.UI;

public class SkillPause : MonoBehaviour
{
    Text name;
    Image icon;
    Image[] Lv;

    public Sprite on;

    private void Awake()
    {
        name = GetComponentInChildren<Text>();

        Image[] tmp = GetComponentsInChildren<Image>();
        icon = tmp[1];

        Lv = new Image[5];
        for (int i=0; i<5; i++)
            Lv[i] = tmp[i+2];
    }

    public void init(Skill skill, int lv)
    {
        //아이콘 변경
        icon.sprite = Resources.Load<Sprite>($"sk_icon/{skill.sk_name}");
        Color color = icon.color;
        color.a = 255;
        icon.color = color;

        //이름 변경
        name.text = skill.sk_name;

        //레벨 
        if (lv == 0)
            Lv[0].transform.parent.gameObject.SetActive(false);

        for (int i =0; i<lv; i++)
            Lv[i].sprite = on;
    }
}
