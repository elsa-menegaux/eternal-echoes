using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, PLAYERATTACKED, PLAYERHEALED, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
	public EnemySpriteDatabase enemySpriteDatabase; // Reference to your Sprite Database
    public GameObject playerPrefab;
	public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;
	
	public Button attackButton;
    public Button healButton;

    public PlayerBattleHUD playerHUD;
    public EnemyBattleHUD enemyHUD;

    PlayerStats playerUnit;
    EnemyStats enemyUnit;

    public Text DialogueText;

    public BattleState state;

    float damageModified;
	
	
	public string battleSceneName;  // Name of your battle scene
	

    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("EnemyStats in Battle Scene: " + GameData.EnemyStats?.Name);
        state = BattleState.START;
		
        StartCoroutine(SetupBattle());
		
		attackButton.interactable = false;
        healButton.interactable = false;
    }

    IEnumerator SetupBattle()
	{
		// Set up player from PlayerManager
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<PlayerStats>();
        SetPlayerStatsFromManager();

        playerHUD.SetHUD(playerUnit);

		
		Debug.Log("Battle initiated with: " + GameData.EnemyStats.Name);
        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyGO.transform.localPosition = new Vector3(0,0.3f,0); // Center the enemy in the battle station
		enemyGO.transform.localRotation = Quaternion.identity; // Reset rotation
		enemyGO.transform.localScale = Vector3.one; // Reset scale if necessary
        enemyUnit = enemyGO.GetComponent<EnemyStats>();
		// Fetch the enemy from GameDataHolder and set up
		Debug.Log("Null Check name: " + GameData.EnemyStats.Name);
		Debug.Log("Null Check reward: " + GameData.EnemyStats.Reward);
		Debug.Log("Null Check level: " + GameData.EnemyStats.Level);
        SetEnemyStatsFromGameData();

        // Set enemy sprite based on enemy name
		string enemyName = GameData.EnemyStats.Name; // Get the enemy's name
		EnemySpriteDatabase.EnemyData enemyData = enemySpriteDatabase.GetEnemyDataByName(enemyName); // Get the enemy data
        //Sprite enemySprite = enemySpriteDatabase.GetSpriteByName(enemyName); // Get the sprite

        if (enemyData != null)
        {
            // Assign the sprite to your enemy GameObject
            enemyGO.GetComponent<SpriteRenderer>().sprite = enemyData.sprite;
			
			if (enemyData.animatorController != null)
            {
                enemyGO.GetComponent<Animator>().runtimeAnimatorController = enemyData.animatorController;
            }
			else
            {
                // Optionally, handle cases where no animation is needed
                Debug.Log("No animator controller found for enemy: " + enemyName);
            }
        }
        else
        {
            Debug.LogError("Enemy Data not found for enemy: " + enemyName);
        }
		
		enemyGO.SetActive(true);
		
		if (enemyName == "'Echo'")
		{
			// Set the size for Echo enemy
			Vector3 newSize = new Vector3(0.09f, 0.09f, 1f); // (0.09x original size)
			enemyGO.transform.localScale = newSize;
		}

        enemyHUD.SetHUD(enemyUnit);
        DialogueText.text = "A shady looking " + enemyUnit.Name + " has snuck up...";

        //Debug.LogError("Enemy stats not found in GameDataHolder!");

		yield return new WaitForSeconds(2f);
	
		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}
	
	void SetPlayerStatsFromManager()
    {
        if (PlayerManager.Instance.playerStats != null)
        {
            playerUnit.playerName = PlayerManager.Instance.playerStats.playerName;
            playerUnit.level = PlayerManager.Instance.playerStats.level;
            playerUnit.maxHealth = PlayerManager.Instance.playerStats.maxHealth;
            playerUnit.currentHealth = PlayerManager.Instance.playerStats.currentHealth;
            playerUnit.currentDamage = PlayerManager.Instance.playerStats.currentDamage;
            playerUnit.currentAbilityDamage = PlayerManager.Instance.playerStats.currentAbilityDamage;
            playerUnit.currentCritChance = PlayerManager.Instance.playerStats.currentCritChance;
            playerUnit.currentCritDamage = PlayerManager.Instance.playerStats.currentCritDamage;
            playerUnit.currentDodgeRate = PlayerManager.Instance.playerStats.currentDodgeRate;
            playerUnit.money = PlayerManager.Instance.playerStats.money;
        }
    }

    void SetEnemyStatsFromGameData()
    {
        enemyUnit.Name = GameData.EnemyStats.Name;
		Debug.Log("GameData.Name: " + GameData.EnemyStats.Name);
		Debug.Log("enemyUnit.Name: " + enemyUnit.Name);
        enemyUnit.Level = GameData.EnemyStats.Level;
		Debug.Log("GameData.Level: " + GameData.EnemyStats.Level);
		Debug.Log("enemyUnit.Level: " + enemyUnit.Level);
        enemyUnit.maxHealth = GameData.EnemyStats.maxHealth;
		Debug.Log("GameData.maxHealth: " + GameData.EnemyStats.maxHealth);
		Debug.Log("enemyUnit.maxHealth: " + enemyUnit.maxHealth);
        enemyUnit.currentHealth = GameData.EnemyStats.currentHealth;
		Debug.Log("GameData.currentHealth: " + GameData.EnemyStats.currentHealth);
		Debug.Log("enemyUnit.currentHealth: " + enemyUnit.currentHealth);
        enemyUnit.currentDamage = GameData.EnemyStats.currentDamage;
		Debug.Log("GameData.currentDamage: " + GameData.EnemyStats.currentDamage);
		Debug.Log("enemyUnit.currentDamage: " + enemyUnit.currentDamage);
        enemyUnit.currentAbilityDamage = GameData.EnemyStats.currentAbilityDamage;
		Debug.Log("GameData.currentAbilityDamage: " + GameData.EnemyStats.currentAbilityDamage);
		Debug.Log("enemyUnit.currentAbilityDamage: " + enemyUnit.currentAbilityDamage);
        enemyUnit.currentCritChance = GameData.EnemyStats.currentCritChance;
        enemyUnit.currentCritDamage = GameData.EnemyStats.currentCritDamage;
        enemyUnit.currentDodgeRate = GameData.EnemyStats.currentDodgeRate;
        enemyUnit.damageModifier = GameData.EnemyStats.damageModifier;
    }
	

    IEnumerator PlayerAttack()
    {
        state = BattleState.PLAYERATTACKED;
		
		// Disable buttons when the player is performing their action
        attackButton.interactable = false;
        healButton.interactable = false;
		
		// Check if the enemy dodges the attack
		float dodgeRoll = Random.Range(0f, 100f);
		if (dodgeRoll <= enemyUnit.currentDodgeRate)
		{
			DialogueText.text = $"{enemyUnit.Name} dodged the attack!";
			yield return new WaitForSeconds(1f);
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
			yield break; // Exit the coroutine if the attack is dodged
		}
		
		// Calculate if the attack is a critical hit
		float critRoll = Random.Range(0f, 100f);
		float damageModified = playerUnit.currentDamage * playerUnit.damageModifier;
	
		if (critRoll <= playerUnit.currentCritChance)
		{
			damageModified *= playerUnit.currentCritDamage; // Apply critical damage multiplier
			DialogueText.text = "Critical hit!";
			yield return new WaitForSeconds(1f);
		}
		
        bool isDead = enemyUnit.TakeDamage(damageModified);

        enemyHUD.SetHP(enemyUnit.currentHealth);
        DialogueText.text = "The attack was successful!";
        //Damage Enemy
        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.WON;
            enemyHUD.SetHP(enemyUnit.currentHealth = 0);
            EndBattle();
            //End Battle
        }
        else
        {  
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            //Enemy Turn
        }
        
        //Check Enemy State
        //Change Battle State based on enemy state
    }

    IEnumerator PlayerHeal()
    {
        state = BattleState.PLAYERHEALED;
		
		// Disable buttons when the player is performing their action
        attackButton.interactable = false;
        healButton.interactable = false;
		
		
        float damageModified = playerUnit.currentAbilityDamage * playerUnit.damageModifier;
        playerUnit.RegainHealth(damageModified);
        playerHUD.SetHP(playerUnit.currentHealth);
        DialogueText.text = "You healed some health!";
        //Heal Self
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        //Enemy Turn
    }
    
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            DialogueText.text = "You won!";
			playerUnit.money =+ enemyUnit.Reward;
			StartCoroutine(TransitionToOverworld());
        }
        else if (state == BattleState.LOST)
        {
            DialogueText.text = "You were defeated...";
			//StartCoroutine(TransitionToOverworld());
            StartCoroutine(ReturnToMenu());
        }
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }
	
	IEnumerator TransitionToOverworld()
	{
		PlayerManager.Instance.playerStats.playerName = playerUnit.playerName;
		PlayerManager.Instance.playerStats.level= playerUnit.level;
		PlayerManager.Instance.playerStats.maxHealth = playerUnit.maxHealth;
		PlayerManager.Instance.playerStats.currentHealth = playerUnit.currentHealth;
		PlayerManager.Instance.playerStats.currentDamage = playerUnit.currentDamage; 
		PlayerManager.Instance.playerStats.currentAbilityDamage = playerUnit.currentAbilityDamage;
		PlayerManager.Instance.playerStats.currentCritChance = playerUnit.currentCritChance;
		PlayerManager.Instance.playerStats.currentCritDamage = playerUnit.currentCritDamage;
		PlayerManager.Instance.playerStats.currentDodgeRate = playerUnit.currentDodgeRate;
		PlayerManager.Instance.playerStats.money = playerUnit.money;
		
		
		yield return new WaitForSeconds(2f);
		if (PlayerManager.Instance != null)
        {
            Debug.Log("PlayerManager is active with current health: " + PlayerManager.Instance.playerStats.currentHealth);
        }
        else
        {
            Debug.LogError("PlayerManager instance not found!");
        }
		string previousSceneName = GameData.PreviousSceneName; // Ensure to store this in GameDataHolder
		SceneManager.LoadScene(previousSceneName, LoadSceneMode.Single);
		
		PlayerManager.Instance.transform.GetChild(0).gameObject.SetActive(true);
	}

    void PlayerTurn()
    {
		attackButton.interactable = true;
        healButton.interactable = true;
        DialogueText.text = "Choose an Action...";
    }

    IEnumerator EnemyTurn()
    {
        DialogueText.text = enemyUnit.Name + " Attacks!";
        yield return new WaitForSeconds(1f);
		
        // Check if the player dodges the attack
		float dodgeRoll = Random.Range(0f, 100f);
		if (dodgeRoll <= playerUnit.currentDodgeRate)
		{
			DialogueText.text = $"You dodged the attack!";
			yield return new WaitForSeconds(1f);
			state = BattleState.PLAYERTURN;
			PlayerTurn();
			yield break; // Exit the coroutine if the attack is dodged
		}
		
		// Calculate if the attack is a critical hit
		float critRoll = Random.Range(0f, 100f);
		float damageModified = enemyUnit.currentDamage * enemyUnit.damageModifier;
	
		if (critRoll <= enemyUnit.currentCritChance)
		{
			damageModified *= enemyUnit.currentCritDamage; // Apply critical damage multiplier
			DialogueText.text = "Critical hit!";
			yield return new WaitForSeconds(1f);
		}
		
        bool isDead = playerUnit.TakeDamage(damageModified);
		
		
        playerHUD.SetHP(playerUnit.currentHealth);
        yield return new WaitForSeconds(1f);
        if(isDead)
        {
            state = BattleState.LOST;
			playerUnit.currentHealth = playerUnit.maxHealth;
            EndBattle();
            //End Battle
        }
        else
        {  
            state = BattleState.PLAYERTURN;
            PlayerTurn();
            //Player Turn
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerHeal());
    }
	
	
}

