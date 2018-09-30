using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUIScript : MonoBehaviour {
	/*
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
			stamina.transform.localPosition = new Vector3 (posStaminaX, posHealthBar.y - 0.25f, 0f);
		}

		UpdateStaminaBars ();
	}
	
	public void UpdateStaminaBars()
	{	
		int numStaminaBar = 0;
		for (int i = 0; i < gameObject.transform.childCount; i++) {
			Transform child = gameObject.transform.GetChild (i);
			if (child.name == "Stamina(Clone)")
			{
				numStaminaBar += 1;
				if (numStaminaBar > actor.stamina) 
				{
					child.gameObject.GetComponent<Stamina> ().staminaBar.GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
				} else 
				{
					child.gameObject.GetComponent<Stamina> ().staminaBar.GetComponent<Image> ().color = new Color (0f, 0.7f, 0f, 1f);
				}
			}
		}
	}*/
}
