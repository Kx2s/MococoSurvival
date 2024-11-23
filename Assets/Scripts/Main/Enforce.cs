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

        //��� ������
        icon.sprite = Resources.Load<Sprite>($"equitment/{equipment} {PlayerPrefs.GetInt($"{equipment}Up")}");

        //��� �̸�
        string name = "";
        switch (PlayerPrefs.GetInt($"{equipment}Up"))
        {
            case 0:
                name += "<color=#B53BDD>������ ";
                break;
            case 1:
                name += "<color=#E88901>�ݶ��� ";
                break;
        }
        switch (equipment)
        {
            case Equipment.Head:
                name += "�Ӹ����";
                break;
            case Equipment.Chest:
                name += "����";
                break;
            case Equipment.Leg:
                name += "����";
                break;
            case Equipment.Hand:
                name += "�尩";
                break;
            case Equipment.Shoulder:
                name += "�߰�";
                break;
            case Equipment.Weapon:
                name += "��Ʋ��";
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
        //��ȭ��ġ
        texts[1].text = "+" + forceNum;

        //ȿ��
        string str_effect;
        if (equipment == Equipment.Weapon)
            str_effect = "���ݷ� +";
        else
            str_effect = "ü�� +";
        texts[3].text = str_effect + (100 + upNum * 1000 + forceNum * 50);

        //Ȯ��
        texts[4].text = per[forceNum] + "%";

        //���
        int gold = Save.instace.GoldGet();
        int need = 100 * (int)Mathf.Pow(2, forceNum);
        string str_gold = "<color=#";

        //��ư Ȱ��ȭ ����
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
        //������
        if (ran <= per[forceNum] * 10)
        {
            //���� + ����Ʈ?
            audioManager.PlaySfx(Sfx.Success);
            forceNum++;
        }
        //���н�
        else
        {
            audioManager.PlaySfx(Sfx.Fail);
            //���� + ����Ʈ?

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
