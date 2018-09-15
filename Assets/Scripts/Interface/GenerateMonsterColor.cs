using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMonsterColor : MonoBehaviour {
	Interface canvasInterface;
	int gold;

	void Start() {
		canvasInterface = GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ();
	}

	void Update () {
		gold = canvasInterface.gold;

		if ((gold - 50) >= 0)
		{
			GameObject.Find ("GenerateMonster").GetComponent<Image> ().color = new Color (0f, 0.7f, 0f, 1f);
		} else {
			GameObject.Find ("GenerateMonster").GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
		}
	}

	public void OnMouseEnter(int monsterNum)
	{
		GameObject.Find ("Button" + monsterNum.ToString()).GetComponent<Image> ().color = new Color (0f, 0f, 0f, .2f);
	}

	public void OnMouseExit (int monsterNum)
	{
		GameObject.Find ("Button" + monsterNum.ToString()).GetComponent<Image> ().color = new Color (0f, 0f, 0f, 0f);	}
}
