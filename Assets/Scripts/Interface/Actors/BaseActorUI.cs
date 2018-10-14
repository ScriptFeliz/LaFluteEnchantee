using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActorUI : MonoBehaviour {
	protected Environment env;
	Interface Interface;
	public bool isSelected;
	private bool fadeDown;
	protected float selectFade;

	private GameObject actorGO;

	// Use this for initialization
	void Awake () {
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
		Interface = GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ();
		isSelected = false;
		selectFade = 1;
		actorGO = gameObject;
	}
	


	private void OnMouseUpAsButton()
	{
		if (Interface.selectionEnabled)
		{
			if (isSelected == true) {
				Unselect ();
			} else {
				Select ();
			}
		}
	}

	protected virtual void Select()
	{
		env.unselectAll();
		isSelected = true;
		StartCoroutine(FadeDown());
	}

	public virtual void Unselect()
	{
		isSelected = false;

		actorGO.GetComponent<SpriteRenderer>().color = new  Color(1f,1f,1f,selectFade);
		GameObject.Find ("StatsOverlay").GetComponent<Canvas> ().enabled = false;

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
