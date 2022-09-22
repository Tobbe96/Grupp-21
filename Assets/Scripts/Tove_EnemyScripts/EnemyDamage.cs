using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float _pushForce = 100f;
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerMovement>().IsFalling())
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _pushForce, ForceMode2D.Impulse);
                return;
            }
            collision.gameObject.GetComponent<PlayerState>().DoHarm(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                (collision.gameObject.transform.position.x - transform.position.x) * _pushForce * 5, 0), ForceMode2D.Impulse);
            GetComponent<EnemyControl>().StartPauseChasing(.5f);
            
            audioSource.PlayOneShot(audioClip);
        }

    }


}

