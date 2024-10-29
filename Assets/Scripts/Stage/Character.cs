using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float Damage {
        get
        {
            int damage = 100;
            damage += damage/10 * PlayerPrefs.GetInt("Weapon")+1;
            return damage;
        }
    }
    
    public static int Health
    {
        get
        {
            int hp = 100;
            int sum = PlayerPrefs.GetInt("Head") + PlayerPrefs.GetInt("Chest")
                + PlayerPrefs.GetInt("Leg") + PlayerPrefs.GetInt("Hand") + PlayerPrefs.GetInt("Foot") + 1;
            hp *= hp/10 * sum;
            return hp;
        }
    }

    public static float Speed {
        get { return GameManager.instance.playerId == 0 ? 1.1f : 1f;}
    }

    public static float WeaponSpeed {
        get { return GameManager.instance.playerId == 1 ? 1.1f : 1f;}
    }

    public static float WeaponRate {
        get { return GameManager.instance.playerId == 1 ? 0.9f : 1f;}
    }


    public static int Count {
        get { return GameManager.instance.playerId == 3 ? 1 : 0;}
    }

}
