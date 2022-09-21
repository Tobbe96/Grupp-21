using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillbox : MonoBehaviour
{
    GameObject gameObjectToKill;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;


    private void Start()
    {

        gameObjectToKill = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        col.CompareTag("Player");
        
            if (col.gameObject.GetComponent<PlayerMovement>().IsFalling() == true)
            {
                gameObject.GetComponentInParent<EnemyControl>().TakeDamage(1);
                audioSource.PlayOneShot(audioClip);
            }
    }
}
// private void onCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.TryGetComponent<EnemyControl>(out EnemyControl enemyComponent))
    //     {
    //         enemyComponent.TakeDamage(1);
    //     }
