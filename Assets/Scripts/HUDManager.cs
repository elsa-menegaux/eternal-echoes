using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{

    public Image healthSlider;
    public TextMeshProUGUI coinsText;
    public TMP_InputField inputField;
    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindObjectOfType<PlayerManager>().playerStats;
        
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = playerStats.Money + " Coins";
        healthSlider.fillAmount = playerStats.currentHealth/playerStats.maxHealth;
        
    }

        public void SavePlayerName()
    {
        // Access the text from the InputField and store it in the playerName variable
        playerStats.Name = inputField.text;
        
        // Optionally, you can log or perform any other actions
        Debug.Log("Player's name is: " + playerStats.Name);
    }
}
