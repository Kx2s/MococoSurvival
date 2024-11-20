using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 저주받은인형 : Skill_Active
{
    private void Awake()
    {
        skill = Skill.GetList()[23];
    }

    public override void init()
    {

        level = 5;
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
            if (targets.Length < level)
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }

            for (int i = 0; i < level; i++)
            {
                GameObject doll = GameManager.instance.pool.Get(8);
                Color color;
                ColorUtility.TryParseHtmlString("#190606", out color);
                doll.GetComponentInChildren<SpriteRenderer>().color = color;
                doll.GetComponent<SuicideBomb>().level = 10;
                doll.GetComponent<SuicideBomb>().target = targets[i].transform;
                doll.transform.position = transform.position;
            }
            yield return new WaitForSeconds(coolTime());
        }
    }
}