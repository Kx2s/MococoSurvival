using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigg : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
            return;

        collision.GetComponent<Enemy>().OnDamage(damage);
    }
}