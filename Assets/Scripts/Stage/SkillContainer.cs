using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillContainer : MonoBehaviour
{
    public static Transform container;

    private void Awake()
    {
        container = transform;
    }

    private void Start()
    {
        foreach(Skill skill in Skill.GetList())
        {
            GameObject g = new GameObject(skill.sk_name);
            g.SetActive(false);
            g.transform.parent = transform;
            g.AddComponent<SkillData>();
            g.GetComponent<SkillData>().init(skill);
        }
    }
}