using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	//float panBorderThickness = 5f;
	float panSpeed = 10f;
	int size;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 camPos = transform.position;

		/*
		// Contrôle de la caméra par la souris sur la bordure de l'écran
		if (Input.mousePosition.x >= Screen.width - panBorderThickness)
		{
			camPos.x += panSpeed * Time.deltaTime / Time.timeScale;
		}
		if (Input.mousePosition.x <= panBorderThickness) 
		{
			camPos.x -= panSpeed * Time.deltaTime / Time.timeScale;
		}
		if (Input.mousePosition.y >= Screen.height - panBorderThickness) 
		{
			camPos.y += panSpeed * Time.deltaTime / Time.timeScale;
		}
		if (Input.mousePosition.y <= panBorderThickness) 
		{
			camPos.y -= panSpeed * Time.deltaTime / Time.timeScale;
		}
		*/

		//Contrôle de la caméra avec les fleches du clavier
		if (Input.GetKey(KeyCode.RightArrow))
		{
			camPos.x += panSpeed * Time.deltaTime / Time.timeScale;
		}
		if (Input.GetKey(KeyCode.LeftArrow))		{
			camPos.x -= panSpeed * Time.deltaTime / Time.timeScale;
		}
		if (Input.GetKey(KeyCode.UpArrow))		{
			camPos.y += panSpeed * Time.deltaTime / Time.timeScale;
		}
		if (Input.GetKey(KeyCode.DownArrow))		{
			camPos.y -= panSpeed * Time.deltaTime / Time.timeScale;
		}


		transform.position = camPos;


		if (Input.GetAxis ("Mouse ScrollWheel") < 0) 
		{
			size = (int)GetComponent <Camera> ().orthographicSize;
			GetComponent <Camera> ().orthographicSize = size + 1;
		}
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) 
		{
			size = (int)GetComponent <Camera> ().orthographicSize;
			if (size > 2) {
				GetComponent <Camera> ().orthographicSize = size - 1;
			}
		}

		if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Space)) 
		{
			camPos.x = 0;
			camPos.y = 16;
			transform.position = camPos;
			GetComponent <Camera> ().orthographicSize = 17;

		}
	}
}
