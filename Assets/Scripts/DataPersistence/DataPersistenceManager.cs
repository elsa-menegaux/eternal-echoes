using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName = "save.json";
    

    [SerializeField]
    private PersistentGameData persistentGameData;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one DataPersistenceManager in the scene.\n Destroying script on " + gameObject.name);
            Destroy(this);
        }
        instance = this;
    }

    private void Start()
    {
        InitDataHandler();
        CollectDataObjectsFromScene();
    }

    public void InitDataHandler()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }
    
    public void CollectDataObjectsFromScene()
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    }

    public void NewGame() 
    {
        this.persistentGameData = new PersistentGameData();
    }

    public void LoadGame()
    {
        // load any saved data from fileHandler
        this.persistentGameData = dataHandler.Load();

        //if no data can be loaded create a new game
        if (this.persistentGameData == null)
        {
            Debug.Log("No data was found. Initializing to defaults.");
            NewGame();
        }
        // push the loaded data to all other scripts that need it
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(persistentGameData);
        }
    }

    public void SaveGame()
    {
        // pass the data to other scripts so they can save the needed data to persistentGameData 
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref persistentGameData);
        }

        //pass data to filehandler to save
        dataHandler.Save(persistentGameData);
    }


    
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        //find all scripts implementing IDataPersistence in the scene
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();
        
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
