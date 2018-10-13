using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUI : BaseActorUI {
	public Heart heart;

	// Update is called once per frame
	void Update () {
		if (isSelected == true) {
			GameObject.Find ("StatsOverlay").GetComponent<Canvas> ().enabled = true;
			heart.StatsOverlay ();
		}
	}

}
