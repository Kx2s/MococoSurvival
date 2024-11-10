using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float Attack {
        get
        {
            int attack = 100;
            attack += attack + 10 * (PlayerPrefs.GetInt("Weapon"));
            return attack;
        }
    }
    
    public static int Health
    {
        get
        {
            int hp = 100;
            int sum = PlayerPrefs.GetInt("Head") + PlayerPrefs.GetInt("Chest")
                + PlayerPrefs.GetInt("Leg") + PlayerPrefs.GetInt("Hand") + PlayerPrefs.GetInt("Shoulder") + 1;
            hp += 10 * sum;
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
