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
		for (int i=0 ; i<=3 ; i++)
		{
			ButtonColorChange (CanAffordMonster(i), i);
		}
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
	//	MonsterPrice ();
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
	//public int StatsValue(int attack, int attackSpeed, int hp, int armor, int mooveSpeed)
	public int StatsValue(int attack, int hp)
	{
		monsterPrice =  (int)Mathf.Round((priceHP * hp) + (priceAttack * attack));
		return monsterPrice;
	}


	//Calcul du coût du monstre
	public void MonsterPrice()
	{
		//monsterPrice = StatsValue(initAttack, initAttackSpeed, initHP, initArmor, 0);
		monsterPrice = StatsValue(initAttack, initHP);

		for (int i=0 ; i<=3 ; i++)
		{
			ButtonColorChange (CanAffordMonster(i), i);
		}
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
		for (int i=0 ; i<=3 ; i++)
		{
			ButtonColorChange (CanAffordMonster(i), i);
		}
	}

	//Achat d'un monstre
	public void addGold(int goldToAdd)
	{
		gold += goldToAdd;
		SetGoldText ();
		for (int i=0 ; i<=3 ; i++)
		{
			ButtonColorChange (CanAffordMonster(i), i);
		}
	}

	//Gestion de couleur du bouton d'achat de monstre
	void ButtonColorChange(bool canAfford, int monsterNum)
	{
		if (canAfford) {
			//VERT
			if (monsterNum == 0) {
				GameObject.Find ("GenerateMonster").GetComponent<Image> ().color = new Color (0f, 0.7f, 0f, 1f);
			} else {
				GameObject.Find ("Button" + monsterNum.ToString ()).GetComponent<Image> ().color = new Color (0f, 0.7f, 0f, 1f);
			}
		} else {
			//ROUGE
			if (monsterNum == 0) {
				GameObject.Find ("GenerateMonster").GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
			} else {
				GameObject.Find ("Button" + monsterNum.ToString ()).GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
			}
		}
	}
		
	public void StartDay()
	{
		GameObject.Find("Environment").GetComponent<DayGest>().StartNewDay();
	}
		
}
