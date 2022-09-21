using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_State : MonoBehaviour
{
    [Header("Health")] [SerializeField] private float health, maxHealth = 3f;
public void start()
{
    health = maxHealth;

}
public void TakeDamage(float damageAmount)
{
    health -= damageAmount; // 3 -> 2 -> 1 -> 0 = enemy ded
    if (health <= 0)
    {
        GetComponent<EnemyControl>().KillMe();
    }
}

}

//WARNING! Remove from final product

//enemy will abandon its routine when detecting player
//when player is detected it will start an attack routine until player leaves attack range