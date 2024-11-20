using DataTable;
using System.Collections;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    int scale = 0;
    Rigidbody2D rigid;
    WaitForSeconds wait = new WaitForSeconds(1);

    private void Awake()
    {
        rigid = GetComponentInChildren<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(coroutine());
    }

    IEnumerator coroutine()
    {
        Vector3 dir = Random.insideUnitCircle.normalized;
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        while (true)
        {
            Vector2 pos = Camera.main.WorldToViewportPoint(transform.position);

            if (scale != GameManager.instance.Range)
            {
                scale = GameManager.instance.Range;
                Transform parent = transform.parent;
                transform.parent = null;
                transform.localScale = Vector3.one * (1 + 0.5f * scale);
                transform.parent = parent;
            }

            if (pos.x <= 0f && dir.x < 0)
                dir.x = 1;

            if (pos.y <= 0f && dir.y < 0)
                dir.y = 1;

            if (pos.x >= 1f && dir.x > 0)
                dir.x = -1f;

            if (pos.y >= 1f && dir.y > 0)
                dir.y = -1f;
            dir = dir.normalized;

            rigid.MovePosition(transform.position + dir * 0.2f);
            yield return wait;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
            return;

        collision.GetComponent<Enemy>().OnDamage(Skill.GetList()[26].sk_bagic);
    }
}
