using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float spawnInterval = 1f;
    private ShooterManager _shooterManager;
    [SerializeField] private GameObject rapidFireBuff;

    private void OnEnable()
    {
        ShooterEventSystem.Instance.onGameWon += OnGameOver;
        ShooterEventSystem.Instance.onGameLost += OnGameOver;
        ShooterEventSystem.Instance.onGameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        ShooterEventSystem.Instance.onGameWon -= OnGameOver;
        ShooterEventSystem.Instance.onGameLost -= OnGameOver;
        ShooterEventSystem.Instance.onGameStarted -= OnGameStarted;
    }
    
    public void OnGameStarted()
    {
        _shooterManager = ShooterManager.Instance;
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        int count = 0;
        while (true)
        {
            while (!_shooterManager.gameOver)
            {
                float xCoordinate = Random.Range(minX, maxX);
                Vector3 randomSpawnPos = new Vector3(xCoordinate, transform.position.y, transform.position.z);
                EnemyPool.Instance.Pool_Instantiate(randomSpawnPos);
                if (count == 12)
                {
                    Instantiate(rapidFireBuff).transform.position = new Vector3(xCoordinate, transform.position.y+2f, transform.position.z);
                    count = 0;
                }
                yield return new WaitForSeconds(spawnInterval);
                count++;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void OnGameOver()
    {
        
    }
}
