using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoRoom : MonoBehaviour {

	public int H;
	public int W;
	public bool containsMonster;

	void Awake ()
	{
		containsMonster = false;
	}

}
