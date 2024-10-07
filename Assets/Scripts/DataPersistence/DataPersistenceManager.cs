using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    private PersistentGameData persistentGameData;

    private List<IDataPersistence> dataPersistenceObjects;

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
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    }


    public void NewGame() 
    {
        this.persistentGameData = new PersistentGameData();
    }

    public void LoadGame()
    {
        //TODO load any saved data from fileHandler
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
        //TODO pass the data to other scripts so they can update it 

        //TODO pass data to filehandler to save
    }


    
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();
        
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
