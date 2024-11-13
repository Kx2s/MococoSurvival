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
            skillPauses[cnt++].init(Skill.GetList()[pair.Key], pair.Value);

        for (; cnt < 5; cnt++)
            skillPauses[cnt].init(null, 0);

        foreach (KeyValuePair<int, int> pair in GameManager.instance.passive)
            skillPauses[cnt++].init(Skill.GetList()[pair.Key], pair.Value);

        for (; cnt < 10; cnt++)
            skillPauses[cnt].init(null, 0);
    }
}
