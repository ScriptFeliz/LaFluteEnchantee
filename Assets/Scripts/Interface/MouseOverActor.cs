using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverActor : MonoBehaviour {

	public Text statHP;
	public Text statArmor;
	public Text statAttack;
	public Text statAttackSpeed;
	public Text statMooveSpeed;
	public Text statKills;

	//public void SetStats (bool monster, int HP, int  HPMax, int Armor, int Attack, double AttackSpeed, double MooveSpeed, int Kills)
	public void SetStats (bool monster, int HP, int  HPMax, int Attack, int Kills)
	{
		statHP.text = "HP : " + HP + "/" + HPMax;
		statAttack.text = "Attack : " + Attack;
		statKills.text = "Kills : " + Kills;
	}
}
