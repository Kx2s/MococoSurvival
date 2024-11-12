using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    SkillPause[] skillPauses;

    private void Awake()
    {
        skillPauses = GetComponentsInChildren<SkillPause>();
    }

    private void OnEnable()
    {
        int cnt = 0;
        foreach (KeyValuePair<int, int> pair in GameManager.instance.active)
        {
            if (cnt >= 5)
                break;
            skillPauses[cnt++].init(Skill.GetList()[pair.Key], pair.Value);
        }

        cnt = 5;
        foreach (KeyValuePair<int, int> pair in GameManager.instance.passive)
        {
            if (cnt >= 10)
                break;
            skillPauses[cnt++].init(Skill.GetList()[pair.Key], pair.Value);
        }
    }
}
