using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Actor : MonoBehaviour {
	public Environment env;
	public ActorUIScript actorUI;

	public bool isMonster;
	public int hpmax;
	public int hp;
    public int attack;
	public int attackSpeed = 100;
	public int mooveSpeed = 100;
	public GameObject healthBar;
	public int value;

	public int damageDealt;
	public int kills;

	public bool isDead = false;
	public bool fighting = false;
	public bool isHeart = false;

	public int orderModif;

	public int roomH;
	public int roomW;
	public int roomPos;
		
	public void Fight(Actor enemy)
	{
		if (enemy != null)
		{
			enemy.TakeDamage (attack);
			if (enemy.isDead) {
				enemy.Die ();
				kills += 1;
				fighting = false;
			}
		} else
		{
			fighting = false;
		}
	}


	public void TakeDamage(int x)
    {
		//Calcul des dégats (les dégats ne peuvent pas être inférieur à 1)
		int dmg = x;
		if (dmg < 1)
		{
			dmg = 1;
		}

		hp -= dmg;

		hp = Mathf.Clamp (hp, 0, hpmax);

		// Calcul et appel de la barre de vie
		SetHealthBar ();

		if (hp == 0)
		{
			isDead = true;
		}
    }

	public void Die()
	{
		//Si c'est un monstre qui est mort, la salle ne contient plus de monstre
		if (tag == "Monster") {
			actorUI.Unselect ();
			if (isHeart) {
				env.gameOver = true;
			} else {
				
				GameObject[] gameObjectOverlay = GameObject.FindGameObjectsWithTag ("Overlay");
				foreach (GameObject Overlay in gameObjectOverlay) {
					if (Overlay.GetComponent<InfoRoom> ().H == roomH && Overlay.GetComponent<InfoRoom> ().W == roomW) {
						Overlay.GetComponent<InfoRoom> ().containsMonster = false;
					}
				}
			}
				
		}

		if (tag == "Adventurer")
		{
//			int adventurerValue = (int)Mathf.Round(GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ().StatsValue(attack, hpmax));
//			100 par aventurier tué le temps de faire un calcul
			int adventurerValue = 100;
			GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ().addGold (adventurerValue);
			env.adventurersNumber -= 1;
		}

		Destroy (gameObject);
	}


    // Use this for initialization
	void Start () {
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
		actorUI = GetComponentInParent<ActorUIScript> ();
		int x = 0;
		int y = 0;

		hp = hpmax;
		kills = 0;

		switch (roomPos)
		{

		case 2:
			x = 5;
			y = 2;
			break;
		case 4:
			x = 2;
			y = 5;
			break;
		case 5:
			x = 5;
			y = 5;
			break;
		case 6:
			x = 8;
			y = 5;
			break;
		case 8:
			x = 5;
			y = 8;
			break;
		default:
			x = 5;
			y = 5;
			break;
		}

		int posx = roomH * 10 + x;
		int posy = roomW * 10 + y;
		DungeonMap.Map [posx, posy] = "Adventurer";

		float isox = posx - posy;
		float isoy = (float)(posx + posy) / 2;

		transform.position = new Vector3 (isox, isoy, transform.position.z);


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Fonction Gestion de la barre de vie
	public void SetHealthBar()
	{
		float myHealth = (float)hp / hpmax;	
		healthBar.transform.localScale = new Vector3 (myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}


	public void StatsOverlay()
	{
		GameObject.Find ("StatsOverlay").GetComponent<MouseOverActor> ().SetStats(isMonster,hp,hpmax,attack,kills);
	}


}




