using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGest : MonoBehaviour {


	void Awake()
	{
		settingAdventurersRoom ();
	}

	// Choisir la salle de départ

	public void settingAdventurersRoom()
	{

		GameObject[] canvas = GameObject.FindGameObjectsWithTag ("Canvas");
		foreach (GameObject curCanvas in canvas) {
			curCanvas.GetComponent<Canvas> ().enabled = false;
		}

		Environment env = GameObject.Find ("Environment").GetComponent<Environment> ();
		env.settingAdventurersRoom = true;
		env.settingHeart = false;
		env.addingMonster = false;
		env.gameOver = false;
		GameObject.Find ("SelectedObject").GetComponent<Image> ().enabled = false;

		GameObject.Find ("TextOverlay").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("TextOverlay").GetComponent<TextOverlay> ().SetTextOverlay ();

		GameObject.Find ("TextOverlay").GetComponent<TextOverlay> ().SetTextOverlay ();

	}
}
