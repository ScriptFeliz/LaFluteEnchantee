using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adventurerSpawner : MonoBehaviour {
	float cooldownTimer;
	public GameObject[] adventurers;
	GameSpeed speed;
	Environment env;

	// Use this for initialization
	void Start () {
		speed = GameObject.Find ("CanvasDayGeneral").GetComponent<GameSpeed> ();
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
		cooldownTimer = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (env.adventurersToInvoke > 0)
		{
			if (cooldownTimer > 0) {
				cooldownTimer -= Time.deltaTime * speed.currentSpeed;
			} else {
				cooldownTimer = 0;
			}
	
			if (cooldownTimer == 0) {
				InvokeAdventurer ();
				if (env.adventurersToInvoke == 0) {
					cooldownTimer = 1;
				} else {
					cooldownTimer = Random.Range (2f, 8f);
				}
			}
		}
	}

	void InvokeAdventurer()
	{
		GameObject adventurer = Instantiate (adventurers[0]) as GameObject;
		Actor actor = adventurer.GetComponent <Actor> ();
		actor.roomH = env.startingH;
		actor.roomW = env.startingW;
		actor.roomPos = 2;
		actor.hpmax = env.baseHP;
		actor.attack = env.baseAttack;
		actor.attackSpeed = 100;
		actor.mooveSpeed = 100;
		adventurer.transform.SetParent (Dungeon.adventurers);
		env.adventurersToInvoke -= 1;
		env.adventurersNumber += 1;
	}
}
