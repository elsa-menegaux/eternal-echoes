using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersistentGameData
{

    
    public Vector3 playerPosition;
    public string playerScene;


    //the values defined in this constructor will be the default values
    public PersistentGameData()
    {
        //Vector3.negativeInfinity will represent Undefined.
        playerPosition = Vector3.negativeInfinity;
    }

}
