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
        foreach(Monster m in Monster.GetList())
            if((int)m.tema == PlayerPrefs.GetInt("Stage")/3)
                monsterData.Add(m);
        levelTime = GameManager.instance.maxGameTime / monsterData.Count;

        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {

        while (true)
        {
            yield return !GameManager.instance.isLive;
            Spawn();
            level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), monsterData.Count - 1);
            yield return new WaitForSeconds(monsterData[level].spawnTime);
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(monsterData[level]);
    }
}