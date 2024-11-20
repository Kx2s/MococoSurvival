using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 강령술 : Skill_Active
{
    private void Awake()
    {
        skill = Skill.GetList()[16];
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
            RaycastHit2D[] targets = Physics2D.CircleCastAll(transform.position, 10, Vector2.zero, 0, LayerMask.GetMask("Enemy"));
            if (targets.Length < level + GameManager.instance.Count)
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }

            for (int i = 0; i < level + GameManager.instance.Count; i++)
            {
                GameObject doll = GameManager.instance.pool.Get(8);
                doll.GetComponent<SuicideBomb>().level = level;
                doll.GetComponent<SuicideBomb>().target = targets[i].transform;
                doll.transform.position = transform.position;
            }
            yield return new WaitForSeconds(coolTime());
        }
    }
}