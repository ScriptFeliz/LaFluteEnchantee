using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIso : MonoBehaviour {
	public GameObject dungeon;
	public GameObject canvasGest;
	public GameObject dungeonMap;

	// Use this for initialization
	void Awake () {
		Instantiate (dungeonMap);	
		Instantiate (dungeon);
		Instantiate (canvasGest);

		GameObject.Find ("CanvasDayGeneral").GetComponent<GameSpeed> ().SetGameSpeed(1);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}

