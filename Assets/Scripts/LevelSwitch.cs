using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitch : MonoBehaviour
{

    public int sceneBuildIndex;
    
    // If Exit zone is entered and collider is a player
    // Scene changes to another room without needing input

    private void OnTriggerEnter2D(Collider2D entity)
    {
        // For testing purpose, check the trigger works
        Debug.Log("Exit triggered");

        // If it is indeed a player that collided, then switch levels
        if (entity.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
