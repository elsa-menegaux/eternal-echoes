using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private string id;
    [ContextMenu("Create GUID for ID")]
    private void generateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }


    private void Awake()
    {
        // Ensure GameManager is initialized before accessing it
        if (GameManager.instance != null)
        {
            // Check if this enemy has been fought
            if (GameManager.instance.enemyStatus.ContainsKey(id) && GameManager.instance.enemyStatus[id])
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
            GameManager.instance.enemyStatus[id] = true; // Track enemy status
            gameObject.SetActive(false); // Deactivate the enemy
        }
        else
        {
            Debug.LogError("GameManager instance is not set when marking enemy as fought!");
        }
    }
}
