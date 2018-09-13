using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour {
	Environment env;
	public Text txtHP, txtAttack, txtAttackSpeed, txtMooveSpeed, txtGold, txtPrice, txtDay;
	public int initHP, initAttack, initAttackSpeed, initMooveSpeed, gold;

	int priceHP, priceArmor, priceAttack, priceAttackSpeed, priceMooveSpeed, monsterPrice;

	Canvas canvasPrice, canvasNotEnoughGold;

	void Update()
	{

	}

	void Start()
	{
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
		initHP = 150;
		initAttack = 10;
		initAttackSpeed = 100;
		initMooveSpeed = 100;
			
		priceHP = 1;
		priceAttack = 5;
			
		gold = 1000;
		SetGoldText ();
		SetDayText ();
		//MonsterPrice ();
	}


	public void SetGoldText()
	{
		txtGold.text = gold + "g";
	}
	public void SetDayText()
	{
		txtDay.text = "Start Day " + env.day;
	}

	//Prix des stats
	public int StatsValue(int attack, int hp)
	{
		monsterPrice =  (int)Mathf.Round((priceHP * hp) + (priceAttack * attack));
		return monsterPrice;
	}
		
	//Possibilité d'achat Oui/Non
	public bool CanAffordMonster(int monsterNum)
	//la variable locale Monster représente :
	//	0 : Prix de la création d'un monstre
	//	1 2 3 : Prix du monstre (1 2 ou 3)
	{
		if (monsterNum == 0) {
			if ((gold - 50) < 0) {
				return false;
			} else {
				return true;
			}
		}
		else
			{
			if ((gold - monsterPrice) < 0) {
				return false;
			} else {
				return true;
			}
		}
	}
	//Achat d'un monstre
	public void BuyMonster(int price)
	{
		gold -= price;
		SetGoldText ();
	}

	//Achat d'un monstre
	public void addGold(int goldToAdd)
	{
		gold += goldToAdd;
		SetGoldText ();
	}


	public void StartDay()
	{
		GameObject.Find("Environment").GetComponent<DayGest>().StartNewDay();
	}
		
}
