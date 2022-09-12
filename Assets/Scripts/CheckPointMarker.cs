using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMarker : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            collision.GetComponent<PlayerState>().ChangeRespawnPosition(gameObject);
            animator.SetTrigger("IsTaken");
        }
    }
}

//is the flag is not the current respawn position it will stop animating
