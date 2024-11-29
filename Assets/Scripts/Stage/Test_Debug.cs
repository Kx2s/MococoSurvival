using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Debug : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string tmp = "";
        tmp += GameManager.instance.isLive;
        tmp += "\n";
        tmp += GameManager.instance.player.GetComponent<Collider2D>().enabled;

        text.text = tmp;
    }
}
