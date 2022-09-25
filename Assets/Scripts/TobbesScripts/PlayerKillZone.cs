using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy01"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerState>().Respawn();
        }
    }
}
