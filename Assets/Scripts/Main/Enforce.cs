using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumManager;

public class Enforce : MonoBehaviour
{
    int upNum;
    int forceNum;
    float[] per = { 100, 95, 90, 85, 80, 75, 70, 65, 60, 55, 50, 45, 40, 35, 30, 25, 20, 15, 10, 8, 6, 4, 2, 1, 0.5f };

    Image icon;
    Text[] texts;
    Button btn;
    Equipment equipment;

    public AudioManager audioManager;

    private void Awake()
    {
        btn = GetComponentsInChildren<Button>()[1];
        texts = GetComponentsInChildren<Text>();
        icon = GetComponentsInChildren<Image>()[2];

        btn.onClick.AddListener(()=>Try());
    }

    private void OnEnable()
    {
        upNum = PlayerPrefs.GetInt($"{equipment}Up");

        //장비 아이콘
        icon.sprite = Resources.Load<Sprite>($"equitment/{equipment} {PlayerPrefs.GetInt($"{equipment}Up")}");

        //장비 이름
        string name = "";
        switch (PlayerPrefs.GetInt($"{equipment}Up"))
        {
            case 0:
                name += "<color=#B53BDD>차원의 ";
                break;
            case 1:
                name += "<color=#E88901>격랑의 ";
                break;
        }
        switch (equipment)
        {
            case Equipment.Head:
                name += "머리장식";
                break;
            case Equipment.Chest:
                name += "상의";
                break;
            case Equipment.Leg:
                name += "하의";
                break;
            case Equipment.Hand:
                name += "장갑";
                break;
            case Equipment.Shoulder:
                name += "견갑";
                break;
            case Equipment.Weapon:
                name += "건틀릿";
                break;
        }
        texts[2].text = name + "</color>";
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
        Save.instace.equipments[(int)equipment] = forceNum;
    }

    public void init()
    {
        //강화수치
        texts[1].text = "+" + forceNum;

        //효과
        string str_effect;
        if (equipment == Equipment.Weapon)
            str_effect = "공격력 +";
        else
            str_effect = "체력 +";
        texts[3].text = str_effect + (100 + upNum * 1000 + forceNum * 50);

        //확률
        texts[4].text = per[forceNum] + "%";

        //골드
        int gold = Save.instace.GoldGet();
        int need = 100 * (int)Mathf.Pow(2, forceNum);
        string str_gold = "<color=#";

        //버튼 활성화 포함
        if (gold >= need)
        {
            str_gold += "97D000>";
            btn.interactable = true;
        }
        else
        {
            str_gold += "E73517>";
            btn.interactable = false;
        }
        texts[5].text = str_gold + gold +" / " + need + "</color>";

    }

    public void On(int value)
    {
        equipment = (Equipment)value;
        gameObject.SetActive(true);
        forceNum = Save.instace.equipments[(int)equipment];
        init();
    }

    public void Try()
    {
        int ran = Random.Range(0, 1000);
        Save.instace.GoldSet(-100 * (int)Mathf.Pow(2, forceNum));
        //성공시
        if (ran <= per[forceNum] * 10)
        {
            //사운드 + 이펙트?
            audioManager.PlaySfx(Sfx.Success);
            forceNum++;
        }
        //실패시
        else
        {
            audioManager.PlaySfx(Sfx.Fail);
            //사운드 + 이펙트?

        }
        StartCoroutine(activeCoroutine());
        init();
        btn.interactable = false;
    }


    IEnumerator activeCoroutine()
    {
        yield return new WaitForSeconds(2);
        btn.interactable = true;
    }
}
