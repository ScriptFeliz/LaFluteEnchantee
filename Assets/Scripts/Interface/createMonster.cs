using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createMonster : MonoBehaviour {

	public Button buttonMonstrow;


	//Instantiate a MONSTROW
	public void newMonster() {


		//Active l'overlay pour choisir la salle dans laquelle instancier le monstre
		GameObject.Find ("Environment").GetComponent<Environment>().addingMonster = true;
		GameObject[] gameObjectOverlay = GameObject.FindGameObjectsWithTag ("Overlay");
		foreach (GameObject Overlay in gameObjectOverlay)
		{
			if (Overlay.GetComponentInParent<InfoRoom>().containsMonster == false) {
				Overlay.GetComponent<Renderer> ().enabled = true;
				Overlay.GetComponent<SpriteRenderer> ().color = new Color (0f, 1f, 0f, .2f);
			}
		}
	}
}
