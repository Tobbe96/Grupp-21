using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest_DoorToNextLvl : MonoBehaviour
{
    [SerializeField] private int levelToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            if (collision.GetComponent<PlayerState>().isQuestComplete == true)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }
}