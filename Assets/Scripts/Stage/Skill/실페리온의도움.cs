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
        Transform effects = transform.GetChild(0);
        GameObject particle = effects.GetChild(0).gameObject;
        effects.parent = null;
        particle.GetComponent<Trigg>().damage = skill.sk_bagic;
        while (true)
        {
            if (effects.childCount - 1 < GameManager.instance.Count)
            {
                GameObject g = Instantiate(particle, effects);
                g.GetComponent<Trigg>().damage = skill.sk_bagic;
            }

            for (int i = 0; i < effects.childCount; i++)
            {
                Vector3 dir = Random.insideUnitCircle.normalized;
                float ran = Random.Range(0f, 4f);

                Transform child = effects.GetChild(i);
                child.position = dir * ran;
                child.parent = null;
                child.localScale = Vector3.one * 0.5f * (GameManager.instance.Range + 2);
                child.parent = effects;
            }

            effects.position = transform.position;
            effects.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            effects.gameObject.SetActive(false);

            yield return new WaitForSeconds(coolTime());
        }
    }
}