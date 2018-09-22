using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedObjectProperties : MonoBehaviour {
	public int num, monsterDungeonID, hp, hpmax, attack, stamina, staminamax, value;
	public Sprite objectImg;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = (Input.mousePosition) - GameObject.Find("CanvasGeneral").transform.localPosition;
		GetComponentInParent<RectTransform> ().localPosition = new Vector3 (mousePos.x, mousePos.y, mousePos.z);
	}
}
