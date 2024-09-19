using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{

    private string FightModeScene; //to change with the actual fight mode scene

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy")){
            Debug.Log("Player encountered an enemy");
            UnityEngine.SceneManagement.SceneManager.LoadScene(FightModeScene);
        }
    }
}
