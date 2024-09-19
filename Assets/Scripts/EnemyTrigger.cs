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
		enemyRespawnTest = true;
		
		PlayerManager.Instance.playerStats = other.GetComponent<PlayerStats>();
        // Load the battle scene
		
		// Make the player object invisible
        other.gameObject.SetActive(false);
		
		// Make the enemy object inactive
        gameObject.SetActive(false); // This deactivates the enemy GameObject
		
        SceneManager.LoadScene(battleSceneName);
    }
}
