using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (int i=0; i<prefabs.Length; i++)
            pools[i] = new List<GameObject>();
    }

    public GameObject Get(int idx)
    {
        int cnt = 0;
        foreach (GameObject item in pools[idx])
            if (!item.activeSelf) {
                cnt++;
                item.SetActive(true);
                return item;
            }
        if (cnt == 500)
            return null;
        
        GameObject select = Instantiate(prefabs[idx], transform);
        pools[idx].Add(select);
        return select;
    }

}
