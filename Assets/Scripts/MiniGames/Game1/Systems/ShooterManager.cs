using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UtilityCode.CodeLibrary.Utilities;

public class ShooterManager : UnitySingleton<ShooterManager>
{
    [HideInInspector] public int score = 0;

    [HideInInspector] public bool gameOver = false;

    [SerializeField] private PlayerInputManager inputManager;

    private int MaxScore = 25;

    public int level = 0;

    private void OnEnable()
    {
        ShooterEventSystem.Instance.onEnemyKilled += IncreaseScore;
        ShooterEventSystem.Instance.onGameLost += OnGameLost;
        ShooterEventSystem.Instance.onNextLevel += LevelUp;
    }

    private void OnDisable()
    {
        ShooterEventSystem.Instance.onEnemyKilled -= IncreaseScore;
        ShooterEventSystem.Instance.onGameLost -= OnGameLost;
        ShooterEventSystem.Instance.onNextLevel -= LevelUp;
    }

    private void IncreaseScore()
    {
        score++;
        CheckGameWonStatus();
    }

    private void LevelUp()
    {
        gameOver = false;
        MaxScore += 25;
        level++;
    }

    private void CheckGameWonStatus()
    {
        if (score < MaxScore) return;
        gameOver = true;
        ShooterEventSystem.Instance.onGameWon?.Invoke();
    }

    private void OnGameLost()
    {
        gameOver = true;
    }

    public void StartTwoPlayerGame()
    {
        var player1 = PlayerInput.Instantiate(inputManager.playerPrefab, controlScheme: "Player1", pairWithDevice: Keyboard.current);
        var player2 = PlayerInput.Instantiate(inputManager.playerPrefab, controlScheme: "keyboard2", pairWithDevice: Keyboard.current);
        player2.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        player2.transform.position+=new Vector3(1.5f,0,0);
    }

    public void StartOnePlayerGame()
    {
        var player1 = PlayerInput.Instantiate(inputManager.playerPrefab, controlScheme: "Player1", pairWithDevice: Keyboard.current);
        //var player2 = PlayerInput.Instantiate(inputManager.playerPrefab, controlScheme: "keyboard2", pairWithDevice: Keyboard.current);
    }
}
