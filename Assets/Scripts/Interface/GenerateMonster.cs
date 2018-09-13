using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMonster : MonoBehaviour {

	private Canvas canvasPrice;
	private Interface canvasInterface;

	void Start()
	{
		canvasInterface = GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ();
		canvasPrice = GameObject.Find ("CanvasPrice").GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	// OnMouseEnter et OnMouseExit permettent de gérer la transparence des cases quand on passe dessus
	public void OnMouseEnter()
	{
		canvasPrice.enabled = true;
	}

	public void OnMouseExit ()
	{
		canvasPrice.enabled = false;
	}

	public void OnMouseClick()
	{
		//On vérifie qu'on a assez d'or pour acheter le monstre
		if (canvasInterface.CanAffordMonster (0)) {
			
		} else {
			//canvasInterface.CantAfford (0);
		}
	}
}
