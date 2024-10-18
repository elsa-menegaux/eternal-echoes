using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataPersistenceManager))]
public class DataPersistenceManagerEditorHelper : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        DataPersistenceManager manager = target.GetComponent<DataPersistenceManager>();

        if( GUILayout.Button("Save Data")) {
            manager.InitDataHandler();
            manager.CollectDataObjectsFromScene();
            manager.SaveGame();
        }

        if(GUILayout.Button("Load Data")) {
            manager.InitDataHandler();
            manager.CollectDataObjectsFromScene();
            manager.LoadGame();
        }

        if(GUILayout.Button("New Save")) {
            manager.InitDataHandler();
            manager.CollectDataObjectsFromScene();
            manager.NewGame();
        }
    }
  
}
