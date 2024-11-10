using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumManager;

public class Save : MonoBehaviour
{
    [SerializeField]
    private int Gold;
    public int[] equipments;
    public Text GoldText;

    public static Save instace;
    
    private void Awake()
    {
        instace = this;
        equipments = new int[6];
        GoldSet(PlayerPrefs.GetInt("Gold"));

        for (int i=0; i<equipments.Length; i++)
            equipments[i] = PlayerPrefs.GetInt($"{(Equipment)i}");
    }

    public void GoldSet(int value)
    {
        Gold += value;
        GoldText.text = $"{Gold}";
    }

    public int GoldGet()
    {
        return Gold;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("Gold", Gold);
        for (int i=0; i < equipments.Length; i++)
            PlayerPrefs.SetInt($"{(Equipment)i}", equipments[i]);

        PlayerPrefs.Save();
    }
}
