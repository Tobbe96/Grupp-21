using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndText : MonoBehaviour
{
    public bool hasWon = false;
    

    public GameObject winText;

    private void Update()
    {
        if (hasWon)
        {
            QuitRestart();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerState>().isQuestComplete)
            { 
                ShowText(); 
                hasWon = true;
            }

        }
    }
    
    void ShowText()
    {
        winText.SetActive(true);
    }
    void QuitRestart()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Om vi kör en vanlig build av spelet
            SceneManager.LoadScene(0);
        }

        // Man vill starta om spelet
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }

}


