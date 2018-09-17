using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorUIScript : MonoBehaviour {
	public bool isSelected;
	bool fadeDown;
	float selectFade;
	public Actor actor;
	public GameObject actorGO;

	// Use this for initialization
	void Start () {
		isSelected = false;
		selectFade = 1;
		actor = GetComponentInParent<Actor> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isSelected == true) {
			GameObject.Find ("StatsOverlay").GetComponent<Canvas> ().enabled = true;
			actor.StatsOverlay ();
		}
	}
	

	public void OnMouseUpAsButton()
	{
		if (isSelected == true) {
			Unselect ();
		} else {
			Select ();
		}
	}

	public void Select()
	{
		for (int i = 0; i < Dungeon.monsters.childCount; i++) {
			selectFade = 1;
			Transform child = Dungeon.monsters.GetChild (i);
			child.GetComponent<ActorUIScript> ().Unselect ();
		}

		isSelected = true;
		StartCoroutine(FadeDown());
	}

	public void Unselect()
	{
		isSelected = false;
		selectFade = 1;
		actorGO.GetComponent<SpriteRenderer>().color = new  Color(1f,1f,1f,selectFade);
		GameObject.Find ("StatsOverlay").GetComponent<Canvas> ().enabled = false;
	}

	public void OnMouseEnter()
	{
	}

	IEnumerator FadeDown()
	{
		if (isSelected) {
			selectFade -= 0.05f;
			yield return new WaitForSeconds (0.05f);
			actorGO.GetComponent<SpriteRenderer> ().color = new  Color (1f, 1f, 1f, selectFade);
			if (selectFade <= 0.6) {
				StartCoroutine (FadeUp ());
			} else {
				StartCoroutine (FadeDown ());
			}
		}
	}

	IEnumerator FadeUp()
	{
		if (isSelected) {
			selectFade += 0.05f;
			yield return new WaitForSeconds (0.05f);
			actorGO.GetComponent<SpriteRenderer> ().color = new  Color (1f, 1f, 1f, selectFade);
			if (selectFade >= 1) {
				StartCoroutine (FadeDown ());
			} else {
				StartCoroutine (FadeUp ());
			}
		}
	}
}
