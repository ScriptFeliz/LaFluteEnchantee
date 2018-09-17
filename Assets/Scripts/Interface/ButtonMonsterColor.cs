using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonMonsterColor : MonoBehaviour {

	public void OnMouseEnter(int monsterNum)
	{
		GameObject.Find ("Monster" + monsterNum.ToString()).GetComponent<Image> ().color = new Color (0f, 0f, 0f, .2f);
	}

	public void OnMouseExit (int monsterNum)
	{
		GameObject.Find ("Monster" + monsterNum.ToString()).GetComponent<Image> ().color = new Color (0f, 0f, 0f, 0f);
	}
}
