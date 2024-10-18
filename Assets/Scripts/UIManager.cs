using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject hud_Object;
    private Canvas canvas;
    
    // For debugging purposes
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public Button LobbyButton;

    private void Awake()
    {
        // Ensure there's only one instance of UIManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("UIManager instantiated.");
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }

        canvas = GetComponent<Canvas>();
        canvas.worldCamera = FindObjectOfType<Camera>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene load.");
        if (pauseButton != null && (pauseButton.activeSelf != true))
        {
            Debug.Log("Time to activate the pause button.");
            pauseMenu.SetActive(false);
            pauseButton.SetActive(true);
        }

        // Hide HUD in Battle and Lobby scenes as they are not relevant there
        if (scene.name == "BattleScene" || scene.name == "Lobby")
        {
            Debug.Log("Hide HUD");
            hud_Object.SetActive(false);
        } else if(!hud_Object.activeSelf) {
            hud_Object.SetActive(true);
        }

        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }
        canvas.worldCamera = FindObjectOfType<Camera>();

        if (scene.name == "Lobby")
        {
            LobbyButton.interactable = false;
        }
        else
        {
            LobbyButton.interactable = true;
        }
    }

    private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
