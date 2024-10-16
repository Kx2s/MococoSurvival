using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    Button exit;
    Toggle vibration;
    Slider[] sliders;
    public AudioMixer mixer;

    private void Awake()
    {
        exit = GetComponentInChildren<Button>();
        vibration = GetComponentInChildren<Toggle>();
        sliders = GetComponentsInChildren<Slider>();

        exit.onClick.AddListener(()=>gameObject.SetActive(false));
        sliders[0].onValueChanged.AddListener((v) => mixer.SetFloat("BGM", v));
        sliders[1].onValueChanged.AddListener((v) => mixer.SetFloat("SFX", v));

        if (!PlayerPrefs.HasKey("BGM")){
            PlayerPrefs.SetFloat("BGM", 0);
            PlayerPrefs.SetFloat("SFX", 0);
            PlayerPrefs.SetInt("Vibration", 1);
        }
    }

    //설정 불러오기
    private void OnEnable() {
        sliders[0].value = PlayerPrefs.GetFloat("BGM");
        sliders[1].value = PlayerPrefs.GetFloat("SFX");
        vibration.isOn = PlayerPrefs.GetInt("Vibration")==1;
    }

    //설정 저장
    private void OnDisable() {
        PlayerPrefs.SetFloat("BGM", sliders[0].value);
        PlayerPrefs.SetFloat("SFX", sliders[1].value);
        PlayerPrefs.SetFloat("Vibration", vibration.isOn? 1:0);
    }
}
