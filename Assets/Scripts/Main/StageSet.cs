using System.Collections;
using System.Collections.Generic;
using DataTable;
using EnumManager;
using UnityEngine;
using UnityEngine.UI;

public class StageSet : MonoBehaviour
{
    List<Stage> stages;
    public Sprite[] sprites;
    public GameObject check;
    public GameObject stagePrefab;
    public AudioManager audioManager;
    
    void Start()
    {
        stages = Stage.GetList();

        foreach (Stage s in stages) {
            GameObject g = Instantiate(stagePrefab, transform);
            StageInfo stageInfo = g.AddComponent<StageInfo>();
            stageInfo.info = s;
            stageInfo.check = check;
            g.GetComponentInChildren<Image>().sprite = sprites[(int)s.tema];
            print(audioManager);
            g.GetComponentInChildren<Button>().onClick.AddListener(()=> audioManager.PlaySfx(Sfx.Enter));
        }
    }
}
