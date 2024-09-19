using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    private void Awake()
    {
        // Ensure GameManager is initialized before accessing it
        if (GameManager.instance != null)
        {
            // Check if this enemy has been fought
            if (GameManager.instance.enemyStatus.ContainsKey(gameObject.name) && GameManager.instance.enemyStatus[gameObject.name])
            {
                gameObject.SetActive(false); // Deactivate if already fought
            }
        }
        else
        {
            Debug.Log("GameManager instance is not set!");
        }
    }

    public void MarkAsFought()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.enemyStatus[gameObject.name] = true; // Track enemy status
            gameObject.SetActive(false); // Deactivate the enemy
        }
        else
        {
            Debug.LogError("GameManager instance is not set when marking enemy as fought!");
        }
    }
}
