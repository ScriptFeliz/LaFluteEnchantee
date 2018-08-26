using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject Main;

	// Use this for initialization
	void Awake () {
		Instantiate(Main);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
