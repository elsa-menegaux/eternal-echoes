using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string saveFile = "save.json";
    public Button continueButton;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        DontDestroyOnLoadDestroyer.killAllObjects = true;
        if (GameManager.instance !=null)
        {
            GameManager.instance.gameWon = false;
        }
        
        StartCoroutine("LoadLobby");
    }

    public void ContinueGame()
    {
        DontDestroyOnLoadDestroyer.killAllObjects = true;
        PlayerManager.loadFromSave = true;
        PlayerManager.loadCounter = 0;
        
        StartCoroutine("LoadLobby");
    }

    IEnumerator LoadLobby()
    {
        yield return new WaitForSeconds(1);
        DontDestroyOnLoadDestroyer.killAllObjects = false;
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetVisitedRooms();
        }
       
        Debug.Log("Reset number of rooms visited.");
        SceneManager.LoadScene("Lobby");
    }

    private void Start()
    {
        if (continueButton == null) 
        {
            Debug.LogWarning("MainMenu: ContinueButton Not Assigned\nCannot show/hide based on existence of save file.");
            return;
        }
        string fullPath = Path.Join(Application.persistentDataPath, saveFile);
        if(File.Exists(fullPath))
        {
            continueButton.gameObject.SetActive(true);
        } else {
            continueButton.gameObject.SetActive(false);
        }
        if (GameManager.instance != null && GameManager.instance.gameWon == true)
        {
            continueButton.gameObject.SetActive(false);
        }
    }
}
