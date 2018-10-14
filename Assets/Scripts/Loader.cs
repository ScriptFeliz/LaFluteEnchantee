using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {
	public GameObject Main;

	// Use this for initialization
	void Awake () {
		Main = Instantiate (Main) as GameObject;
		Main.name = "Main";
	}
}
