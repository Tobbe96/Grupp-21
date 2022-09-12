using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Quest_Giver : MonoBehaviour
{
    [SerializeField] private GameObject questGiverText;

   [SerializeField] private TextMeshProUGUI textComponent;
   
   [SerializeField] private string questBeginText;
   [SerializeField] private string questCompletionText;
   [SerializeField] private GameObject doorToOpenWhenQuestIsComplete;
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private AudioClip audioClip;
   
   [SerializeField] private int amountToCollect = 1;
    // Start is called before the first frame update
    void Start()
    {
        questGiverText.SetActive(false);
        textComponent.text = questBeginText;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
                audioSource.PlayOneShot(audioClip);
                {
                    if (collision.CompareTag("Player") == true) 
                    { 
                        if (collision.GetComponent<PlayerState>().coinAmount >= amountToCollect) 
                        { 
                            textComponent.text = questCompletionText; 
                            collision.GetComponent<PlayerState>().isQuestComplete = true; 
                            doorToOpenWhenQuestIsComplete.SetActive(false); 
                        } 
                        questGiverText.SetActive(true); 
                    }
                    else 
                    { 
                        textComponent.text = questBeginText; 
                    } 
                } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            questGiverText.SetActive(false);
        }
    }
}
