using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShooterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameStatusText;
    [SerializeField] private Color gameWonColor;
    [SerializeField] private Color gameLostColor;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject startButtons;
    private void OnEnable()
    {
        ShooterEventSystem.Instance.onEnemyKilled += IncreaseScore;
        ShooterEventSystem.Instance.onGameWon += OnGameWon;
        ShooterEventSystem.Instance.onGameLost += OnGameLost;
    }

    private void OnDisable()
    {
        ShooterEventSystem.Instance.onEnemyKilled -= IncreaseScore;
        ShooterEventSystem.Instance.onGameWon -= OnGameWon;
        ShooterEventSystem.Instance.onGameLost -= OnGameLost;
    }

    private void IncreaseScore()
    {
        int score = int.Parse(scoreText.text);
        score++;
        scoreText.text = score.ToString();
    }

    private void OnGameWon()
    {
        gameStatusText.text = "Game Cleared!";
        gameStatusText.color = gameWonColor;
        gameStatusText.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
    }
    private void OnGameLost()
    {
        gameStatusText.text = "Game Over!";
        gameStatusText.color = gameLostColor;
        gameStatusText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        gameStatusText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        ShooterEventSystem.Instance.onNextLevel?.Invoke();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void StartOnePlayer()
    {
        startButtons.SetActive(false);
        ShooterEventSystem.Instance.onGameStarted?.Invoke();
        ShooterManager.Instance.StartOnePlayerGame();
    }

    public void StartTwoPlayer()
    {
        startButtons.SetActive(false);
        ShooterEventSystem.Instance.onGameStarted?.Invoke();
        ShooterManager.Instance.StartTwoPlayerGame();
    }
}
