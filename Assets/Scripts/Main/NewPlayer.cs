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

        PlayerPrefs.SetInt("begin", 1);
        PlayerPrefs.SetInt("Weapon", 0);
        PlayerPrefs.SetInt("Head", 0);
        PlayerPrefs.SetInt("Chest", 0);
        PlayerPrefs.SetInt("Leg", 0);
        PlayerPrefs.SetInt("Hand", 0);
        PlayerPrefs.SetInt("Foot", 0);
    }
}
