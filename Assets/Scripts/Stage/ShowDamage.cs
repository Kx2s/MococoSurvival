using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDamage : MonoBehaviour
{

    public GameObject damageObject;
    public void Hit(float damage, bool isCritical)
    {
        GameObject g = Get();
        string tmp = "<color=";
        if (isCritical)
            tmp += "#FFDF24>";
        else
            tmp += "#FFFFFF>";

        tmp += damage + "</color>";
        g.GetComponent<Text>().text = tmp;
    }


    GameObject Get()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject g = transform.GetChild(i).gameObject;
            if (!g.activeSelf)
            {
                g.SetActive(true);
                return g;
            }
        }

        GameObject select = Instantiate(damageObject, transform);
        return select;
    }
}
