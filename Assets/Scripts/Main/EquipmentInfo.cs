using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumManager;
using UnityEngine.UI;

public class EquipmentInfo : MonoBehaviour
{
    public Equipment equipment;

    Image icon;
    Text forceNum;

    private void Awake()
    {
        icon = GetComponent<Image>();
        forceNum = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        icon.sprite = Resources.Load<Sprite>($"equitment/{equipment} {PlayerPrefs.GetInt($"{equipment}Up")}");
    }

    private void OnEnable()
    {
        init();
    }

    public void init()
    {
        forceNum.text = "+" + Save.instace.equipments[(int) equipment];
    }
}