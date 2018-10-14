using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterPool : MonoBehaviour {

	public int[] monsterID, hp, attack, price;
	public int rdmMonster, gold;
	Interface canvasInterface;
	Environment env;
	public Sprite[] monsterSprite;

	// Use this for initialization
	void Start () {
		canvasInterface = GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ();
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GeneratePool()
	{
		for (int i = 0; i <= 2; i++)
		{

			monsterID = new int[3];
			hp = new int[3];
         	attack = new int[3];
			price = new int[3];
			monsterSprite = new Sprite[3];

			rdmMonster = (int)(Random.Range(1,(env.monsterDiscovered + 1)));

			GameObject monster =  GameObject.Find("Dungeon").GetComponent<Dungeon> ().monsterList[rdmMonster];

			BaseActor baseActor = monster.GetComponent <BaseActor> ();

			monsterID[i] = rdmMonster;
			//Pas de randomisation des statistiques pour le moment non plus
			hp [i] = baseActor.hpmax;
			attack [i] = baseActor.attack;
			price [i] = baseActor.value;

			//On affiche à l'écran les stats générées
			GameObject.Find("Monster" + (i + 1).ToString()).GetComponent<SetMonster>().SetStats(monsterID[i], hp[i], attack[i], price[i]);

			//Gestion de l'image
			monsterSprite [i] = monster.GetComponent<SpriteRenderer>().sprite;

			GameObject.Find("Monster" + (i + 1).ToString()).GetComponent<Image>().sprite = monsterSprite [i];

			//Gestion de la couleur du bouton d'achat
			gold = canvasInterface.gold;

			if ((gold - price [i]) >= 0)
			{
				GameObject.Find ("Button" + (i + 1).ToString()).GetComponent<Image> ().color = new Color (0f, 0f, 0f, 0f);
				GameObject.Find ("ImagePrice" + (i + 1).ToString()).GetComponent<Image>().color = new Color (0f, 0.7f, 0f, 1f);

			} else {
				GameObject.Find ("Button" + (i + 1).ToString()).GetComponent<Image> ().color = new Color (0f, 0f, 0f, 0f);
				GameObject.Find ("ImagePrice" + (i + 1).ToString()).GetComponent<Image>().color = new Color (1f, 0f, 0f, 1f);
			}
		}
	}
}
