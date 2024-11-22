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

        GameManager.instance.audioManager.PlaySfx(Sfx.Gain);
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
        //ÆÄÆø »ç¿îµå

        GameManager.instance.Clean();
    }

    public void Gold()
    {
        GameManager.instance.addGold(100 * (GameManager.instance.stage+1));
    }
}
