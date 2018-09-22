using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorUIScript : MonoBehaviour {
	Environment env;
	Interface Interface;

	public bool isSelected;
	bool fadeDown;
	float selectFade;
	public Actor actor;
	public GameObject actorGO;

	// Use this for initialization
	void Start () {
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
		Interface = GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ();
		isSelected = false;
		selectFade = 1;
		actor = GetComponentInParent<Actor> ();
		actorGO = transform.gameObject;
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
		if (Interface.monsterSelectionEnabled)
		{
			if (isSelected == true) {
				Unselect ();
			} else {
				Select ();
			}
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

		if (env.isDay == false)
		{
			moovingMonster ();
		}
	}

	public void Unselect()
	{
		isSelected = false;
		if (actor.isKO)
		{
			selectFade = 0.4f;
		} else
		{
			selectFade = 1;
		}
		actorGO.GetComponent<SpriteRenderer>().color = new  Color(1f,1f,1f,selectFade);
		GameObject.Find ("StatsOverlay").GetComponent<Canvas> ().enabled = false;
		env.RoomOverlayOff();

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

	public void moovingMonster()
	{
		GameObject.Find ("Environment").GetComponent<Environment> ().moovingMonster = true;

		GameObject[] gameObjectOverlay = GameObject.FindGameObjectsWithTag ("Overlay");
		foreach (GameObject Overlay in gameObjectOverlay)
		{
			if (Overlay.GetComponentInParent<InfoRoom>().containsMonster == false) {
				Overlay.GetComponent<Renderer> ().enabled = true;
				Overlay.GetComponent<PolygonCollider2D> ().enabled = true;
				Overlay.GetComponent<SpriteRenderer> ().color = new Color (0f, 1f, 0f, .2f);
			}
		}


		GameObject selectedObject = GameObject.Find ("SelectedObject");
		SelectedObjectProperties properties = selectedObject.GetComponent<SelectedObjectProperties> ();

		Actor actor = GetComponentInParent<Actor> ();
		properties.monsterDungeonID = actor.monsterDungeonID;
		properties.hp = actor.hp;
		properties.hpmax = actor.hpmax;
		properties.attack = actor.attack;
		properties.value = actor.value;
		selectedObject.GetComponent<Image>().sprite = GetComponentInParent<SpriteRenderer>().sprite;
	}


}
