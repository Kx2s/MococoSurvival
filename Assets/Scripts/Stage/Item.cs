using EnumManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemT type;
    WaitForSeconds wait = new WaitForSeconds(.5f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        //에테르 포식자 추가예정


        switch (type)
        {
            case ItemT.Bomb:
                Bomb(); break;
            case ItemT.Gold:
                Gold(); break;
            case ItemT.Heal:
                Healing(); break;
        }
        gameObject.SetActive(false);
    }

    public void Healing()
    {
        GameManager.instance.addhealth(GameManager.instance.baseHealth / 10);
    }

    public void Bomb()
    {
        //파폭 사운드

        GameManager.instance.Clean();
    }

    public void Gold()
    {
        print(GameManager.instance.stage + 1);
        GameManager.instance.addGold(100 * (GameManager.instance.stage+1));
    }
}
