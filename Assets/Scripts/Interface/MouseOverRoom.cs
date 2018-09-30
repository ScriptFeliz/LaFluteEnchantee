﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverRoom: MonoBehaviour {

	Environment env;
	Interface Interface;
	GameObject monster;

	void Start()
	{
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
		Interface = GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ();
	}

	// OnMouseEnter et OnMouseExit permettent de gérer la transparence des cases quand on passe dessus
	public void OnMouseEnter()
	{
		GetComponent<SpriteRenderer>().color = new Color (0f, 1f, 0f, .5f);
	}

	public void OnMouseExit ()
	{
		GetComponent<SpriteRenderer>().color = new Color (0f, 1f, 0f, .2f);
	}

	// Si on clique sur une salle, on instancie le monstre dans cette salle avec les valeurs d'attaque et hp du canvas
	public void OnMouseDown()
	{

		//Si on est en train de placer le coeur
		// Cette condition est placée au dessus de la condition settingAdventurersRoom pour éviter de passer directement dedans une fois que l'on passe en.settingHeart = true (en attendant de trouver mieux ...)
		if (env.settingHeart == true)
		{
			//On enregistre la salle du coeur
			env.heartH = GetComponent<InfoRoom> ().H;
			env.heartW = GetComponent<InfoRoom> ().W;

			env.settingHeart = false;

			//On réactive le Canvas et on enlève le texte de Choix de salle de départ
			GameObject.Find ("CanvasGeneral").GetComponent<Canvas> ().enabled = true;
			GameObject.Find ("CanvasNightGeneral").GetComponent<Canvas> ().enabled = true;
			GameObject.Find ("TextOverlay").GetComponent<Canvas> ().enabled = false;

			//On créé le coeur
			monster = Instantiate(GameObject.Find("Dungeon(Clone)").GetComponent<Dungeon> ().monsterList[0]) as GameObject;

			//monster = Instantiate (monsters [0]) as GameObject;
			Actor actor = monster.GetComponent <Actor> ();
			actor.isHeart = true;
			actor.roomH = GetComponent<InfoRoom> ().H;
			actor.roomW = GetComponent<InfoRoom> ().W;
			monster.transform.SetParent (Dungeon.monsters);
			GetComponentInParent<InfoRoom>().containsMonster = true;

			//On désactive l'overlay
			env.RoomOverlayOff();
		}

		//Si on est en train de choisir la salle de départ
		if (env.settingAdventurersRoom == true) 
		{
			//On enregistre la salle de départ des aventuriers
			env.startingH = GetComponent<InfoRoom> ().H;
			env.startingW = GetComponent<InfoRoom> ().W;

			//On n'est plus en train de choisir la salle de départ
			env.settingAdventurersRoom = false;

			//On ne pourra plus sélectionner cette case donc on la détruit
			Destroy (gameObject);

			//On change la couleur de la salle de départ pour la repérer
			GameObject[] groundTiles = GameObject.FindGameObjectsWithTag("Ground");
			foreach (GameObject Tile in groundTiles)
			{
				if ((Tile.GetComponent<InfoRoom>().H == GetComponent<InfoRoom>().H) && (Tile.GetComponent<InfoRoom>().W == GetComponent<InfoRoom>().W))
					{
						Tile.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f,0.8f,1f);
					}
			}
		
			//On affiche toutes les salles pour choisir la salle du coeur
			GameObject[] gameObjectOverlay = GameObject.FindGameObjectsWithTag ("Overlay");
			foreach (GameObject Overlay in gameObjectOverlay)
			{
				if (Overlay.GetComponentInParent<InfoRoom>().containsMonster == false) {
					Overlay.GetComponent<Renderer> ().enabled = true;
					Overlay.GetComponent<PolygonCollider2D> ().enabled = true;
					Overlay.GetComponent<SpriteRenderer> ().color = new Color (0f, 1f, 0f, .2f);
				}
			}
			 
			//On va choisir la salle du coeur
			env.settingHeart = true;
			GameObject.Find ("TextOverlay").GetComponent<TextOverlay> ().SetTextOverlay ();
		}
				
				//Si on est en train de placer ou déplacer un monstre
		if ((env.addingMonster == true) ||  (env.movingMonster == true))
		{
			SelectedObjectProperties selectedMonster = GameObject.Find ("SelectedObject").GetComponent<SelectedObjectProperties>();
			GameObject monster =  Instantiate (GameObject.Find("Dungeon(Clone)").GetComponent<Dungeon> ().monsterList[selectedMonster.monsterDungeonID]) as GameObject;
			Actor actor = monster.GetComponent <Actor> ();

			if (env.addingMonster == true)
			{
				//On retire le prix au trésor
				Interface.removeGold (selectedMonster.value);
			}


			//On enregistre les données du monstre
			actor.roomH = GetComponent<InfoRoom> ().H;
			actor.roomW = GetComponent<InfoRoom> ().W;
			actor.monsterDungeonID = selectedMonster.monsterDungeonID;
			actor.hp = selectedMonster.hp;
			actor.hpmax = selectedMonster.hpmax;
			actor.stamina = selectedMonster.stamina;
			actor.staminamax = selectedMonster.staminamax;
			actor.attack = selectedMonster.attack;
			actor.value = selectedMonster.value;
			actor.roomPos = 5;
			monster.transform.SetParent (Dungeon.monsters);
			GetComponentInParent<InfoRoom>().containsMonster = true;

			//On désactive l'overlay
			env.RoomOverlayOff();
			env.addingMonster = false;

			//On autorise la génération d'un nouveau monstre
			Interface.generationEnabled = true;
			Interface.monsterSelectionEnabled = true;

			GameObject.Find ("CanvasNightGeneral").GetComponent<Canvas> ().enabled = true;
			GameObject.Find ("TextOverlay").GetComponent<Canvas> ().enabled = false;

			GameObject.Find("SelectedObject").GetComponent<Image> ().enabled = false;
			GameObject.Find ("StatsOverlay").GetComponent<Canvas> ().enabled = false;
			Cursor.visible = true;

			if (env.movingMonster == true)
			{
				for (int i = 0; i < Dungeon.monsters.childCount; i++) {
					Transform child = Dungeon.monsters.GetChild (i);
					if (child.GetComponent<ActorUIScript> ().isSelected)
					{
						Actor selectedActor = child.GetComponent<Actor> ();

						GameObject[] gameObjectOverlay = GameObject.FindGameObjectsWithTag ("Overlay");
						foreach (GameObject Overlay in gameObjectOverlay) {
							if (Overlay.GetComponent<InfoRoom> ().H == selectedActor.roomH && Overlay.GetComponent<InfoRoom> ().W == selectedActor.roomW) {
								Overlay.GetComponent<InfoRoom> ().containsMonster = false;
								break;
							}
						}
						selectedActor.SelfDestroy ();
						break;
					}
				}
				env.movingMonster = false;
			}
		}
	}

	public void InvokeMonster()
	{
		
	}
	public void DeleteMonster()
	{
	}
		
}
