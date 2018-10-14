using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : BaseActorUI {

	public Monster monster;

	// Update is called once per frame
	void Update () {
		if (isSelected == true) {
			GameObject.Find ("StatsOverlay").GetComponent<Canvas> ().enabled = true;
			monster.StatsOverlay ();
		}
	}

	protected override void Select()
	{
		base.Select();
		if ((env.isDay == false) && (!monster.isHeart))
		{
			movingMonster ();
		}
	}

	public override void Unselect()
	{
		base.Unselect();
		if (monster.isKO)
		{
			selectFade = 0.4f;
		} else
		{
			selectFade = 1;
		}
		env.RoomOverlayOff();
	}

	protected void movingMonster()
	{
		env.movingMonster = true;

		GameObject[] gameObjectOverlay = GameObject.FindGameObjectsWithTag ("Overlay");
		foreach (GameObject Overlay in gameObjectOverlay)
		{
			if (Overlay.GetComponentInParent<InfoRoom>().containsMonster == false) {
				Overlay.GetComponent<Renderer> ().enabled = true;
				Overlay.GetComponent<PolygonCollider2D> ().enabled = true;
				Overlay.GetComponent<SpriteRenderer> ().color = new Color (0f, 1f, 0f, .2f);
			}
		}


		GameObject selectedObject = GameObject.Find ("SelectedObject");
		SelectedObjectProperties properties = selectedObject.GetComponent<SelectedObjectProperties> ();

		properties.monsterDungeonID = monster.monsterDungeonID;
		properties.hp = monster.hp;
		properties.hpmax = monster.hpmax;
		properties.stamina = monster.stamina;
		properties.staminamax = monster.staminamax;
		properties.attack = monster.attack;
		properties.value = monster.value;
		selectedObject.GetComponent<Image>().sprite = GetComponentInParent<SpriteRenderer>().sprite;
	}
}
