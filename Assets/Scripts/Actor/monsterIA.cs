using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterIA : MonoBehaviour {
	public float cooldown;
	public float cooldownTimer;

	Actor currentMonster;
	Actor adventurerFighting;

	GameSpeed speed;

	void Start()
	{
		speed = GameObject.Find ("CanvasDayGeneral").GetComponent<GameSpeed> ();
		currentMonster = GetComponent<Actor> ();
		cooldownTimer = 0;
	}

	void Update()
	{
		if (!currentMonster.fighting) {
			for (int i = 0; i < Dungeon.adventurers.childCount; i++) {
				Transform child = Dungeon.adventurers.GetChild (i);
				Actor childActor = child.GetComponent<Actor> ();
				if (childActor.roomH == currentMonster.roomH && childActor.roomW == currentMonster.roomW) {
					currentMonster.fighting = true;
					adventurerFighting = child.GetComponent<Actor> ();
					cooldown = (float)100 / (float)currentMonster.attackSpeed;
					cooldownTimer = cooldown;
				}
			}
		} else {
			if (cooldownTimer > 0) {
				cooldownTimer -= Time.deltaTime * speed.currentSpeed;
			} else {
				cooldownTimer = 0;
			}
			if (cooldownTimer == 0) {
				currentMonster.Fight (adventurerFighting);
				cooldownTimer = cooldown;
			}
		}
	}
}
