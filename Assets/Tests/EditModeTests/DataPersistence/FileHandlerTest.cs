using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.TestTools;

public class FileHandlerTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void FileHandler_FileContentTest()
    {
        PersistentGameData dataToTestSave = new PersistentGameData
        {
            playerPosition = new Vector3(1, 2, 3),
            enemyStatus = new SerializableDictionary<string, bool> { {"enemy1", true} },
            playerScene = "TestScene"
        };

        string fileName = "fileContentTest.json";
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);
        FileDataHandler fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        fileDataHandler.Save(dataToTestSave);

        string savedContent = File.ReadAllText(fullPath);
        Assert.IsTrue(savedContent.Contains("\"playerPosition\":"));
        Assert.IsTrue(savedContent.Contains("\"enemyStatus\":"));
        Assert.IsTrue(savedContent.Contains("\"playerScene\": \"TestScene\""));

        // Cleanup
        File.Delete(fullPath);
    }


    [Test]
    public void FileHandler_SaveLoadWithNullValuesTest()
    {
        PersistentGameData dataToTestSave = new PersistentGameData
        {
            playerPosition = Vector3.zero,
            enemyStatus = null,  // Null enemy status
            playerScene = null  // Null scene name
        };

        string fileName = "nullValuesTest.json";
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);
        FileDataHandler fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        fileDataHandler.Save(dataToTestSave);
        PersistentGameData loadedTestData = fileDataHandler.Load();

        Assert.AreEqual(dataToTestSave.playerPosition, loadedTestData.playerPosition);
        Assert.IsEmpty(loadedTestData.enemyStatus);
        Assert.IsEmpty(loadedTestData.playerScene);

        File.Delete(fullPath);
    }


}
