using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDungeonSize : MonoBehaviour {
	public int price;
	Interface canvasInterface;
	Dungeon dungeon;
	public Canvas canvasPrice, canvasNotEnoughGold;
	public Text priceTxt;
	int gold;


	// Use this for initialization
	void Start () {
		canvasInterface = GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ();
		dungeon = GameObject.Find ("Dungeon").GetComponent<Dungeon> ();
		priceTxt.text = price + "g";
	}
	
	// Update is called once per frame
	void Update () {
		gold = canvasInterface.gold;

		if ((gold - price) >= 0) {
			GameObject.Find ("UpgradeDungeonSize").GetComponent<Image> ().color = new Color (0f, 0.7f, 0f, 1f);
		} else {
			GameObject.Find ("UpgradeDungeonSize").GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
		}
	}

	public void OnMouseEnter()
	{
		canvasPrice.enabled = true;
	}

	public void OnMouseExit ()
	{
		canvasPrice.enabled = false;
	}

	public void OnMouseClick()
	{
		if (gold - price >= 0)
		{
			canvasInterface.removeGold (price);
			price *= 2;
			priceTxt.text = price + "g";
			if (Dungeon.height == Dungeon.width)
			{
				dungeon.UpgradeDungeonSize (Dungeon.height + 1, Dungeon.width);
			} else
			{
				dungeon.UpgradeDungeonSize (Dungeon.height, Dungeon.width + 1);
			}
		} else {
			StartCoroutine(CantAffordCanvas ());
		}
	}

	IEnumerator CantAffordCanvas()
	{
		canvasNotEnoughGold = GameObject.Find ("NotEnoughGoldSize").GetComponent<Canvas> ();
		canvasPrice.enabled = false;
		canvasNotEnoughGold.enabled = true;
		yield return new WaitForSeconds (1);
		canvasNotEnoughGold.enabled = false;
		canvasPrice.enabled = true;
	}
}
