using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;

    public void SetHUD(PlayerStats Unit)
    {
        nameText.text = Unit.Name;
        levelText.text = "Lvl "+Unit.Level;
        hpSlider.maxValue = Unit.maxHealth;
        hpSlider.value = Unit.currentHealth;
    }

    public void SetHP(float hp)
    {
        hpSlider.value = hp;
    }
}
