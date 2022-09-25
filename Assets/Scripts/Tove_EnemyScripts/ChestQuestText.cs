using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestQuestText : MonoBehaviour

{
    /// <summary>
    /// Modified NPC script - Tove
    /// </summary>
    [SerializeField] private GameObject turnInQuestText;
    private bool hasInteracted = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!hasInteracted)
            turnInQuestText.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (!other.GetComponent<PlayerState>().isQuestComplete) return;
        
        if (Input.GetKeyDown(KeyCode.E))
        { 
            turnInQuestText.SetActive(false);
            hasInteracted = true;   
        } 
        
    }
}

//visa text bara om spelaren är nära (vid collidern)
//Kolla att spelaren har gjort klart quest
//kolla att spelaren trycker på E knapp
//Ta bort text vid E tryckning
