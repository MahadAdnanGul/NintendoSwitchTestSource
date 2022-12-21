using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Vector3 originalScale;
    [SerializeField] private AudioClip explodeSound;
    private AudioSource src;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        src = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        ShooterEventSystem.Instance.onGameWon += SelfDestruct;
        ShooterEventSystem.Instance.onGameLost += SelfDestruct;
        rb.velocity = new Vector2(0, -speed - (ShooterManager.Instance.level * 0.25f));
    }

    private void OnDisable()
    {
        ShooterEventSystem.Instance.onGameWon -= SelfDestruct;
        ShooterEventSystem.Instance.onGameLost -= SelfDestruct;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        src.PlayOneShot(explodeSound);
        ShooterEventSystem.Instance.onAttackSpeedBuff?.Invoke();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Invoke(nameof(SelfDestruct),1f);
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
    
}
