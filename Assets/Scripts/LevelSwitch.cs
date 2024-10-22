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

    // List of available rooms
    public List<string> roomScenes = new List<string>{"Room 1","Room 2","Room 3","Room 4","Room 5"};
    
    // If Exit zone is entered and collider is a player
    // Scene changes to another room without needing input

    private void OnTriggerEnter2D(Collider2D entity)
    {
        Debug.Log("Exit triggered");

        // If it is indeed a player that collided, then switch levels
        if (entity.CompareTag("Player"))
        {
            Debug.Log("Player detected");

            // Depending on how many rooms have been visited, send player to winning screen
            GameManager.instance.RoomVisited();
            Debug.Log("Rooms Visited +1");
            if (GameManager.instance.roomsVisited >= 6)
            {
                Debug.Log("Game Won, loading Winning screen.");
                GameManager.instance.gameWon = true;
                SceneManager.LoadScene("Winning Screen");
                return;
            }

            // Randomly select one of the 4 rooms left 
            SceneManager.LoadScene(DetermineNextScene());
        }

    }
    public string DetermineNextScene() {
        if (randomIndex)
            {
                string scene = "";
                int roomNum = UnityEngine.Random.Range(0,roomScenes.Count);
                scene = roomScenes.ToArray()[roomNum];
                if (banSameRoom && scene == SceneManager.GetActiveScene().name){
                    while(scene == SceneManager.GetActiveScene().name)
                    {
                        roomNum = UnityEngine.Random.Range(0,roomScenes.Count-1);
                        scene = roomScenes.ToArray()[roomNum];
                    }
                }
                //Reset Enemy  Status' before new room

                if (GameManager.instance != null) {
                    GameManager.instance.enemyStatus = new Dictionary<string, bool>();
                } 
                
				        GameData.roomCount++;
				        Debug.Log("RoomCount incremented to "+GameData.roomCount);
                
                return scene;
            } 

        else {
            
                //Reset Enemy  Status' before new room
                
                if (GameManager.instance != null) {
                    GameManager.instance.enemyStatus = new Dictionary<string, bool>();
                } 
                return SceneUtility.GetScenePathByBuildIndex(sceneBuildIndex);
            }
    } 
}
