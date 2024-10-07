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
            manager.SaveGame();
        }

        if(GUILayout.Button("Load Data")) {
            manager.LoadGame();
        }
    }
  
}
