using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Actor : MonoBehaviour {
	public Environment env;
	public ActorUIScript actorUI;

	public bool isMonster;
	public int monsterDungeonID;
	public int hp, hpmax, attack, stamina, staminamax, value;
	public int attackSpeed = 100;
	public int mooveSpeed = 100;
	public GameObject healthBar;
	public GameObject healthGO;

	public int damageDealt;
	public int kills;

	public bool isDead = false;
	public bool isKO = false;
	public bool isFighting = false;
	public bool isHeart = false;

	public int orderModif;

	public int roomH;
	public int roomW;
	public int roomPos;
		
	// Use this for initialization
	void Start () {
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
		actorUI = GetComponentInParent<ActorUIScript> ();
		int x = 0;
		int y = 0;

		hp = hpmax;
		stamina = staminamax;
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

	public void Fight(Actor enemy)
	{
		if ((enemy != null) && (!enemy.isKO))
		{
			enemy.TakeDamage (attack);
			if (enemy.isDead) {
				//enemy.Die ();
				kills += 1;
				isFighting = false;
			}
		} else
		{
			isFighting = false;
		}
	}


	public void TakeDamage(int dmg)
    {
		//Calcul des dégats (les dégats ne peuvent pas être inférieur à 1)
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
			if (tag == "Monster")
			{
				LooseStamina();
			} else {
				Die ();
			}
		}
    }
	
	void LooseStamina()
	{
		stamina -= 1;
		int numStaminaBar = 0;
		for (int i = 0; i < gameObject.transform.childCount; i++) {
			Transform child = gameObject.transform.GetChild (i);
			if (child.name == "Stamina(Clone)")
			{
				numStaminaBar += 1;
				Debug.Log (numStaminaBar);
				Debug.Log (stamina);
				if (numStaminaBar > stamina)
				{
					Debug.Log ("HERE");	
					child.localScale = new Vector3 (0.05f,0.05f,0f);
				}
			}
		}
		/*
		for (int i = 0; i < actor.staminamax; i++) {
			GameObject stamina = Instantiate (GameObject.Find ("Dungeon(Clone)").GetComponent<Dungeon> ().staminaGO) as GameObject;
			stamina.transform.SetParent (gameObject.transform);

			Vector3 posHealthBar = healthGO.transform.localPosition;
			float posStaminaX = (float)(i * 2 - actor.staminamax + 1) / 15;
			stamina.transform.localPosition = new Vector3 (posStaminaX, posHealthBar.y - 0.2f, 0f);

		}*/

		if (stamina > 0) {
			KO ();
		} else {
			Die ();
		}
	}

	public void KO()
	{
		isFighting = false;
		isKO = true;
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.4f);
		healthGO.SetActive(false);
	}

	public void unKO()
	{
		isKO = false;
		gameObject.GetComponent<SpriteRenderer>().color = new Color (1f,1f,1f,1f);
		healthGO.SetActive(true);
	}

	public void Die()
	{
		isDead = true;
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
						break;
					}
				}
			}
				
		}

		if (tag == "Adventurer")
		{
//			int adventurerValue = (int)Mathf.Round(GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ().StatsValue(attack, hpmax));
//			100 par aventurier tué le temps de faire un calcul
			int adventurerValue = 150;
			GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ().addGold (adventurerValue);
			env.adventurersNumber -= 1;
		}

		SelfDestroy();
	}

	public void SelfDestroy()
	{
		Destroy (gameObject);
	}

    

	//Fonction Gestion de la barre de vie
	public void SetHealthBar()
	{
		float myHealth = (float)hp / hpmax;	
		healthBar.transform.localScale = new Vector3 (myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}

	//Fonction Gestion de la barre de vie
	public void SetStaminaBar()
	{
		
		/*
		for (int i = 0; i < staminaGO.;i++)
		{
			//GameObject child = staminaGO.GetChild (i);
		}*/
	}

	public void StatsOverlay()
	{
		GameObject.Find ("StatsOverlay").GetComponent<MouseOverActor> ().SetStats(isMonster,hp,hpmax,attack,stamina,staminamax,kills);
	}


}




