using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour
{

    public int sceneBuildIndex;

    public bool randomIndex = true;
    public bool banSameRoom = true;
    public List<string> roomScenes = new List<string>{"Room 1","Room 2","Room 3","Room 4","Room 5"};
    
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
            if (randomIndex)
            {
                string scene = "";
                int roomNum = UnityEngine.Random.Range(0,roomScenes.Count-1);
                scene = roomScenes.ToArray()[roomNum];
                if (banSameRoom && scene == SceneManager.GetActiveScene().name){
                    while(scene == SceneManager.GetActiveScene().name)
                    {
                        roomNum = UnityEngine.Random.Range(0,roomScenes.Count-1);
                        scene = roomScenes.ToArray()[roomNum];
                    }
                }
                //Reset Enemy  Status' before new room
                GameManager.instance.enemyStatus = new Dictionary<string, bool>();
                SceneManager.LoadScene(scene);
            } else{
                SceneManager.LoadScene(sceneBuildIndex);
                //Reset Enemy  Status' before new room
                GameManager.instance.enemyStatus = new Dictionary<string, bool>();
            }
            
        }
    }
}
