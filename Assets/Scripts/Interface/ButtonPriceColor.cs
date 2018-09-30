using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPriceColor : MonoBehaviour {
	public int price;

	Interface canvasInterface;
	int gold;

	void Start() {
		canvasInterface = GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ();
	}

	void Update () {
		gold = canvasInterface.gold;

		if ((gold - price) >= 0)
		{
			GameObject.Find ("GenerateMonsters").GetComponent<Image> ().color = new Color (0f, 0.7f, 0f, 1f);
		} else {
			GameObject.Find ("GenerateMonsters").GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
		}
	}


}
