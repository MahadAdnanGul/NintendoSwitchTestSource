using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Vector3 originalScale;
    [SerializeField] private float scaleMultiplier = 1.2f;
    [SerializeField] private float time = 0.25f;
    [SerializeField] private GameObject explosionEffect;
    
    [SerializeField] private AudioClip explodeSound;
    private AudioSource src;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        src = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        ShooterEventSystem.Instance.onGameWon += SelfDestruct;
        ShooterEventSystem.Instance.onGameLost += SelfDestruct;
        rb.velocity = new Vector2(0, -speed - (ShooterManager.Instance.level * 0.25f));
        //rb.AddForce(new Vector2(0,-speed-(ShooterManager.Instance.level*0.05f)),ForceMode2D.Impulse);
        StartCoroutine(Animation());
    }

    private void OnDisable()
    {
        ShooterEventSystem.Instance.onGameWon -= SelfDestruct;
        ShooterEventSystem.Instance.onGameLost -= SelfDestruct;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        src.PlayOneShot(explodeSound);
        explosionEffect.SetActive(true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        CancelInvoke(nameof(SelfDestruct));
        Invoke(nameof(SelfDestruct),0.5f);
    }
    

    private void SelfDestruct()
    {
        EnemyPool.Instance.Pool_Destroy(gameObject);
        explosionEffect.SetActive(false);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private IEnumerator Animation()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            if (transform.localScale == originalScale)
            {
                transform.localScale *= scaleMultiplier;
            }
            else
            {
                transform.localScale = originalScale;
            }
            
            
        }

    }
}
