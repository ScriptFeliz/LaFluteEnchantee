using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerUI : BaseActorUI {
	public Adventurer adventurer;

	// Update is called once per frame
	void Update () {
		if (isSelected == true) {
			GameObject.Find ("StatsOverlay").GetComponent<Canvas> ().enabled = true;
			adventurer.StatsOverlay ();
		}
	}

}
