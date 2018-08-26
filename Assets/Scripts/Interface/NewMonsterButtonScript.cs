using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NewMonsterButtonScript : MonoBehaviour {

	private Canvas canvasPrice, generateMonster, canvasNotEnoughGold;
	private Interface canvasInterface;

	void Start()
	{
		canvasInterface = GameObject.Find ("CanvasNight").GetComponent<Interface> ();
		canvasPrice = GameObject.Find ("CanvasGenePrice").GetComponent<Canvas> ();
	}
	void Update()
	{

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

	public void OnMouseClick(int monsterNum)
	{
		//On vérifie qu'on a assez d'or pour acheter le monstre
		if (canvasInterface.CanAffordMonster (monsterNum))
		{

			// monsternum == 0 correspond à la génération des 3 monstres que l'on peut acheter
			if (monsterNum == 0) {
				//On retire les 50g au coffre
				canvasInterface.BuyMonster (50);
				GameObject.Find ("GeneratedMonsters").GetComponent<Canvas> ().enabled = true;
				GameObject.Find ("GenerateMonster").GetComponent<MonsterPool> ().GeneratePool ();	

				//On génère les monstres
			} else {
				//On active l'overlay pour choisir la salle dans laquelle instancier le monstre (c'est dans le contrôle de l'overlay que le monstre est ensuite instancié, voir MouseOverRoom.cs)
				GameObject.Find ("Environment").GetComponent<Environment> ().addingMonster = true;
				GameObject.Find ("Environment").GetComponent<Environment> ().monsterNum = monsterNum;
				GameObject[] gameObjectOverlay = GameObject.FindGameObjectsWithTag ("Overlay");
				foreach (GameObject Overlay in gameObjectOverlay) {
					if (Overlay.GetComponentInParent<InfoRoom> ().containsMonster == false) {
						Overlay.GetComponent<Renderer> ().enabled = true;
						Overlay.GetComponent<PolygonCollider2D> ().enabled = true;
						Overlay.GetComponent<SpriteRenderer> ().color = new Color (0f, 1f, 0f, .2f);
					}
				}
				GameObject.Find ("GeneratedMonsters").GetComponent<Canvas> ().enabled = false;
			}

		} else {
			CantAfford (monsterNum);
		}
	}

	public void CancelGeneratedMonsetrs()
	{
		GameObject.Find ("GeneratedMonsters").GetComponent<Canvas> ().enabled = false;

	}

	//Achat impossible
	public void CantAfford(int monsterNum)
	{
		StartCoroutine(CantAffordCanvas (monsterNum));
	}

	IEnumerator CantAffordCanvas(int monsterNum)
	{
		if (monsterNum == 0) {
			canvasNotEnoughGold = GameObject.Find ("NotEnoughGold").GetComponent<Canvas> ();
			canvasPrice.enabled = false;
			canvasNotEnoughGold.enabled = true;
			yield return new WaitForSeconds (1);
			canvasNotEnoughGold.enabled = false;
			canvasPrice.enabled = true;
		} else {
			canvasNotEnoughGold = GameObject.Find ("NotEnoughGold" + monsterNum.ToString ()).GetComponent<Canvas> ();
			canvasNotEnoughGold.enabled = true;
			yield return new WaitForSeconds (1);
			canvasNotEnoughGold.enabled = false;
		}

	}

}
