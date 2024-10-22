using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEditor.Build;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;


public class RandomRoomTest : MonoBehaviour
{
    

   

    [Test]
    public void RandomRoom_Test() {
    
        

        GameObject gameObj = new GameObject("testName");
        LevelSwitch levelSwitchTest = gameObj.AddComponent<LevelSwitch>();


        levelSwitchTest.roomScenes = new List<string>{"Room 1","Room 2","Room 3","Room 4","Room 5"};
        levelSwitchTest.randomIndex = true;
        levelSwitchTest.banSameRoom = false;

        
        string test = levelSwitchTest.DetermineNextScene();
        SceneManager.LoadScene(test);
        Debug.Log(test);
        
        Assert.IsTrue(levelSwitchTest.roomScenes.Contains(test));

        UnityEngine.Object.Destroy(gameObj);
        
    }
}
