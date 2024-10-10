using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTrigger : MonoBehaviour
{
    private string battleSceneName = "BattleScene";  // Name of your battle scene
    private string currentSceneName; // Store the current scene name

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartBattle(other);
        }
    }

    private void StartBattle(Collider2D other)
    {		
        // Store the current scene name
        currentSceneName = SceneManager.GetActiveScene().name;

        PlayerManager.Instance.playerStats = other.GetComponent<PlayerStats>();
        
        // Make the player object invisible
        other.gameObject.SetActive(false);
		
        // Get the EnemyStats component
        EnemyStats enemyStats = GetComponent<EnemyStats>();
        if (enemyStats != null)
        {
            GameData.EnemyStats = enemyStats; // Assign the enemy stats
            GameData.EnemyName = gameObject.name; // Store the enemy name
            Debug.Log("EnemyStats assigned from " + gameObject.name + " to GameData");
        }
        else
        {
            Debug.LogError("EnemyStats component not found on " + gameObject.name);
            return; // Exit if enemyStats is not found
        }
    
        EnemyState enemyState = GetComponent<EnemyState>();
        if (enemyState != null)
        {
            enemyState.MarkAsFought();
        }
        else
        {
            Debug.LogError("EnemyState component not found on " + gameObject.name);
            gameObject.SetActive(false);
        }
		
        GameData.PreviousSceneName = currentSceneName; 
		
        // Load the battle scene
        SceneManager.LoadScene(battleSceneName);
    }
}