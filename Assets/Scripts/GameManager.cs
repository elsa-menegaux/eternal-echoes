using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager instance;
    
    public Dictionary<string, bool> enemyStatus = new Dictionary<string, bool>();

    [Header("Echo Spawning")]
    public List<string> dungeonLevels = new List<string>() { "Room 1", "Room 2", "Room 3", "Room 4", "Room 5" };
    public GameObject echoPrefab;
    public string echoSpawnLocationObjectName = "EchoSpawnPosition";
    [Range(0,100)][Tooltip("Percentage Change on range 0-100")]
    public float echoSpawnChance = 100f; //1f being 1%


    public void LoadData(PersistentGameData persistentGameData)
    {
        enemyStatus = persistentGameData.enemyStatus;
    }

    public void SaveData(ref PersistentGameData persistentGameData)
    {
        persistentGameData.enemyStatus = new SerializableDictionary<string, bool>(enemyStatus);
    }

    private void Awake()
    {
        // Ensure there's only one instance of GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
			Debug.Log("GameManager instantiated.");
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }
	
	private void Start()
	{
		// Initialize enemyStatus if needed
		if (enemyStatus == null)
		{
			enemyStatus = new Dictionary<string, bool>();
			Debug.Log("GameManager instantiated.");
		}
		
	}

    private void Update()
    {
        if (DontDestroyOnLoadDestroyer.killAllObjects)
        {
            Destroy(gameObject);
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if (dungeonLevels.Contains(scene.name))
        {
            float echoSpawnRoll = Random.Range(0f,100f);
            if (echoSpawnRoll <= echoSpawnChance)
            {
                //spawn Echo
                attemptEchoSpawn(); 
            }
        }
    }

    private void attemptEchoSpawn()
    {
        GameObject echoLocationObj = GameObject.Find(echoSpawnLocationObjectName);
        //if we didnt find the locaion abort.
        if (echoLocationObj == null) 
        {
            Debug.LogWarning("Attempted to Spawn Echo enemy but no location to Instantiate was Found.");
            return;
        }

        Vector3 spawnLocation = echoLocationObj.transform.position;
        //if no prefab is assigned
        if (echoPrefab == null)
        {
            Debug.LogWarning("Attempted to Spawn Echo enemy but no Prefab Was Assigned to GameManager.");
            return;
        }

        GameObject echoObject = Instantiate<GameObject>(echoPrefab, spawnLocation, Quaternion.identity);
        EnemyStats echoStats = echoObject.GetComponent<EnemyStats>();
		//echoObject.name = "'Echo'";

        if (echoStats == null)
        {
            Debug.LogWarning("No EnemyStats were found on the Echo Prefab.\nAborting spawn and cleaning up.");
            Destroy(echoObject);
            return;
        }

        echoStats.CopyStats(PlayerManager.Instance.playerObject.GetComponent<PlayerStats>());
        echoStats.currentHealth = echoStats.maxHealth;

    }
}
