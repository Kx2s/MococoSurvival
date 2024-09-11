using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]

    public static GameManager instance;
    WaitForSeconds wait;

    [SerializeField]
    [Header("# Player Info")]
    private float Hp;
    public float maxHealth = 100;
    public int kill = -1;
    public int playerId;
    public int level;
    public int Exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Control")]
    public bool isLive;
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
        UGS.UnityGoogleSheet.LoadAllData();
    }

    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;
        exp = 0;

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
