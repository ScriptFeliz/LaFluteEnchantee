using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour {
	
	public static int height = 3, width = 3, columns = 11, rows = 11;
	public static Transform tiles, monsters, adventurers, dungeonOverlay;
	public GameObject tile;
	public GameObject[] floorTiles, wallTiles, doorTiles, adventurerList, monsterList, selectRoomSprites;

	int posx, posy, correctionPosH, correctionPosW;
	float isox, isoy;
	private int rdmTile;
	private int tilesNum;

	bool flipTile;

	void Awake()
	{
		CreateDungeon ();
	}

	void Start()
	{
		
	}

	public void CreateDungeon ()
	{
		GameObject tileType;
		double mid1x, mid2x, mid3x, mid1y, mid2y, mid3y;

		tiles = new GameObject("Tiles").transform;
		monsters = new GameObject("Monsters").transform;
		adventurers = new GameObject("Adventurers").transform;
		dungeonOverlay = new GameObject("DungeonOverlay").transform;

		int maxX = height * (columns - 1);
		int maxY = width * (rows - 1);
		mid1x = ((columns) / 2);
		mid2x = mid1x + 1;
		mid3x = mid1x - 1;
		mid1y = ((rows) / 2);
		mid2y = mid1y + 1;
		mid3y = mid1y - 1;

		int floorNum = floorTiles.Length;
		int wallNum = wallTiles.Length;
		int doorNum = doorTiles.Length;

		for (int h = 0; h < height; h++) {
			for (int w = 0; w < width; w++) {
				for (int x = 0; x < columns; x++) {
					for (int y = 0; y < rows; y++) {
						if (((h > 0) && (x == 0)) || ((w > 0) && (y == 0))) {
							
						} else {
							flipTile = false;

							posx = h * columns + x - h;
							posy = w * rows + y - w;

							isox = posx - posy;
							isoy = (float)(posx + posy) / 2;

							rdmTile = Random.Range (0, floorNum);
							tileType = floorTiles [rdmTile];
							DungeonMap.Map [posx, posy] = "Floor";

							tile = Instantiate (tileType, new Vector3 (isox, isoy, 0), Quaternion.identity) as GameObject;
							tile.transform.SetParent (tiles);
							tile.GetComponent<InfoRoom> ().H = h;
							tile.GetComponent<InfoRoom> ().W = w;

							//Murs
							if (x == 0 || x == columns - 1 || y == 0 || y == rows - 1) {
								rdmTile = (int)(Random.Range (0, 1) * wallNum);
								if (rdmTile == wallNum) {
									rdmTile = wallNum - 1;
								}
								tileType = wallTiles [rdmTile];
								DungeonMap.Map [posx, posy] = "Wall";
						
								//Portes
								if (((x == mid1x || x == mid2x || x == mid3x) && (y == 0 || y == rows - 1)) || ((y == mid1y || y == mid2y || y == mid3y) && (x == 0 || x == columns - 1))) {
									if ((posx != maxX) && (posx != 0) && (posy != maxY) && (posy != 0)) {
										rdmTile = (int)(Random.Range (0, 1) * doorNum);
										if (rdmTile == doorNum) {
											rdmTile = doorNum - 1;
										}
										tileType = doorTiles [rdmTile];
										DungeonMap.Map [posx, posy] = "Door";

										if (x == columns - 1) {
											flipTile = true;
										}
									}
								}
								
								tile = Instantiate (tileType, new Vector3 (isox, isoy, 0), Quaternion.identity) as GameObject;	
								tile.transform.SetParent (tiles);
								if (flipTile) {
									tile.transform.localScale = new Vector3 (-tile.transform.localScale.x, tile.transform.localScale.y, tile.transform.localScale.z);
								}  
							}

							//Select Room Overlay
							if (x == mid1x && y == mid1x) {
								tileType = selectRoomSprites [0];
								tile = Instantiate (tileType, new Vector3 (isox, isoy, 0), Quaternion.identity) as GameObject;
								tile.transform.SetParent (dungeonOverlay);
								tile.GetComponent<InfoRoom> ().H = h;
								tile.GetComponent<InfoRoom> ().W = w;
								tile.GetComponent<SpriteRenderer> ().color = new Color (0f, 1f, 0f, .2f);
							}
						}
					}
				}
			}
		}

		//Lors de la sélection de la salle de départ, on n'affiche pas les salles au centre ou dans les coins.
		GameObject[] gameObjectOverlay = GameObject.FindGameObjectsWithTag ("Overlay");
		foreach (GameObject Overlay in gameObjectOverlay)
		{
			if ((((Overlay.GetComponent<InfoRoom> ().H == 0) || (Overlay.GetComponent<InfoRoom> ().H == height - 1)) &&
				 ((Overlay.GetComponent<InfoRoom> ().W == 0) || (Overlay.GetComponent<InfoRoom> ().W == width - 1))) ||
				((Overlay.GetComponent<InfoRoom>().H > 0) && (Overlay.GetComponent<InfoRoom>().H < height - 1)
				&& (Overlay.GetComponent<InfoRoom>().W > 0) && (Overlay.GetComponent<InfoRoom>().W < width - 1)))
			{
				Overlay.GetComponent<Renderer> ().enabled = false;
				Overlay.GetComponent<PolygonCollider2D> ().enabled = false;
			}
		}
	}
}
