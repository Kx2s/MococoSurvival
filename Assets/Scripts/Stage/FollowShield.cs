using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShield : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void UpdateTransform(float hp)
    {
        rect.offsetMin = new Vector2(hp * 10, 0);
        rect.offsetMax = new Vector2(hp * 10, 0);
    }
}
