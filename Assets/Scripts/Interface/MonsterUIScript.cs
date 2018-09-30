using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUIScript : MonoBehaviour {
	Actor actor;
	GameObject healthGO;

	// Use this for initialization
	void Start () {
		actor = GetComponentInParent<Actor> ();
		healthGO = actor.healthGO;
		for (int i = 0; i < actor.staminamax; i++) {
			GameObject stamina = Instantiate (GameObject.Find ("Dungeon(Clone)").GetComponent<Dungeon> ().staminaGO) as GameObject;
			stamina.transform.SetParent (gameObject.transform);

			Vector3 posHealthBar = healthGO.transform.localPosition;
			float posStaminaX = (float)(i * 2 - actor.staminamax + 1) / 15;
			stamina.transform.localPosition = new Vector3 (posStaminaX, posHealthBar.y - 0.2f, 0f);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
			
	}
}
