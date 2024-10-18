using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence 
{
    void LoadData(PersistentGameData persistentGameData);

    void SaveData(ref PersistentGameData persistentGameData);
}
