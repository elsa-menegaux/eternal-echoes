using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    private void Awake()
    {
        // Check if this enemy has been fought
        if (GameManager.instance.enemyStatus.ContainsKey(gameObject.name) && GameManager.instance.enemyStatus[gameObject.name])
        {
            gameObject.SetActive(false); // Deactivate if already fought
        }
    }

    public void MarkAsFought()
    {
        GameManager.instance.enemyStatus[gameObject.name] = true; // Track enemy status
        gameObject.SetActive(false); // Deactivate the enemy
    }
}
