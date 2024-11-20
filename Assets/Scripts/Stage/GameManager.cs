using DataTable;
using EnumManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    WaitForSeconds wait;
    public static GameManager instance;

    [Header("# Player Base")]
    public float baseAttack;
    public float baseHealth;
    public float baseSpeed;
    public int cnt = 1;

    [Header("# Player Info")]
    public int kill = -1;
    public float Attack;
    public float health;
    public float Shield;
    public float Speed;
    public int Gold;
    public int level;
    public float Exp;
    public int nextExp = 3;

    [SerializeField]
    [Header("# Special Ability")]
    public int Critical;
    public int Range;
    public int Slow;
    public int Reduces;
    public int Count;
    [SerializeField]
    private int AttackRate;
    private int SpeedRate;
    public float AttackSpeed;
    public float ExpBoost;
    public float GoldBoost;

    public int playerId; //��������

    public Dictionary<int, int> passive;
    public Dictionary<int, int> active;


    [Header("# Game Control")]
    public bool isLive;
    public int stage;
    [SerializeField]
    private float time;
    public float maxGameTime = 2 * 10f;

    [Header("# Game Object")]
    public GameObject enemyCleaner;
    public GameObject pause;
    public HUD hud;
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public Transform uiJoy;
    public AchiveManager achiveManager;
    public float exp
    {
        get { return Exp; }
        set
        {
            if (!isLive)
                return;
            Exp += value;
            kill++;

            if (Exp >= nextExp)
            {
                Exp -= nextExp;
                nextExp += cnt++;
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

    public void addhealth(float value)
    {
        health += value;
        health = Math.Min(health, baseHealth);
        hud.uiHealth();
    }

    public void subHealth(float value)
    {
        value = Shield - value;
        Shield = Math.Max(0, value);

        if (value < 0)
            health += value;
        hud.uiHealth();
    }


    public void addGold(float value)
    {
        Gold += (int)(value * (GoldBoost+100) / 100);
        hud.uigold();
    }

    public void AttackRateUp(int value)
    {
        AttackRate += value;
        Attack = baseAttack * (100 + AttackRate) / 100;
    }

    public void SpeedRateUp(int value)
    {
        SpeedRate += value;
        Speed = baseSpeed * (100 + SpeedRate) / 100;
    }

    public void ShieldReset(float value)
    {
        Shield = value;
        hud.uiHealth();
    }

    void Awake()
    {
        instance = this;
        wait = new WaitForSeconds(1);
        Application.targetFrameRate = 60;
        passive = new Dictionary<int, int>();
        active = new Dictionary<int, int>();
        stage = PlayerPrefs.GetInt("Stage");
    }

    public void Start()
    {
        pause.SetActive(true);
        pause.SetActive(false);
        baseHealth = Character.Health;
        baseAttack = Character.Attack;
        baseSpeed = 3;


        playerId = 0;
        health = baseHealth;
        Attack = baseAttack;
        Speed = baseSpeed;
        exp = 0;

        player.gameObject.SetActive(true);
        SkillContainer.container.GetChild(25).gameObject.SetActive(true);
        StartCoroutine(TimeUpdate());
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(Sfx.Select);
    }

    private void OnDisable()
    {
        int total = PlayerPrefs.GetInt("Gold") + Gold;
        PlayerPrefs.SetInt("Gold", total);
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
        player.DeadAnim();
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
        AudioManager.instance.PlaySfx(Sfx.Dead);
    }

    public void GameVictory()
    {
        Gold += 10000 * (stage + 1);
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        Clean();
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(Sfx.Win);
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

    public void Pause()
    {
        if (!isLive)
            return;
        Stop();
        pause.SetActive(true);
    }

    public void Clean()
    {
        StartCoroutine(cleanerCoroutine());
    }

    IEnumerator cleanerCoroutine()
    {
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        enemyCleaner.SetActive(false);
    }
}
