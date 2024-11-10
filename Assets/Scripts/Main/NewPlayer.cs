using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewPlayer : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("begin"))
            return;

        //���߿� Ǯ�����
        //PlayerPrefs.SetInt("begin", 1);

        //���
        PlayerPrefs.SetInt("Gold", 10000);

        //��ȭ��ġ
        PlayerPrefs.SetInt("Weapon", 0);
        PlayerPrefs.SetInt("Head", 0);
        PlayerPrefs.SetInt("Chest", 0);
        PlayerPrefs.SetInt("Leg", 0);
        PlayerPrefs.SetInt("Hand", 0);
        PlayerPrefs.SetInt("Shoulder", 0);

        //���׷��̵� ��ġ
        PlayerPrefs.SetInt("WeaponUp", 0);
        PlayerPrefs.SetInt("HeadUp", 0);
        PlayerPrefs.SetInt("ChestUp", 1);
        PlayerPrefs.SetInt("LegUp", 0);
        PlayerPrefs.SetInt("HandUp", 0);
        PlayerPrefs.SetInt("ShoulderUp", 0);
    }
}
