using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverActor : MonoBehaviour {

	public Text statHP, statArmor, statAttack, statStamina, statAttackSpeed, statMooveSpeed, statKills;

	//public void SetStats (bool monster, int HP, int  HPMax, int Armor, int Attack, double AttackSpeed, double MooveSpeed, int Kills)
	public void SetStats (int HP, int  HPMax, int Attack, int Stamina, int StaminaMax, int Kills)
	{
		statHP.text = "HP : " + HP + "/" + HPMax;
		statAttack.text = "Attack : " + Attack;
		statKills.text = "Kills : " + Kills;
		if (StaminaMax == 0)
		{
			statStamina.text = "";
		} else 
		{
			statStamina.text = "Stamina : " + Stamina + "/" + StaminaMax;
		}
	}
}
