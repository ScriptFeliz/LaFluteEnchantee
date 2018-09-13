using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGest : MonoBehaviour {


	void Awake()
	{
		settingAdventurersRoom ();
	}

	// Choisir la salle de départ
	public void settingAdventurersRoom()
	{
		GameObject.Find ("CanvasNightGeneral").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("CanvasDayGeneral").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("CanvasGenePrice").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("CanvasGeneral").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("NotEnoughGold").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("StatsOverlay").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("Environment").GetComponent<Environment>().settingAdventurersRoom = true;
		GameObject.Find ("Environment").GetComponent<Environment>().settingHeart = false;
		GameObject.Find ("Environment").GetComponent<Environment>().addingMonster = false;
		GameObject.Find ("Environment").GetComponent<Environment>().gameOver = false;
		GameObject.Find ("TextOverlay").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("TextOverlay").GetComponent<TextOverlay> ().SetTextOverlay ();
	}
}
