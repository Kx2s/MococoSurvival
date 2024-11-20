using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    enum textType { Gold, Time, Kill }
    enum sliderType { Exp, Health, Shield }

    Text[] myText;
    Slider[] mySlider;
    FollowShield follow;

    void Awake()
    {
        myText = GetComponentsInChildren<Text>();
        mySlider = GetComponentsInChildren<Slider>();
        follow = GetComponentInChildren<FollowShield>();

        uiHunting();
        uigold();
    }

    public void uiHunting()
    {
        float curExp = GameManager.instance.exp;
        float maxExp = GameManager.instance.nextExp;
        mySlider[(int)sliderType.Exp].value = curExp / maxExp;

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
        float shield = GameManager.instance.Shield;
        float maxHealth = GameManager.instance.baseHealth + shield;

        follow.UpdateTransform(curHealth / maxHealth);
        mySlider[(int)sliderType.Health].value = curHealth / maxHealth;
        mySlider[(int)sliderType.Shield].value = shield / maxHealth;
    }

    public void uigold()
    {
        myText[(int)textType.Gold].text = string.Format("{0:n0}", GameManager.instance.Gold);
    }
}
