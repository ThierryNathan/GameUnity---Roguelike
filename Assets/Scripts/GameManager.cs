﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public BoardManager boardManager;

    private int level = 1;

    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;

    public float turnDelay = .1f;
    private List<Enemy> enemies;
    private bool enemiesMoving;

    public float levelStartDelay = 2f;
    private Text levelText;
    private GameObject levelImage;
    private bool doingSetup;

    private void OnLevelWasLoaded(int index)
    {
        level++;
        InitGame();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        enemies = new List<Enemy>();

        boardManager = GetComponent<BoardManager>();
        InitGame();
    }
    void InitGame()
    {
        doingSetup = true;
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Dia " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        boardManager.SetupScene(level);
        enemies.Clear();
    }

    private void HideLevelImage() 
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    IEnumerator MoveEnemies() 
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playersTurn = true;
        enemiesMoving = false;
    }

    private void Update() 
    {
        if (playersTurn || enemiesMoving || doingSetup)
        {
            return;
        }

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy enemy) 
    {
        enemies.Add(enemy);
    }

    public void GameOver() 
    {
        levelText.text = "Depois de " + level + " dias você morreu.";
        levelImage.SetActive(true);
        enabled = false;
    }
}
