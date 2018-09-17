using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour {
	Environment env;
	public Text txtGold, txtDay;
	public int gold;

	Canvas canvasPrice, canvasNotEnoughGold;

	void Update()
	{

	}

	void Start()
	{
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
			
		gold = 1000;
		SetGoldText ();
		SetDayText ();
	}


	public void SetGoldText()
	{
		txtGold.text = gold + "g";
	}
	public void SetDayText()
	{
		txtDay.text = "Start Day " + env.day;
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
			int price = GameObject.Find ("Monster" + (monsterNum).ToString ()).GetComponent<SetMonster>().monsterPrice;
			if ((gold - price) < 0) {
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
