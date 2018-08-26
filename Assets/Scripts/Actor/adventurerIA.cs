using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adventurerIA : MonoBehaviour {
	public bool moving;
	public float cooldown;
	public float cooldownTimer;
	bool destOK;

	Actor currentAdventurer;
	Actor monsterFighting;

	GameSpeed speed;

	void Start()
	{
		speed = GameObject.Find ("CanvasDay").GetComponent<GameSpeed> ();
		currentAdventurer = GetComponent<Actor> ();
		moving = false;
		cooldown = (float)100 / (float)currentAdventurer.mooveSpeed;
		cooldownTimer = cooldown;
	}

	void Update()
	{
		if (cooldownTimer > 0)
		{
			cooldownTimer -= Time.deltaTime * speed.currentSpeed;
		} else 
		{
			cooldownTimer = 0;
		}

		if (cooldownTimer == 0) {
			if (!currentAdventurer.fighting && !moving)
			{
				// S'il y a des monstres
				if (Dungeon.monsters.childCount > 0)
				{
					// On boucle sur les monstres pour voir s'il y en a un dans la même salle que l'aventurier
					for (int i = 0; i < Dungeon.monsters.childCount; i++)
					{
						Transform child = Dungeon.monsters.GetChild (i);
						if (child.GetComponent<Actor> ().roomH == currentAdventurer.roomH && child.GetComponent<Actor> ().roomW == currentAdventurer.roomW)
						{
							currentAdventurer.fighting = true;
							//moving = false;
							monsterFighting = child.GetComponent<Actor> ();
							cooldown = (float)100 / (float)currentAdventurer.attackSpeed;
							cooldownTimer = cooldown;
						} else {
							moving = true;
							cooldown = (float)100 / (float)currentAdventurer.mooveSpeed;
							cooldownTimer = cooldown;
						}
					}
				} else
				{
					moving = true;
					cooldown = (float)100 / (float)currentAdventurer.mooveSpeed;
					cooldownTimer = cooldown;
				}
			} 

			if (currentAdventurer.fighting) {
				currentAdventurer.Fight (monsterFighting);
				cooldownTimer = cooldown;
			} else 
			{
				if (moving) {

					int posx = currentAdventurer.roomH * 10 + PosXY(currentAdventurer.roomPos, "x");
					int posy = currentAdventurer.roomW * 10 + PosXY(currentAdventurer.roomPos, "y");
					DungeonMap.Map [posx, posy] = "Floor";

					Move ();
					cooldownTimer = cooldown;
				}
			}

		}
	}

	void Move()
	{
		int roomHdest = 0;
		int roomWdest = 0;
		int roomXdest = 5;
		int[] roomHpos;
		int[] roomWpos;
		int[] roomXpos;
		int roomPossibilities = -1;

		int RdmNextPos;
		int x;
		int y;

		roomHpos = new int[4];
		roomWpos = new int[4];
		roomXpos = new int[4];

		for (int i = 1; i <= 4; i++) {
			switch (i) {
			case 1:
				roomHdest = currentAdventurer.roomH;
				roomWdest = currentAdventurer.roomW - 1;
				roomXdest = 8;
				break;
			case 2:
				roomHdest = currentAdventurer.roomH - 1;
				roomWdest = currentAdventurer.roomW;
				roomXdest = 6;
				break;
			case 3:
				roomHdest = currentAdventurer.roomH;
				roomWdest = currentAdventurer.roomW + 1;
				roomXdest = 2;
				break;
			case 4:
				roomHdest = currentAdventurer.roomH + 1;
				roomWdest = currentAdventurer.roomW;
				roomXdest = 4;
				break;
			}



			if (roomHdest >= 0 && roomHdest <= Dungeon.height - 1 && roomWdest >= 0 && roomWdest <= Dungeon.width - 1) {
				roomPossibilities += 1;
				roomHpos [roomPossibilities] = roomHdest;
				roomWpos [roomPossibilities] = roomWdest;
				roomXpos [roomPossibilities] = roomXdest;
			}
		}

		//On choisit une destination random parmis les choix possibles
		RdmNextPos = (Random.Range(0,roomPossibilities + 1)) ;

		//On enregistre cette destination 
		roomHdest = roomHpos [RdmNextPos];
		roomWdest = roomWpos [RdmNextPos];
		roomXdest = roomXpos [RdmNextPos];

		//On vérifie qu'il n'y ait pas déjà un aventurier sur la case de la salle ou l'on veut aller, sinon on modifie jusqu'à ne plus avoir d'aventurier sur une case
		destOK = false;
		while (destOK == false)
		{
			destOK = true;

			for (int i = 0; i < Dungeon.adventurers.childCount; i++) {
				Transform child = Dungeon.adventurers.GetChild (i);
				Actor ActorCheck = child.GetComponent<Actor> ();
				if ((ActorCheck.roomH == roomHdest) && (ActorCheck.roomW == roomWdest) && (ActorCheck.roomPos == roomXdest))
				{
					destOK = false;
					if (roomXdest < 8)
					{
						roomXdest = roomXdest + 2;
					} else
					{
						roomXdest = 2;
					}
					break;
				} 
			}
		}

		currentAdventurer.roomH = roomHdest;
		currentAdventurer.roomW = roomWdest;
		currentAdventurer.roomPos = roomXdest;

		x = PosXY(currentAdventurer.roomPos,"x");
		y = PosXY(currentAdventurer.roomPos,"y");


		int posx = currentAdventurer.roomH * 10 + x;
		int posy = currentAdventurer.roomW * 10 + y;
		DungeonMap.Map [posx, posy] = "Adventurer";

		float isox = posx - posy;
		float isoy = (float)(posx + posy) / 2;

		transform.position = new Vector3 (isox, isoy, transform.position.z);
		moving = false;
	}

	int PosXY(int pos, string xy)
	{
		int x;
		int y;

		switch (pos) {
		case 2:
			x = 5;
			y = 2;
			break;
		case 4:
			x = 2;
			y = 5;
			break;
		case 5:
			x = 5;
			y = 5;
			break;
		case 6:
			x = 8;
			y = 5;
			break;
		case 8:
			x = 5;
			y = 8;
			break;
		default:
			x = 5;
			y = 5;
			break;
		}

		switch(xy)
		{
		case "x" :
			return x;
		case "y" :
			return y;
		default :
			Debug.Log ("ERREUR : La valeur envoyée dans adventurerIA.DecodeRoomPos n'est pas valide - " + xy);
			return 0;
		}

	}
		
}
