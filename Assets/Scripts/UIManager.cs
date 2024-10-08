using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject hud_Object;
    private Canvas canvas;

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
        if (scene.name == "BattleScene")
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
