using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GeneratedMonster : MonoBehaviour {
	Environment env;
	Interface Interface;

	public Canvas canvasPriceBidon, canvasNotEnoughGold;
	public Text hpTxt, attackTxt, priceTxt, staminaTxt;
	public int monsterPoolNum;
	public Image monsterImg;
	int monsterDungeonID, monsterHP, monsterHPmax, monsterAttack, monsterPrice, monsterStamina, monsterStaminamax;

	private Canvas generateMonster;

	void Start()
	{	
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
		Interface = GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ();
	}

	void Update()
	{

	}
	public void OnMouseEnter()
	{
		GetComponent<Image>().color = new Color (0f, 0f, 0f, .5f);
	}

	public void OnMouseExit ()
	{
		GetComponent<Image>().color = new Color (0f, 0f, 0f, 0f);
	}

	public void OnMouseClick()
	{
		//On vérifie qu'on a assez d'or pour acheter le monstre
		if (Interface.CanAfford (monsterPrice))
		{	

			//On active l'overlay pour choisir la salle dans laquelle instancier le monstre (c'est dans le contrôle de l'overlay que le monstre est ensuite instancié, voir MouseOverRoom.cs)
			env.addingMonster = true;
			GameObject.Find ("TextOverlay").GetComponent<TextOverlay> ().SetTextOverlay ();
			GameObject.Find ("TextOverlay").GetComponent<Canvas> ().enabled = true;
			GameObject[] gameObjectOverlay = GameObject.FindGameObjectsWithTag ("Overlay");
			foreach (GameObject Overlay in gameObjectOverlay)
			{
				if (Overlay.GetComponentInParent<InfoRoom> ().containsMonster == false)
				{
					Overlay.GetComponent<Renderer> ().enabled = true;
					Overlay.GetComponent<PolygonCollider2D> ().enabled = true;
					Overlay.GetComponent<SpriteRenderer> ().color = new Color (0f, 1f, 0f, .2f);
				}
			}
			GameObject.Find ("GeneratedMonsters").GetComponent<Canvas> ().enabled = false;


			GameObject selectedObject = GameObject.Find ("SelectedObject");
			SelectedObjectProperties properties = selectedObject.GetComponent<SelectedObjectProperties> ();
			properties.monsterDungeonID = monsterDungeonID;
			properties.hp = monsterHP;
			properties.hpmax = monsterHPmax;
			properties.attack = monsterAttack;
			properties.stamina = monsterStamina;
			properties.staminamax = monsterStaminamax;
			properties.value = monsterPrice;
			selectedObject.GetComponent<Image>().sprite = GameObject.Find("Monster" + monsterPoolNum.ToString()).GetComponent<GeneratedMonster>().monsterImg.sprite;

			selectedObject.GetComponent<Image> ().enabled = true;
			Cursor.visible = false;
			GameObject.Find ("StatsOverlay").GetComponent<MouseOverActor> ().SetStats(true,monsterHP,monsterHP,monsterAttack,monsterStamina,monsterStaminamax,0);
			GameObject.Find ("StatsOverlay").GetComponent<Canvas> ().enabled = true;
		} else {
			StartCoroutine (Interface.CantAffordCanvas (canvasPriceBidon, canvasNotEnoughGold));
		}
	}



	public void SetPoolMonster(int dungeonID, int hp, int hpmax, int attack, int price, int stamina, int staminamax, Sprite monsterSprite)
	{
		monsterDungeonID = dungeonID;
		hpTxt.text = "HP : " + hp.ToString();
		attackTxt.text = "Attack : " + attack.ToString();
		staminaTxt.text = "Stamina : " + stamina.ToString();
		priceTxt.text = price.ToString() + "g";
		
		monsterHP = hp;
		monsterHPmax = hpmax;
		monsterAttack = attack;
		monsterPrice = price;
		monsterStamina = stamina;
		monsterStaminamax = staminamax;
		monsterImg.GetComponent<Image>().sprite = monsterSprite;
		
	}

	public void CancelGeneration()
	{
		GameObject.Find ("GeneratedMonsters").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("CanvasNightGeneral").GetComponent<Canvas> ().enabled = true;
		Interface.generationEnabled = true;
		Interface.monsterSelectionEnabled = true;
	}
}
