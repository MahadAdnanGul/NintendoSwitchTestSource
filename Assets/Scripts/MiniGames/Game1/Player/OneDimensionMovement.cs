using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class OneDimensionMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D rb;
    private ShooterManager _shooterManager;
    private float movementVal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _shooterManager = ShooterManager.Instance;
    }

    void Update()
    {
        if (_shooterManager.gameOver)
        {
            rb.velocity=Vector2.zero;
            return;
        }

        float inputHorizontal = movementVal;
        rb.velocity=new Vector2(inputHorizontal*(speed+_shooterManager.level),0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Bounds")&&!other.gameObject.CompareTag("Player")) 
            ShooterEventSystem.Instance.onGameLost?.Invoke();
        
    }
    

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        movementVal = callbackContext.ReadValue<float>();
    }
}
