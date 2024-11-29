using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoySet : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("Joy") == 1;
    }
}
