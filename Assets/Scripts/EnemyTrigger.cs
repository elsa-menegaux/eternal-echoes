using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTrigger : MonoBehaviour
{
    public string battleSceneName;  // Name of your battle scene

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        // Optionally change color for feedback
        //other.GetComponent<SpriteRenderer>().color = Color.red;
        StartBattle(other);
    }
}

    private void StartBattle(Collider2D other)
    {		
		PlayerManager.Instance.playerStats = other.GetComponent<PlayerStats>();
        // Load the battle scene
		
		// Make the player object invisible
        other.gameObject.SetActive(false);
		
		// Get the EnemyState component
		EnemyState enemyState = GetComponent<EnemyState>();
    
		if (enemyState != null)
		{
			// Deactivate this enemy through its state manager
			enemyState.MarkAsFought();
		}
		else
		{
			Debug.LogError("EnemyState component not found on " + gameObject.name);
			gameObject.SetActive(false); // Fallback if the component is missing
		}
		
        SceneManager.LoadScene(battleSceneName);
    }
}
