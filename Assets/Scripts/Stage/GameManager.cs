using DataTable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    WaitForSeconds wait;
    public static GameManager instance;

    [Header("# Player Base")]
    public float baseAttack;
    public float baseHealth;
    public float baseSpeed;

    [Header("# Player Info")]
    public int kill = -1;
    public float Attack;
    [SerializeField]
    private float Hp;
    public float Shield;
    public int level;
    public int Exp;

    [SerializeField]
    [Header("# Special Ability")]
    public int Critical;
    public int Range;
    public int Slow;
    public int Reduces;
    public int Count;
    public int AttackRate;
    public float Speed;
    public float AttackSpeed;
    public float ExpBoost;
    public float GoldBoost;

    public int playerId; //삭제예정

    public Dictionary<int, int> passive;
    public Dictionary<int, int> active;

    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Control")]
    public bool isLive;
    [SerializeField]
    private float time;
    public float maxGameTime = 2 * 10f;

    [Header("# Game Object")]
    public HUD hud;
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public Transform uiJoy;
    public GameObject enemyCleaner;
    public AchiveManager achiveManager;

    public float health
    {
        get { return Hp; }
        set
        {
            Hp = value;
            hud.uiHealth();
        }
    }

    public int exp
    {
        get { return Exp; }
        set
        {
            if (!isLive)
                return;
            Exp += value;
            kill++;

            if (Exp >= nextExp[Mathf.Min(level, nextExp.Length - 1)])
            {
                Exp -= nextExp[Mathf.Min(level, nextExp.Length - 1)];
                level++;
                uiLevelUp.Show();
            }
            hud.uiHunting();
        }
    }

    public float gameTime {
        get {return time;}
        set {
            time = value;
            hud.uiTime();
        }
    }

    void Awake()
    {
        instance = this;
        wait = new WaitForSeconds(1);
        Application.targetFrameRate = 60;
        passive = new Dictionary<int, int>();
        active = new Dictionary<int, int>();
    }

    public void Start()
    {
        baseHealth = Character.Health;
        baseAttack = Character.Attack;
        baseSpeed = Character.Speed;


        playerId = 0;
        health = baseHealth;
        Attack = baseAttack;
        Speed = baseSpeed;
        exp = 0;

        for (int i=0; i<5; i++)
        {
            if (i < 3)
            {
                continue;
            }
            print(i);
        }

        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);
        StartCoroutine(TimeUpdate());
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    

    IEnumerator TimeUpdate()
    {
        while (gameTime < maxGameTime)
        {
            yield return wait;

            if (isLive)
                gameTime++;
        }
        gameTime = maxGameTime;
        GameVictory();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
        uiJoy.localScale = Vector3.zero;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
        uiJoy.localScale = Vector3.one;
    }
}
