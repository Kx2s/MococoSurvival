using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 풍신초래 : Skill_Active
{
    public GameObject tornado;
    WaitForSeconds wait = new WaitForSeconds(1);

    private void Awake()
    {
        skill = Skill.GetList()[26];
        level = 5;
    }

    public override void init()
    {
    }

    public override void func()
    {
        StartCoroutine(coroutine());
    }

    IEnumerator coroutine()
    {
        while (true)
        {
            if (transform.childCount < GameManager.instance.Count+1)
            {
                Transform child = Instantiate(tornado).transform;
                child.parent = transform;
                child.position = transform.position;
            }
            yield return wait;
        }
    }
}