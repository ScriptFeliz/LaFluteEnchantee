using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayInterface : MonoBehaviour {
	Environment env;
	public Text txtDay;
	public Text txtAdvAlive;
	public Text txtAdvToCome;

	void Start()
	{
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
	}

	void Update()
	{
		int invokedAdv = env.baseAdventurersToInvoke - env.adventurersToInvoke;
		txtAdvAlive.text = "Adventurers Alive : " + env.adventurersNumber + "/" + invokedAdv;
		txtAdvToCome.text = "Adventurers to come : " + env.adventurersToInvoke;
	}

	public void setDayTxt()
	{
		txtDay.text = "DAY " + env.day;
	}
}
