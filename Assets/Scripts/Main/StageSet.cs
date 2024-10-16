using System.Collections;
using System.Collections.Generic;
using DataTable;

using UnityEngine;
using UnityEngine.UI;

public class StageSet : MonoBehaviour
{
    List<Stage> stages;
    public Sprite[] sprites;
    public GameObject stagePrefab;
    
    void Start()
    {
        stages = Stage.GetList();

        foreach (Stage s in stages) {
            print(s.index);
            GameObject g = Instantiate(stagePrefab, transform);
            g.AddComponent<StageInfo>().info = s;
            g.GetComponentInChildren<Image>().sprite = sprites[(int)s.tema];
        }
    }
}
