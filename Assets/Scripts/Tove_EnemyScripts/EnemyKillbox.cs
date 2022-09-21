using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillbox : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;


    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<PlayerMovement>().IsFalling())
            {
                
                gameObject.GetComponentInParent<Enemy_State>().TakeDamage(1);
                audioSource.PlayOneShot(audioClip);
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f);
            }
    }
}
// private void onCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.TryGetComponent<EnemyControl>(out EnemyControl enemyComponent))
    //     {
    //         enemyComponent.TakeDamage(1);
    //     }
