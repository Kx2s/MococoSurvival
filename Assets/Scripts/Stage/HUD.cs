using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    enum textType { Level, Time, Kill }
    enum sliderType { Exp, Health }

    Text[] myText;
    Slider[] mySlider;

    void Awake()
    {
        myText = GetComponentsInChildren<Text>();
        mySlider = GetComponentsInChildren<Slider>();
    }

    public void uiHunting()
    {
        float curExp = GameManager.instance.exp;
        float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
        mySlider[(int)sliderType.Exp].value = curExp / maxExp;

        myText[(int)textType.Level].text = string.Format("Lv.{0:F0}", GameManager.instance.level);
        myText[(int)textType.Kill].text = string.Format("{0:F0}", GameManager.instance.kill);
    }

    public void uiTime()
    {
        int min = Mathf.FloorToInt(GameManager.instance.gameTime / 60);
        int sec = Mathf.FloorToInt(GameManager.instance.gameTime % 60);
        myText[(int)textType.Time].text = string.Format("{0:D2}:{1:D2}", min, sec);
    }

    public void uiHealth()
    {
        float curHealth = GameManager.instance.health;
        float maxHealth = GameManager.instance.baseHealth;
        mySlider[(int)sliderType.Health].value = curHealth / maxHealth;
    }
}
