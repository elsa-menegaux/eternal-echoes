using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
}
