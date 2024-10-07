using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour, IDataPersistence
{
    public static PlayerManager Instance;

    //public PlayerBattleHUD OverworldHUD;

    public GameObject playerObject;
    [HideInInspector] public PlayerStats playerStats;

    private void Awake()
    {
        // Ensure there's only one instance of PlayerManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("PlayerManager instantiated.");
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    private void Start()
    {
        // Assign PlayerStats from the existing player GameObject
        playerStats = playerObject.GetComponent<PlayerStats>();

        // Automatically find and assign PlayerBattleHUD
        //OverworldHUD = FindObjectOfType<PlayerBattleHUD>();
        //if (OverworldHUD != null)
        //{
        //    Debug.Log("OverworldHUD found and assigned.");
        //    OverworldHUD.SetHUD(playerStats);
        //}
        //else
        //{
        //    Debug.LogError("OverworldHUD not found!");
        //}
//
        if (playerStats != null)
        {
            Debug.Log("PlayerStats assigned: " + playerStats.Name);
        }
        else
        {
            Debug.LogError("PlayerStats not found!");
        }
    }
	
	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// Check if the current scene is the overworld
		//if (scene.name == "OverworldTestScene") // replace with actual scene name
		//{
		//	OverworldHUD = FindObjectOfType<PlayerBattleHUD>();
		//	if (OverworldHUD != null)
		//	{
		//		OverworldHUD.SetHUD(playerStats);
		//	}
		//	else
		//	{
		//		Debug.LogError("OverworldHUD not found after scene load!");
		//	}
		//}

        if (scene.name != GameData.PreviousSceneName && scene.name != "BattleScene")
        {
            //New Room is detected
            playerObject.transform.position = GameObject.Find("PlayerStartPosition").transform.position;
        }
	}

    private void Update()
    {
        //if (playerStats != null && OverworldHUD != null)
        //{
        //    OverworldHUD.SetHP(playerStats.currentHealth);
        //}
    }

    public void LoadData(PersistentGameData persistentGameData)
    {
        if (!persistentGameData.playerPosition.Equals(Vector3.negativeInfinity))
        {
            playerObject.transform.position = persistentGameData.playerPosition;
        }
    }

    public void SaveData(ref PersistentGameData persistentGameData)
    {
        persistentGameData.playerPosition = playerObject.transform.position;
    }
}
