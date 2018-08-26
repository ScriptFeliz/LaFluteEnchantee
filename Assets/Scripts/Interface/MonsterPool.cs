using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour {

	public int[] monsterID, hp, attack, price;
	public int rdmMonster;

	// Use this for initialization
	void Start () {
		
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

			rdmMonster = 1;// pour le moment on n'a qu'un type de monstre

			GameObject monster =  GameObject.Find("Dungeon(Clone)").GetComponent<Dungeon> ().monsterList[1];
			Actor actor = monster.GetComponent <Actor> ();

			monsterID[i] = rdmMonster;
			//Pas de randomisation des statistiques pour le moment non plus
			hp [i] = actor.hpmax;
			attack [i] = actor.attack;
			price [i] = actor.value;
			//On affiche à l'écran les stats générées
			GameObject.Find("Monster" + (i + 1).ToString()).GetComponent<SetMonster>().SetStats(monsterID[i], hp[i], attack[i], price[i]);
		}
	}
}
