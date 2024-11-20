using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillContainer : MonoBehaviour
{
    public static Transform container;

    private void Awake()
    {
        container = transform;
    }
}