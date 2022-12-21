using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5f;
    private Rigidbody2D rb;
    [SerializeField] private AudioClip shootSound;
    private AudioSource src;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        src = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        rb.AddForce(new Vector2(0,bulletSpeed),ForceMode2D.Impulse);
        src.PlayOneShot(shootSound);
        Invoke(nameof(SelfDestruct),0.75f*(10f/bulletSpeed));
    }

    private void SelfDestruct()
    {
        BulletObjectPool.Instance.Pool_Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        ShooterEventSystem.Instance.onEnemyKilled?.Invoke();
        CancelInvoke();
        SelfDestruct();
    }
}
