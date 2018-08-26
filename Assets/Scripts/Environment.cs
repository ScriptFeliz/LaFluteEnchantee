using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Environment : MonoBehaviour {

	public bool settingAdventurersRoom;
	public bool settingHeart;
	public bool addingMonster;
	public int monsterNum;
	public bool gameOver;

	public int day;

	public int baseAdventurersToInvoke;
	public int adventurersToInvoke;
	public int adventurersNumber;

	public int startingH;
	public int startingW;

	public int heartH;
	public int heartW;

	public int baseAttack;
	public int baseHP;

	public int priceHP;
	public int priceAttack;

	public int gold;

	void Start()
	{
		baseAdventurersToInvoke = 5;
		day = 1;
		baseAttack = 10;
		baseHP = 100;
		priceHP = 1;
		priceAttack = 5;
	}

	void Update()
	{
		if (gameOver) {
			StartCoroutine (GameOver ());
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
		GameObject.Find ("CanvasDay").GetComponent<GameSpeed> ().SetGameSpeed(1);
		GameObject.Find ("CanvasDay").GetComponent<Canvas> ().enabled = false;
		yield return new WaitForSeconds (4);
		SceneManager.LoadScene ("StartScene");
	}
}
