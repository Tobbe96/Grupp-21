using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    public int healthPoints = 2;
    public int initialHealthPoints = 2;

    public int coinAmount = 0;
    public GameObject respawnPosition;

    [SerializeField] private GameObject startPosition;
    [SerializeField] private bool useStartPosition = true;
    public bool isQuestComplete;
    
    void Start()
    {
        healthPoints = initialHealthPoints;
        if (useStartPosition == true) 
        { 
            gameObject.transform.position = startPosition.transform.position;
        }
        respawnPosition = startPosition;
    }
    public void DoHarm(int doHarmByThisMuch)
    {
        healthPoints -= doHarmByThisMuch;
        if (healthPoints <= 0)
        {
            Respawn();
        }
    }
    private void Respawn()
    {
        healthPoints = initialHealthPoints;
        gameObject.transform.position = respawnPosition.transform.position;
    }

    public void CoinPickUp()
    {
        coinAmount++;
    }

    public void ChangeRespawnPosition(GameObject newRespawnPosition)
    {
        respawnPosition = newRespawnPosition;
    }
    
}
