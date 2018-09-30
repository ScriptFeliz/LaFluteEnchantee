using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Environment : MonoBehaviour {

	public bool settingAdventurersRoom, settingHeart, addingMonster, movingMonster, isDay, gameOver = false;

	public int day;

	public int monsterDiscovered;

	public int baseAdventurersToInvoke, adventurersToInvoke, adventurersNumber;

	public int startingH, startingW;
	public int heartH, heartW;

	public int baseAttack, baseHP;

	public int priceHP, priceAttack;

	void Start()
	{
		baseAdventurersToInvoke = 5;
		day = 1;
		baseAttack = 10;
		baseHP = 100;
		priceHP = 1;
		priceAttack = 5;
		monsterDiscovered = 1;
		isDay = false;
	}

	void Update()
	{
		if (gameOver) {
			StartCoroutine (GameOver ());
		}

		if (Input.GetKeyDown (KeyCode.Escape))
		{
			unselectAll ();
		}

	}

	public void unselectAll()
	{
		for (int i = 0; i < Dungeon.monsters.childCount; i++) {
			Transform child = Dungeon.monsters.GetChild (i);
			if (child.GetComponent<ActorUIScript> ().isSelected)
			{
				child.GetComponent<ActorUIScript> ().Unselect ();
			}
		}	
	}

	public void RoomOverlayOff()
	{
		// Désactiver l'overlay
		GameObject[] gameObjectOverlay = GameObject.FindGameObjectsWithTag ("Overlay");
		foreach (GameObject Overlay in gameObjectOverlay)
		{
			Overlay.GetComponent<Renderer> ().enabled = false;
			Overlay.GetComponent<PolygonCollider2D> ().enabled = false;
		}
	}

	public void newDay()
	{
		adventurersNumber = 0;
		adventurersToInvoke = baseAdventurersToInvoke;
	}

	IEnumerator GameOver()
	{
		GameObject.Find ("TextOverlay").GetComponent<Canvas> ().enabled = true;
		GameObject.Find ("TextOverlay").GetComponent<TextOverlay> ().SetTextOverlay ();
		GameObject.Find ("CanvasDayGeneral").GetComponent<GameSpeed> ().SetGameSpeed(1);
		GameObject.Find ("CanvasDayGeneral").GetComponent<Canvas> ().enabled = false;
		GameObject.Find ("CanvasGeneral").GetComponent<Canvas> ().enabled = false;
		yield return new WaitForSeconds (4);
		SceneManager.LoadScene ("StartScene");
	}
}
