using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class ChestNPC : MonoBehaviour
{
    public GameObject Panel;
    public Text dialougeText;
    public string[] dialouge;
    public int index;

    public float wordSpeed;
    public bool playerIsClose;

    public GameObject nextButton;


    void OnTriggerStay2D(Collider2D col)
    {
        {
            if (Input.GetKeyDown(KeyCode.E) && playerIsClose && col.GetComponent<PlayerState>().isQuestComplete)
            {
                if (Panel.activeInHierarchy)
                {
                    zeroText();
                }
                else
                {
                    Panel.SetActive(true);
                    StartCoroutine(Typing());
                }
            }

            if (dialougeText.text == dialouge[index])
            {
                nextButton.SetActive(true);
            }
        }
    }

        public void zeroText()
        {
            dialougeText.text = "";
            index = 0;
            Panel.SetActive(false);
        }

        IEnumerator Typing()
        {
            foreach (char letter in dialouge[index].ToCharArray())
            {
                dialougeText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }

        public void NextLine()
        {

            nextButton.SetActive(false);
            if (index < dialouge.Length - 1)
            {
                index++;
                dialougeText.text = "";
                StartCoroutine(Typing());
            }
            else
            {
                zeroText();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsClose = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsClose = false;

                zeroText();
            }
        }
    }




