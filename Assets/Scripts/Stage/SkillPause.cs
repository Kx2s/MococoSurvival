using DataTable;
using UnityEngine;
using UnityEngine.UI;

public class SkillPause : MonoBehaviour
{
    Text name;
    Image icon;
    Image[] Lv;

    public Sprite on;
    public Sprite off;

    private void Awake()
    {
        name = GetComponentInChildren<Text>();

        Image[] tmp = GetComponentsInChildren<Image>(true);
        icon = tmp[1];

        Lv = new Image[5];
        for (int i=0; i<5; i++)
            Lv[i] = tmp[i+2];
    }

    public void init(Skill skill, int lv)
    {
        //������ ����
        Color color = icon.color;
        if (skill == null)
        {
            color.a = 0;
        }
        else
        {
            icon.sprite = Resources.Load<Sprite>($"sk_icon/{skill.sk_name}");
            color.a = 255;
        }
        icon.color = color;


        //�̸� ����
        name.text = skill!=null ? skill.sk_name : "";

        //���� 
        if (skill != null && skill.sk_type == SkillType.��ȭ)
            Lv[0].transform.parent.gameObject.SetActive(false);
        else
            Lv[0].transform.parent.gameObject.SetActive(true);

        for (int i =0; i<5; i++)
        {
            if (i < lv)
                Lv[i].sprite = on;
            else
                Lv[i].sprite = off;
        }
    }
}
