using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayGest : MonoBehaviour {
	Environment env;
	bool dayStarted;

	public float timeBeforeNextAdv;
	public int attack, attackSpeed, hp, armor, mooveSpeed;

	// Use this for initialization
	void Start () {
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
		timeBeforeNextAdv = 0f;
		dayStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (dayStarted && env.adventurersNumber == 0 && env.adventurersToInvoke == 0)
		{
			dayStarted = false;
			StartCoroutine (EndDay ());
        }
	}

	public void StartNewDay()
	{
		env.newDay ();
		dayStarted = true;
		GameObject.Find ("CanvasNight").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("CanvasDay").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("CanvasDay").GetComponent<DayInterface>().setDayTxt();
	}

	IEnumerator EndDay()
	{
		SetNextDay ();
		yield return new WaitForSeconds (2);
	
		for (int i = 0; i < Dungeon.monsters.childCount; i++)
		{
			Actor actor = Dungeon.monsters.GetChild (i).GetComponent<Actor>();
			actor.hp = actor.hpmax;
			actor.SetHealthBar();
		}

		GameObject.Find ("CanvasNight").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("CanvasDay").GetComponent<Canvas> ().enabled = false;
	}

	void SetNextDay()
	{
		env.day += 1;
		GameObject.Find ("CanvasNight").GetComponent<Interface> ().SetDayText();
        GameObject.Find ("CanvasNight").GetComponent<Interface> ().MonsterPrice();

        env.baseHP = env.baseHP + (int)(env.baseHP * 0.1);

		// +1 d'attack tous les 2 jours
		env.baseAttack = 10 + (int)Mathf.Round (env.day / 2);

		// + 1 aventurier tous les 3 jours
		env.baseAdventurersToInvoke = 5 + (int)Mathf.Round (env.day / 3);
	}

}
