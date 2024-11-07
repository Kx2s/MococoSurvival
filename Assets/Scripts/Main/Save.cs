using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    [SerializeField]
    private int Gold;
    public Text GoldText;

    public static Save instace;
    
    private void Awake()
    {
        instace = this;
    }


    public void GoldSet(int value)
    {
        Gold += value;
        GoldText.text = $"{Gold}";
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("Gold", Gold);



        PlayerPrefs.Save();
    }
}
