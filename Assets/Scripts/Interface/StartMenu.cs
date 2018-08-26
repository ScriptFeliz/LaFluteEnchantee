using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
	public Button startButton;
	public Button exitButton;

	// Use this for initialization
	void Start () {
		//startButton = startButton.GetComponent<Button> ();
		//exitButton = exitButton.GetComponent<Button> ();

	}

	public void StartLevel()
	{
			SceneManager.LoadScene ("Main");
	}

	public void ExitGame()
	{
		Application.Quit ();
	}
}
