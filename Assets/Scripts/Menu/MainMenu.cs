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
        
        StartCoroutine("LoadLobby");
    }

    IEnumerator LoadLobby()
    {
        yield return new WaitForSeconds(1);
        DontDestroyOnLoadDestroyer.killAllObjects = false;
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
    }
}
