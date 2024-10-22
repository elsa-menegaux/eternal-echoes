using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    DataPersistenceManager dataPersistenceManager;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveButton()
    {
        if (dataPersistenceManager == null)
        {
            dataPersistenceManager = FindObjectOfType<DataPersistenceManager>();
            if (dataPersistenceManager == null)
            {
                Debug.LogError("Cannot Find DataPersistenceManager to Save to");
                return;
            }
        }

        dataPersistenceManager.SaveGame();

    }

    public void ReturnToLobby()
    {
        SceneManager.LoadScene("Lobby");
		GameData.roomCount=0;
		PlayerManager.Instance.transform.GetChild(0).gameObject.SetActive(true);
    }
    // NB : we need to reset the game for any of those actions.
}
