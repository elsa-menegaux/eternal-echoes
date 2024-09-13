using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Unit
{
	[SerializeField] public string Name = "Player";
	[SerializeField] public int Level = 1;
	[SerializeField] public float damageModifier = 0.2F;
	
	public int money;
	
	private void Awake() {
	
	}
	
    // Start is called before the first frame update
    private void Start()
    {
		GetStats();
		money = 0;
    }
	
	public void GainMoney(int reward)
	{
		money+=reward;
	}
}
