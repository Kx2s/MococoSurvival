using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 실페리온의도움 : Skill_Active
{
    private void Awake()
    {
        skill = Skill.GetList()[21];
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
        Transform child = transform.GetChild(0);
        child.parent = null;
        child.GetComponent<Trigg>().damage = skill.sk_bagic;
        while (true)
        {
            Vector3 dir = Random.insideUnitCircle.normalized;
            float ran = Random.Range(0f, 3f);

            child.position = dir * ran;
            child.localScale = Vector3.one * 0.5f * (GameManager.instance.Range + 2);

            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            child.gameObject.SetActive(false);

            yield return new WaitForSeconds(coolTime());
        }
    }
}