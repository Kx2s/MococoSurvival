using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using EnumManager;

public class NewPlayer : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("begin"))
            return;

        PlayerPrefs.SetInt("begin", 1);

        //���
        PlayerPrefs.SetInt("Gold", 0);

        //��ȭ, ���׷��̵��ġ
        for (int i = 0; i < 6; i++)
        {
            PlayerPrefs.SetInt($"{(Equipment)i}", 0);
            PlayerPrefs.SetInt($"{(Equipment)i}Up", 0);
        }

        PlayerPrefs.SetInt("Joy", 1);
    }
}
