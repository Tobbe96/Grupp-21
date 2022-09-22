using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver1 : MonoBehaviour
{

    public Questr quest;

    public Player player;

    public GameObject questWindow;

    public Text titleText;
    public Text descriptionText;


    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
    }

}
