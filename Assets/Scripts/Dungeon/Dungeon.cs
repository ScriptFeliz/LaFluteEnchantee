using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dungeon : MonoBehaviour {
	public static int height, width, columns = 11, rows = 11;
	public static Transform tiles, monsters, adventurers, dungeonOverlay;
	public GameObject tile, staminaGO;
	public GameObject[] floorTiles, wallTiles, doorTiles, adventurerList, monsterList, selectRoomSprites;

	int posx, posy, correctionPosH, correctionPosW;
	float isox, isoy;
	private int rdmTile;
	private int tilesNum;
	private string tileName;

	bool flipTile;

	void Awake()
	{
		height = 3;
		width = 3;
		CreateDungeon ();
	}

	void Start()
	{
		
	}

	public void CreateDungeon ()
	{
		tiles = new GameObject("Tiles").transform;
		dungeonOverlay = new GameObject("DungeonOverlay").transform;
		monsters = new GameObject("Monsters").transform;
		adventurers = new GameObject("Adventurers").transform;

		UpgradeDungeonSize (3,3);

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

	public void UpgradeDungeonSize (int newHeight, int newWidth)
	{
		GameObject tileType;

		int startHeight;
		int startWidth;

		double mid1x, mid2x, mid3x, mid1y, mid2y, mid3y;
		int maxX = newHeight * (columns - 1);
		int maxY = newWidth * (rows - 1);
		mid1x = ((columns) / 2);
		mid2x = mid1x + 1;
		mid3x = mid1x - 1;
		mid1y = ((rows) / 2);
		mid2y = mid1y + 1;
		mid3y = mid1y - 1;

		int floorNum = floorTiles.Length;
		int wallNum = wallTiles.Length;
		int doorNum = doorTiles.Length;

		if (height == newHeight)
		{
			startHeight = 0;
		} else {
			//Upgrading Height

			//On remplace les murs par des portes du côté de l'augmentation de taille
			int h = height - 1;
			int x = columns - 1;
			for (int w = 0; w < newWidth; w++)
			{
				for (int y = 0; y < rows; y++)
				{
					//Portes
					if (y == mid1y || y == mid2y || y == mid3y)
					{
						posx = h * columns + x - h;
						posy = w * rows + y - w;

						isox = posx - posy;
						isoy = (float)(posx + posy) / 2;

						if ((posx != maxX) && (posx != 0) && (posy != maxY) && (posy != 0))
						{
							rdmTile = (int)(Random.Range (0, 1) * doorNum);
							if (rdmTile == doorNum)
							{
								rdmTile = doorNum - 1;
							}
							tileType = doorTiles [rdmTile];

							if (x == columns - 1)
							{
								flipTile = true;
							}
							Destroy(GameObject.Find("Wall_" + h + "_" + w + "_" + x + "_" + y));
							tile = Instantiate (tileType, new Vector3 (isox, isoy, 0), Quaternion.identity) as GameObject;	
							tile.name = "Door_" + h + "_" + w + "_" + x + "_" + y;
							tile.transform.SetParent (tiles);
							if (flipTile) {
								tile.transform.localScale = new Vector3 (-tile.transform.localScale.x, tile.transform.localScale.y, tile.transform.localScale.z);
							}  
						}
					}
				}
			}
			startHeight = height;
		}

		if (width == newWidth)
		{
			startWidth = 0;
		} else {
			//Upgrading Width

			//On remplace les murs par des portes du côté de l'augmentation de taille
			int w = width - 1;
			int y = rows - 1;
			for (int h = 0; h < newHeight; h++)
			{
				for (int x = 0; x < columns; x++)
				{
					//Portes
					if (x == mid1x || x == mid2x || x == mid3x)
					{
						posx = h * columns + x - h;
						posy = w * rows + y - w;

						isox = posx - posy;
						isoy = (float)(posx + posy) / 2;

						if ((posx != maxX) && (posx != 0) && (posy != maxY) && (posy != 0))
						{
							rdmTile = (int)(Random.Range (0, 1) * doorNum);
							if (rdmTile == doorNum)
							{
								rdmTile = doorNum - 1;
							}
							tileType = doorTiles [rdmTile];

							if (x == columns - 1)
							{
								flipTile = true;
							}
							Destroy(GameObject.Find("Wall_" + h + "_" + w + "_" + x + "_" + y));
							tile = Instantiate (tileType, new Vector3 (isox, isoy, 0), Quaternion.identity) as GameObject;	
							tile.name = "Door_" + h + "_" + w + "_" + x + "_" + y;
							tile.transform.SetParent (tiles);
							if (flipTile) {
								tile.transform.localScale = new Vector3 (-tile.transform.localScale.x, tile.transform.localScale.y, tile.transform.localScale.z);
							}  
						}
					}
				}
			}
			startWidth = width;
		}

		for (int h = startHeight; h < newHeight; h++) {
			for (int w = startWidth; w < newWidth; w++) {
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

							tile = Instantiate (tileType, new Vector3 (isox, isoy, 0), Quaternion.identity) as GameObject;
							tile.name = "Floor" + "_" + h + "_" + w + "_" + x + "_" + y;
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
								tileName = "Wall";

								//Portes
								if (((x == mid1x || x == mid2x || x == mid3x) && (y == 0 || y == rows - 1)) || ((y == mid1y || y == mid2y || y == mid3y) && (x == 0 || x == columns - 1))) {
									if ((posx != maxX) && (posx != 0) && (posy != maxY) && (posy != 0)) {
										rdmTile = (int)(Random.Range (0, 1) * doorNum);
										if (rdmTile == doorNum) {
											rdmTile = doorNum - 1;
										}
										tileType = doorTiles [rdmTile];
										tileName = "Door";

										if (x == columns - 1) {
											flipTile = true;
										}
									}
								}

								tile = Instantiate (tileType, new Vector3 (isox, isoy, 0), Quaternion.identity) as GameObject;	
								tile.name = tileName + "_" + h + "_" + w + "_" + x + "_" + y;
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

								if ((newHeight != height) || (newWidth != width))
								{
									tile.GetComponent<Renderer> ().enabled = false;
									tile.GetComponent<PolygonCollider2D> ().enabled = false;
								}
							}
						}
					}
				}
			}
		}

		height = newHeight;
		width = newWidth;
	}
}
