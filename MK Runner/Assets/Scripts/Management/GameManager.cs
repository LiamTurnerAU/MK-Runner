﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    [HideInInspector]
    public float currentGameSpeed;
    [SerializeField]
    private float startingGameSpeed
        ,maxGameSpeed;
    private int score;
    private bool playerIsAlive = true;
    
    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        GameEvents.OnPlayerDeath += PlayerDeath;
        GameEvents.OnSpeedIncrease += SpeedIncrease;
        GameEvents.OnGameStart += OnGameStart;
        GameEvents.OnMaxSpeedChanged += MaxSpeedChanged;
    }

    private void OnGameStart()
    {
        currentGameSpeed = startingGameSpeed;
        playerIsAlive = true;
        score = 0;
        GameEvents.InvokeScoreChange(score);
        StartCoroutine(RunSession());
    }

    IEnumerator RunSession()
    {
        while (playerIsAlive)
        {
            score += 1;
            GameEvents.InvokeScoreChange(score);
            yield return new WaitForSeconds(1/currentGameSpeed);
        }
        yield return new WaitForSeconds(3f);
        GameEvents.InvokeGameOver();
    }

    private void SpeedIncrease()
    {
        currentGameSpeed+=0.5f;
        if (currentGameSpeed > maxGameSpeed)
        {
            currentGameSpeed = maxGameSpeed;
            GameEvents.InvokeOnInfoText("MAX SPEED!");
        }
        else
        {
            GameEvents.InvokeOnInfoText("Speed Increased!");
        }
    }

    private void MaxSpeedChanged(int value)
    {
        maxGameSpeed += value;
    }

    private void PlayerDeath()
    {
        playerIsAlive = false;
        currentGameSpeed = 0;
    }
}
