using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    private ShooterManager _shooterManager;
    [SerializeField] private float fireRate = 4f;
    private float fireRateReference;
    private float timeSinceLastShot = 0f;
    [SerializeField] private GameObject[] shootPoints;
    private int index = 0;
    private bool hasShot = false;

    private void OnEnable()
    {
        ShooterEventSystem.Instance.onAttackSpeedBuff += AttackSpeedBuff;
    }

    private void OnDisable()
    {
        ShooterEventSystem.Instance.onAttackSpeedBuff -= AttackSpeedBuff;
    }

    private void Start()
    {
        _shooterManager = ShooterManager.Instance;
        fireRateReference = fireRate;
    }
    void Update()
    {
        if (_shooterManager.gameOver)
            return;

        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= 1f / fireRate)
        { 
            if (hasShot)
            {
                BulletObjectPool.Instance.Pool_Instantiate(shootPoints[index].transform.position);
                timeSinceLastShot = 0f;
                if (index >= 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
        }
       
    }

    public void OnShoot(InputAction.CallbackContext callbackContext)
    {
        hasShot = callbackContext.action.triggered;
    }

    private void AttackSpeedBuff()
    {
        fireRate = fireRateReference * 3;
        Invoke(nameof(SetDefaultAttackSpeed),10f);

    }

    private void SetDefaultAttackSpeed()
    {
        fireRate = fireRateReference;
    }
}
