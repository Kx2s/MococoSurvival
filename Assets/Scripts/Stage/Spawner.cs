using DataTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    int level;
    float timer;
    List<Monster> monsterData;

    public float levelTime;
    public Transform[] spawnPoint;

    void Awake()
    {
        monsterData = new List<Monster>();
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {

        while (true)
        {
            yield return !GameManager.instance.isLive;
            Spawn();
            yield return new WaitForSeconds(Mathf.Max(0.001f, .5f - 0.02f * GameManager.instance.gameTime/10));
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}