using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedMonstersScript : MonoBehaviour {

	public void OnMouseEnter()
	{
		GetComponent<SpriteRenderer>().color = new Color (0f, 0f, 0f, .5f);
	}

	public void OnMouseExit ()
	{
		GetComponent<SpriteRenderer>().color = new Color (0f, 0f, 0f, 0f);
	}
}
