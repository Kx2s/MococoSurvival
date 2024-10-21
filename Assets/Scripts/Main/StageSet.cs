using System.Collections;
using System.Collections.Generic;
using DataTable;

using UnityEngine;
using UnityEngine.UI;

public class StageSet : MonoBehaviour
{
    List<Stage> stages;
    public Sprite[] sprites;
    public GameObject check;
    public GameObject stagePrefab;
    
    void Start()
    {
        stages = Stage.GetList();

        foreach (Stage s in stages) {
            GameObject g = Instantiate(stagePrefab, transform);
            StageInfo stageInfo = g.AddComponent<StageInfo>();
            stageInfo.info = s;
            stageInfo.check = check;
            g.GetComponentInChildren<Image>().sprite = sprites[(int)s.tema];
        }
    }
}
