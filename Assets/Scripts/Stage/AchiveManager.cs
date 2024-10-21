using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AchiveManager : MonoBehaviour
{
    public GameObject uiNotice;
    public Sprite[] sprites;
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;

    Text achiveDesc;
    Image achiveImage;
    EnumManager.Achive[] achives;
    WaitForSecondsRealtime wait;

    void Awake()
    {
        achives = (EnumManager.Achive[]) Enum.GetValues(typeof(EnumManager.Achive));
        wait = new WaitForSecondsRealtime(5);
        achiveDesc = uiNotice.GetComponentInChildren<Text>();
        achiveImage = uiNotice.GetComponentsInChildren<Image>()[1];

        if (!PlayerPrefs.HasKey("AchiveSet"))
            Init();
    }

    void Init()
    {
        PlayerPrefs.SetInt("AchiveSet", 1);
        foreach (EnumManager.Achive achive in achives)
            PlayerPrefs.SetInt(achive.ToString(), 0);
    }

    void Start()
    {
        //UnlockCharacter();
        //CheckAchive(EnumManager.Achive.UnlockPoato);
    }

    void UnlockCharacter()
    {
        for(int i=0; i<lockCharacter.Length; i++) {
            string achiveName = achives[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            
            lockCharacter[i].SetActive(!isUnlock);
            unlockCharacter[i].SetActive(isUnlock);
        }
    }

    public void CheckAchive(EnumManager.Achive achive){
        if (PlayerPrefs.GetInt(achive.ToString()) != 0){
            Debug.Log("이미 해금된 업적");
            return;
        }
        PlayerPrefs.SetInt(achive.ToString(), 1);

        achiveImage.sprite = sprites[(int)achive];
        achiveDesc.text = LanguageTable.Archive.ArchiveList[(int)achive].kor;
        
        StartCoroutine(NoticeRoutine());
    }

    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        yield return wait;
        uiNotice.SetActive(false);
    }
}
