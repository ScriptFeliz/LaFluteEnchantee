using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIso : MonoBehaviour {
	public GameObject dungeon;
	public GameObject canvasGest;

	// Use this for initialization
	void Awake () {
		dungeon = Instantiate (dungeon) as GameObject;
		dungeon.name = "Dungeon";

		canvasGest = Instantiate (canvasGest) as GameObject;
		canvasGest.name = "CanvasGest";

		GameObject.Find ("CanvasDayGeneral").GetComponent<GameSpeed> ().SetGameSpeed(1);
	}
}

