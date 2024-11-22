using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float Attack {
        get
        {
            int attack = 100;
            attack += 50 * (PlayerPrefs.GetInt("Weapon"));
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
            hp += 50 * sum;
            return hp;
        }
    }
}
