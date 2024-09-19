using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitch : MonoBehaviour
{

    public int sceneBuildIndex;
    
    // If Exit zone is entered and collider is a player
    // Scene changes to another room without needing input

    private void OnTriggerEnter2D(Collider2D other)
    {
        // For testing purpose, check the trigger works
        print("Exit triggered");

        // If it is indeed a player that collided, then switch levels
        if (other.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
