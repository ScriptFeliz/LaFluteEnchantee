using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMap : MonoBehaviour {

	public static string[,] Map ;

	void Awake(){
		Map = new string[999,999];
	}

	// Use this for initialization
	void Start () {
		//int height = (int)Map.GetLongLength(0);
	}

	// Update is called once per frame
	void Update () {
		
	}


	// Augmenter la taille de la matrice quand on augment la taille du donjon. Pour le moment on a un String en entrée (qu'il faut appeler Height ou Width), à voir plus tard si on modifie
	public void Resize(string Dir)
	{
		int height = (int)Map.GetLongLength(0);
		int width = (int)Map.GetLongLength(1);
		int newHeight = height;
		int newWidth = width;

		switch (Dir)
		{
		case "Height":
			newHeight += 5;
			break;
		case "Width":
			newWidth += 5;
			break;
		default:
			Debug.Log ("ERREUR : La valeur envoyée dans DungeonMap.Resize n'est pas valide - " + Dir);
			break;
		}


		// On sauvegarde l'ancienne map
		string[,] savedMap = new string[height,width];
		for (int i = 0; i <= height; i++)
		{
			for (int j = 0; j <= width; j++)
			{
				savedMap [i, j] = Map [i, j];
			}
		}

		// On recréé une map dans laquelle on repositionne les anciennes données
		Map = new string[newHeight,newWidth];

		for (int i = 0; i <= height; i++)
		{
			for (int j = 0; j <= width; j++)
			{
				Map [i, j] = savedMap [i, j];
			}
		}

	}
}
