using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTrigger : MonoBehaviour
{
    public string battleSceneName;  // Name of your battle scene
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
		
        // Get the EnemyState component
        EnemyStats enemyStats = GetComponent<EnemyStats>();
        Debug.Log("EnemyStats found on " + gameObject.name);
        
        if (enemyStats != null)
        {
            GameDataHolder.Instance.enemyStats = enemyStats;
            Debug.Log("EnemyStats assigned from " + gameObject.name + " to GameDataHolder");
			GameDataHolder.Instance.enemyname = gameObject.name;
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
		
		GameDataHolder.Instance.previousSceneName = currentSceneName; 
		
        // Load the battle scene
        SceneManager.LoadScene(battleSceneName);
    }
}