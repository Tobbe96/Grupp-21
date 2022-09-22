using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : MonoBehaviour
{
    GameObject text;

    private bool removeGameObject = false;



    private void Update()
    {
        if (removeGameObject == true) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            Destroy(gameObject);
        }
    }










}
