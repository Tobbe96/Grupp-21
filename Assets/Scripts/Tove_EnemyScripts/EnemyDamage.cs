using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(KnockbackCooldown());
            collision.gameObject.GetComponent<PlayerState>().DoHarm(damage);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);
            audioSource.PlayOneShot(audioClip);

        }

    }
    IEnumerator KnockbackCooldown()
    {
        return(null);
        //  Rigidbody2D.velocity
    }

}

