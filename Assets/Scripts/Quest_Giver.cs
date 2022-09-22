using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Quest_Giver : MonoBehaviour
{
    [SerializeField] private GameObject doorToOpenWhenQuestIsComplete;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    [SerializeField] private int amountToCollect = 1;
    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.PlayOneShot(audioClip);
        {

            if (collision.CompareTag("Player") == true)
            {
                if (collision.GetComponent<PlayerState>().coinAmount >= amountToCollect)
                {
                    collision.GetComponent<PlayerState>().isQuestComplete = true;
                    doorToOpenWhenQuestIsComplete.SetActive(false);
                }
            }
        }
    }
}
