using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMonsters : MonoBehaviour {
	Interface Interface;
	Environment env;
	Canvas canvasPrice, canvasNotEnoughGold;

	int price;


	public void Start()
	{
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
		Interface = GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ();
		canvasPrice = GameObject.Find ("CanvasPriceGenerate").GetComponent<Canvas>();
		canvasNotEnoughGold = GameObject.Find ("NotEnoughGoldGenerate").GetComponent<Canvas>();
		price = 50;
	}
	// OnMouseEnter et OnMouseExit permettent de gérer la transparence des cases quand on passe dessus
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
		env.unselectAll ();

		//On vérifie qu'on a assez d'or pour acheter le monstre
		if (Interface.CanAfford (price))
		{	
			bool generationEnabled = Interface.generationEnabled;

			if (generationEnabled)
			{
				Interface.removeGold(price);

				GeneratePool ();
				generationEnabled = false;
				Interface.selectionEnabled = false;

				GameObject.Find ("CanvasNightGeneral").GetComponent<Canvas> ().enabled = false;
				GameObject.Find ("GeneratedMonsters").GetComponent<Canvas> ().enabled = true;
			}
		} else {
			StartCoroutine (Interface.CantAffordCanvas (canvasPrice, canvasNotEnoughGold));
		}
	}


	public void GeneratePool()
	{
		for (int i = 0; i <= 2; i++)
		{
			int monsterDungeonID, hpmax, attack, price, staminamax;
			Sprite monsterSprite;

			monsterDungeonID = (int)(Random.Range(1,(env.monsterDiscovered + 1)));

			GameObject monsterGO =  GameObject.Find("Dungeon(Clone)").GetComponent<Dungeon> ().monsterList[monsterDungeonID];
			Monster monster = monsterGO.GetComponent <Monster> ();

			//Pas de randomisation des statistiques pour le moment
			hpmax = monster.hpmax;
			attack = monster.attack;
			staminamax = monster.staminamax;
			price = monster.value;
			monsterSprite = monsterGO.GetComponent<SpriteRenderer>().sprite;

			//On affiche à l'écran les stats générées
			GeneratedMonster monsterGenerating = GameObject.Find("Monster" + (i + 1).ToString()).GetComponent<GeneratedMonster>();
			monsterGenerating.SetPoolMonster(monsterDungeonID, hpmax, attack, price, staminamax, monsterSprite);

			//Gestion de la couleur du bouton d'achat
			int gold = Interface.gold;

			if ((gold - price) >= 0)
			{
				GameObject.Find("Price" + (i + 1).ToString()).GetComponent<Image>().color = new Color (0f, 0.7f, 0f, 1f);
			} else {
				GameObject.Find("Price" + (i + 1).ToString()).GetComponent<Image>().color = new Color (1f, 0f, 0f, 1f);
			}
		}
	}
}
