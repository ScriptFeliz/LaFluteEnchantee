using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOverlay : MonoBehaviour {
	public Text textOverlay;
	Environment env;

	public void SetTextOverlay()
	{
        env = GameObject.Find("Environment").GetComponent<Environment>();

        if (env.settingAdventurersRoom == true) 
		{
			textOverlay.text = "CHOOSE THE ADVENTURERS STARTING ROOM";
		} 

		if (env.settingHeart == true) 
		{
			textOverlay.text = "CHOOSE YOUR HEARTS ROOM";
		}

		if (env.gameOver == true)
		{
			textOverlay.text = "! GAME OVER !" ;
		}
	}
}
