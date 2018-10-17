using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : BaseActor {
	BaseActor monsterFighting;
	private bool moving = false;
	private bool destOK;

	void Update()
	{
		if (cooldownTimer > 0) {
			cooldownTimer -= Time.deltaTime * speed.currentSpeed;
		} else {
			cooldownTimer = 0;
		}
		if (cooldownTimer == 0) {
			if (!isFighting && !moving)
			{
				// S'il y a des monstres
				if (Dungeon.monsters.childCount > 0)
				{
					// On boucle sur les monstres pour voir s'il y en a un dans la même salle que l'aventurier
					for (int i = 0; i < Dungeon.monsters.childCount; i++)
					{
						Transform child = Dungeon.monsters.GetChild (i);
						if (child.GetComponent<BaseActor> ().roomH == roomH && child.GetComponent<BaseActor> ().roomW == roomW && child.GetComponent<BaseActor>().isKO == false) 
						{
							isFighting = true;
							//moving = false;
							monsterFighting = child.GetComponent<BaseActor> ();
							cooldown = (float)100 / (float)attackSpeed;
							cooldownTimer = cooldown;
						} else {

								moving = true;
								cooldown = (float)100 / (float)mooveSpeed;
								cooldownTimer = cooldown;
						}
					}
				} else
				{
					moving = true;
					cooldown = (float)100 / (float)mooveSpeed;
					cooldownTimer = cooldown;
				}
			} 

			if (isFighting) {
				Fight (monsterFighting);
				cooldownTimer = cooldown;
			} else 
			{
				if (moving) {

					int posx = roomH * 10 + PosXY(roomPos, "x");
					int posy = roomW * 10 + PosXY(roomPos, "y");

					Move ();
					cooldownTimer = cooldown;
				}
			}
		}
	}

	protected override void TakeDamage(int dmg)
	{
		base.TakeDamage (dmg);

		if (hp == 0)
		{
			Die ();
		}
	}

	protected override void Die(){
		base.Die ();
		int adventurerValue = 150;
		GameObject.Find ("CanvasNightGeneral").GetComponent<Interface> ().addGold (adventurerValue);
		env.adventurersNumber -= 1;
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
				roomHdest = roomH;
				roomWdest = roomW - 1;
				roomXdest = 8;
				break;
			case 2:
				roomHdest = roomH - 1;
				roomWdest = roomW;
				roomXdest = 6;
				break;
			case 3:
				roomHdest = roomH;
				roomWdest = roomW + 1;
				roomXdest = 2;
				break;
			case 4:
				roomHdest = roomH + 1;
				roomWdest = roomW;
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
				BaseActor ActorCheck = child.GetComponent<BaseActor> ();
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

		roomH = roomHdest;
		roomW = roomWdest;
		roomPos = roomXdest;

		x = PosXY(roomPos,"x");
		y = PosXY(roomPos,"y");


		int posx = roomH * 10 + x;
		int posy = roomW * 10 + y;

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
