using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeed : MonoBehaviour {
	public int currentSpeed;

	public void SetGameSpeed(int speed)
	{
		//Time.timeScale = speed;
		GameObject.Find(currentSpeed.ToString()).GetComponentInChildren<Image> ().color = new Color (0.6f, 0.6f, 0.6f, 1f);
		GameObject.Find(speed.ToString()).GetComponentInChildren<Image> ().color = new Color (0.2f, 0.2f, 0.2f, 1f);
		GameObject.Find(currentSpeed.ToString()).GetComponentInChildren<Text> ().color = new Color (0f, 0f, 0f, 1f);
		GameObject.Find(speed.ToString()).GetComponentInChildren<Text> ().color = new Color (1f, 1f, 1f, 1f);

		currentSpeed = speed;
	}
}
