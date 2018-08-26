using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetMonster : MonoBehaviour {
	public Text hpTxt, attackTxt, priceTxt;
	public Image monsterImg;
	public int monsterID, monsterHP, monsterAttack, monsterPrice;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void SetStats(int id, int hp, int attack, int price)
	{
		//GameObject monster =  GameObject.Find("Dungeon").GetComponent<Dungeon> ().monsterList[id];
		//monsterImg.Image = monster.GetComponent<SpriteRenderer>().sprite;
		hpTxt.text = "HP : " + hp.ToString();
		attackTxt.text = "Attack : " + attack.ToString();
		priceTxt.text = price.ToString() + "g";
		monsterID = id;
		monsterHP = hp;
		monsterAttack = attack;
		monsterPrice = price;
	}
}
