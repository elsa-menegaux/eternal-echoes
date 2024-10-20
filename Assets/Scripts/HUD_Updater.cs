using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{

    public bool findPlayerStatsInScene = true;

    public Image healthSlider;
    public TextMeshProUGUI coinsText;
    public TMP_InputField inputField;
    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        if (findPlayerStatsInScene)
        {
            playerStats =  PlayerManager.Instance.playerStats;
        }
    }

    // Update is called once per frame
    void Update()
    {   if (playerStats != null)
        {
            updateHealthBar(playerStats.currentHealth, playerStats.maxHealth);
            updateCoinsValue(playerStats.money);
        }
    }

    public void updateHealthBar(float currentHealth, float maxHealth)
    {
        healthSlider.fillAmount = currentHealth/maxHealth;
    }

    public void updateCoinsValue(int coinsCount)
    {
        coinsText.text = coinsCount + " Coins";
    }

    public void SavePlayerName()
    {
        // Access the text from the InputField and store it in the playerName variable
        playerStats.playerName = inputField.text;
        
        // Optionally, you can log or perform any other actions
        Debug.Log("Player's name is: " + playerStats.playerName);
    }
}
