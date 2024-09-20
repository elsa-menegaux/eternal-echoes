using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
	public Text hpText;
	public string max;
    public Slider hpSlider;

    public void SetHUD(EnemyStats Unit)
    {
        nameText.text = Unit.Name;
        levelText.text = "Lvl "+Unit.Level;
        hpSlider.maxValue = Unit.maxHealth;
        hpSlider.value = Unit.currentHealth;
		hpText.text = Unit.currentHealth +"/"+ Unit.maxHealth;
		max = "/"+ Unit.maxHealth;
    }

    public void SetHP(float hp)
    {
        hpSlider.value = hp;
		hpText.text = hp + max;
    }

}
