using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
	public Text hpText;
	public string max;
    public Slider hpSlider;

    public void SetHUD(PlayerStats unit)
	{
		if (unit == null)
		{
			Debug.LogError("Unit is null in SetHUD!");
			return; // Prevent further execution
		}

		nameText.text = unit.Name;
		levelText.text = "Lvl " + unit.Level;
		hpSlider.maxValue = unit.maxHealth;
		hpSlider.value = unit.currentHealth;
		hpText.text = unit.currentHealth +"/"+unit.maxHealth;
		max ="/"+unit.maxHealth;
	}


    public void SetHP(float hp)
    {
        hpSlider.value = hp;
		hpText.text = hp + max;
    }
}
