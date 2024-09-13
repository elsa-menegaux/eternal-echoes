using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, PLAYERATTACKED, PLAYERHEALED, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public PlayerBattleHUD playerHUD;
    public EnemyBattleHUD enemyHUD;

    PlayerStats playerUnit;
    EnemyStats enemyUnit;

    public Text DialogueText;

    public BattleState state;

    float damageModified;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<PlayerStats>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<EnemyStats>();

        DialogueText.text = "A shady looking "+enemyUnit.Name+" has snuck up...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        state = BattleState.PLAYERATTACKED;
        float damageModified = playerUnit.currentDamage * playerUnit.damageModifier;
        bool isDead = enemyUnit.TakeDamage(damageModified);

        enemyHUD.SetHP(enemyUnit.currentHealth);
        DialogueText.text = "The attack was successful!";
        //Damage Enemy
        yield return new WaitForSeconds(2f);

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
        }
        else if (state == BattleState.LOST)
        {
            DialogueText.text = "You were defeated...";
        }
    }

    void PlayerTurn()
    {
        DialogueText.text = "Choose an Action...";
    }

    IEnumerator EnemyTurn()
    {
        DialogueText.text = enemyUnit.Name + " Attacks!";
        yield return new WaitForSeconds(1f);
        float damageModified = enemyUnit.currentDamage * enemyUnit.damageModifier;
        bool isDead = playerUnit.TakeDamage(damageModified);
        playerHUD.SetHP(playerUnit.currentHealth);
        yield return new WaitForSeconds(1f);
        if(isDead)
        {
            state = BattleState.LOST;
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

