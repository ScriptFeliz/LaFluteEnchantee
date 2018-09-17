using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscoverNextMonster : MonoBehaviour {
	public int price;
	public Text priceTxt, hpTxt, attackTxt, valueTxt;
	public Sprite monsterSprite;
	public Canvas canvasPrice, canvasNotEnoughGold;

	Environment env;
	Interface canvasInterface;
	Canvas newMonsterCanvas ;

	int gold;
	int numPossibleMonster;

	// Use this for initialization
	void Start () {
		numPossibleMonster = GameObject.Find ("Dungeon(Clone)").GetComponent<Dungeon> ().monsterList.GetLength(0) - 1;

		env = GameObject.Find ("Environment").GetComponent<Environment> ();
		canvasInterface = GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ();
		newMonsterCanvas = GameObject.Find ("NewMonsterDiscovered").GetComponent<Canvas> ();
	}

	void Update () {
		gold = canvasInterface.gold;

		if (env.monsterDiscovered < numPossibleMonster)
		{
			if ((gold - price) >= 0) {
				GameObject.Find ("DiscoverNextMonster").GetComponent<Image> ().color = new Color (0f, 0.7f, 0f, 1f);
			} else {
				GameObject.Find ("DiscoverNextMonster").GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
			}
		}
	}

	public void OnMouseEnter()
	{
		if (env.monsterDiscovered < numPossibleMonster)
		{
			canvasPrice.enabled = true;
		}
	}

	public void OnMouseExit ()
	{
		if (env.monsterDiscovered < numPossibleMonster)
		{
			canvasPrice.enabled = false;
		}
	}

	public void OnMouseClick()
	{
		DiscoverMonster ();
	}

	public void DiscoverMonster()
	{

		if (env.monsterDiscovered < numPossibleMonster)
		{
			if (gold - price >= 0)
			{
				canvasInterface.BuyMonster (price);
				price *= 2;

				if (env.monsterDiscovered < numPossibleMonster) {
					env.monsterDiscovered += 1;
					priceTxt.text = price + "g";
					NewMonsterCanvas (env.monsterDiscovered);
				}
			} else {
				StartCoroutine(CantAffordCanvas ());
			}
		}

	}

	public void NewMonsterCanvas(int monsterNumInList)
	{
		newMonsterCanvas.enabled = true;
		canvasPrice.enabled = false;


		GameObject monster =  GameObject.Find("Dungeon(Clone)").GetComponent<Dungeon> ().monsterList[monsterNumInList];
		Actor newMonster = monster.GetComponent <Actor> ();

		hpTxt.text = "HP : " + newMonster.hpmax;
		attackTxt.text = "Attack : " + newMonster.attack;
		valueTxt.text =  "Value : " + newMonster.value;
		monsterSprite = monster.GetComponent<SpriteRenderer>().sprite;

		GameObject.Find("NewMonsterSprite").GetComponent<Image>().sprite = monsterSprite;

	}

	public void NewMonsterCanvasHide()
	{
		newMonsterCanvas.enabled = false;
		if (env.monsterDiscovered >= numPossibleMonster)
		{
			GameObject.Find ("DiscoverNextMonster").GetComponent<Image> ().color = new Color (0.7f, 0.7f, 0.7f, 1f);
		}
	}

	IEnumerator CantAffordCanvas()
	{
		canvasNotEnoughGold = GameObject.Find ("NotEnoughGoldDiscover").GetComponent<Canvas> ();
		canvasPrice.enabled = false;
		canvasNotEnoughGold.enabled = true;
		yield return new WaitForSeconds (1);
		canvasNotEnoughGold.enabled = false;
		canvasPrice.enabled = true;
	}
}
