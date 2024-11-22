using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{
    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
