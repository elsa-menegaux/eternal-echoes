using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour, IDataPersistence
{
    public static PlayerManager Instance;
    public static bool loadFromSave = false;
    public static int loadCounter = 0;

    //public PlayerBattleHUD OverworldHUD;

    public GameObject playerObject;
    [HideInInspector] public PlayerStats playerStats;
    [HideInInspector] public PlayerColourController playerColourController;

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

        playerColourController = playerObject.GetComponentInChildren<PlayerColourController>();

        if (playerStats != null)
        {
            Debug.Log("PlayerStats assigned: " + playerStats.playerName);
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
		

        if (scene.name != GameData.PreviousSceneName && scene.name != "BattleScene")
        {
            //New Room is detected
            GameObject playerStart = GameObject.Find("PlayerStartPosition");
            if (playerStart != null)
            {
                playerObject.transform.position = playerStart.transform.position;
            }
            
        }
        if (loadFromSave && loadCounter < 2)
        {
            if (DataPersistenceManager.instance != null)
            {
                DataPersistenceManager.instance.LoadGame();
                loadCounter+=1;
            }
        } else {
            loadFromSave = false;
            loadCounter = 0;
        }
	}

    private void Update()
    {
        if (DontDestroyOnLoadDestroyer.killAllObjects)
        {
            Destroy(gameObject);
        }
    }

    public void LoadData(PersistentGameData persistentGameData)
    {
        //load needed scene first
        if (SceneManager.GetActiveScene().name != persistentGameData.playerScene)
        {
            SceneManager.LoadScene(persistentGameData.playerScene);
        }


        //Load Player position only if not Negative Infinity
        if (!persistentGameData.playerPosition.Equals(Vector3.negativeInfinity))
        {
            playerObject.transform.position = persistentGameData.playerPosition;
        }
		
		GameData.roomCount = persistentGameData.roomCount;
		Debug.Log("RoomCount loaded as "+GameData.roomCount);

        if (playerColourController == null)
        {
            playerColourController = playerObject.GetComponentInChildren<PlayerColourController>();
        }
        playerColourController.SetColours(persistentGameData.playerColourData);
    }

    public void SaveData(ref PersistentGameData persistentGameData)
    {
        //Save player position
        persistentGameData.playerPosition = playerObject.transform.position;

        //Save Current Scene Name// or build index
        persistentGameData.playerScene = SceneManager.GetActiveScene().name;
		    persistentGameData.roomCount = GameData.roomCount;
        persistentGameData.playerColourData = playerColourController.GetColours();
    }
}
