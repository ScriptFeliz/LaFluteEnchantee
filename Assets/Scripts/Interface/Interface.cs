using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour {
	Environment env;
	public Text txtGold, txtDay;
	public int gold;
	public bool generationEnabled, selectionEnabled;

	Canvas canvasPrice, canvasNotEnoughGold;

	void Update()
	{

	}

	void Start()
	{
		env = GameObject.Find ("Environment").GetComponent<Environment> ();

		gold = 1000;
		generationEnabled = true;
		selectionEnabled = true;

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
	public bool CanAfford(int price)
	{
		if ((gold - price) < 0) {
			return false;
		} else {
			return true;
		}
	}

	public IEnumerator CantAffordCanvas(Canvas canvasPrice, Canvas canvasNotEnoughGold)
	{
		canvasPrice.enabled = false;
		canvasNotEnoughGold.enabled = true;
		yield return new WaitForSeconds (1);
		canvasNotEnoughGold.enabled = false;
		canvasPrice.enabled = true;
	}

	//Achat d'un monstre
	public void removeGold(int price)
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
		env.isDay = true;
		env.RoomOverlayOff ();
		GameObject.Find("Environment").GetComponent<DayGest>().StartNewDay();
	}
		
}
