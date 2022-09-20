using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 3f;
    [SerializeField] private float despawnTimer = 5f;

    public Enemy_Bullet(float bulletSpeed)
    {
        this.bulletSpeed = bulletSpeed;
    }
    
    private void FixedUpdate()
    {
        despawnTimer -= Time.fixedDeltaTime;
        if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
        Move();    
    }

    private void Move()
    {
        transform.Translate(Vector2.right * (bulletSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D mayhem)
    {
        if (mayhem.CompareTag("Player"))
        {
            mayhem.GetComponent<PlayerState>().DoHarm(1);
            Destroy(gameObject);
        }
    }
}

//MAYHEM!!
