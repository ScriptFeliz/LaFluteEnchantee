using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : BaseActor {
	
	public MonsterUI monsterUI;
	private BaseActor adventurerFighting;
	public int stamina, staminamax, monsterDungeonID;
	public bool isHeart;

	protected override void Start ()
	{
		for (int i = 0; i < staminamax; i++) {
			GameObject stamina = Instantiate (GameObject.Find ("Dungeon").GetComponent<Dungeon> ().staminaGO) as GameObject;
			stamina.transform.SetParent (gameObject.transform);

			Vector3 posHealthBar = healthGO.transform.localPosition;
			float posStaminaX = (float)(i * 2 - staminamax + 1) / 15;
			stamina.transform.localPosition = new Vector3 (posStaminaX, posHealthBar.y - 0.25f, 0f);
		}
		UpdateStaminaBars ();
		base.Start ();

	}

	void Update()
	{
		if (!isHeart) {
			if (!isFighting) {
				for (int i = 0; i < Dungeon.adventurers.childCount; i++) {
					Transform child = Dungeon.adventurers.GetChild (i);
					BaseActor childActor = child.GetComponent<BaseActor> ();
					if (childActor.roomH == roomH && childActor.roomW == roomW) {
						isFighting = true;
						adventurerFighting = child.GetComponent<BaseActor> ();
						cooldown = (float)100 / (float)attackSpeed;
						cooldownTimer = cooldown;
					}
				}
			} else {
				if (!isKO) {
					if (cooldownTimer > 0) {
						cooldownTimer -= Time.deltaTime * speed.currentSpeed;
					} else {
						cooldownTimer = 0;
					}
					if (cooldownTimer == 0) {
						Fight (adventurerFighting);
						cooldownTimer = cooldown;
					}
				}
			}
		} else {
			if (cooldownTimer > 0) {
				cooldownTimer -= Time.deltaTime * speed.currentSpeed;
			} else {
				cooldownTimer = 0;
			}
			if (cooldownTimer == 0) {
				for (int i = 0; i < Dungeon.adventurers.childCount; i++) {
					Transform child = Dungeon.adventurers.GetChild (i);
					Adventurer childActor = child.GetComponent<Adventurer> ();
					if (childActor.roomH == roomH && childActor.roomW == roomW) {
						isFighting = true;
						Fight(child.GetComponent<Adventurer> ());
						cooldown = (float)100 / (float)attackSpeed;
					}
				}

				if (!isFighting)
				{
					Regen ((int)(hpmax / 50));
				}
				cooldownTimer = cooldown;
			}
		}
	}

	void UpdateStaminaBars()
	{	
		int numStaminaBar = 0;
		for (int i = 0; i < gameObject.transform.childCount; i++) {
			Transform child = gameObject.transform.GetChild (i);
			if (child.name == "Stamina(Clone)")
			{
				numStaminaBar += 1;
				if (numStaminaBar > stamina) 
				{
					child.gameObject.GetComponent<Stamina> ().staminaBar.GetComponent<Image> ().color = new Color (1f, 0f, 0f, 1f);
				} else 
				{
					child.gameObject.GetComponent<Stamina> ().staminaBar.GetComponent<Image> ().color = new Color (0f, 0.7f, 0f, 1f);
				}
			}
		}
	}

	protected override void TakeDamage(int dmg)
	{	
		base.TakeDamage (dmg);
		if (hp == 0)
		{
			LooseStamina();
		}
	}

	private void LooseStamina()
	{
		stamina -= 1;
		UpdateStaminaBars ();

		if (stamina > 0) {
			KO ();
		} else {
			Die ();
		}
	}

	public void GainStamina()
	{
		stamina += 1;
		UpdateStaminaBars ();
	}

	private void KO()
	{
		isFighting = false;
		isKO = true;
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.4f);
		for (int i = 0; i < gameObject.transform.childCount; i++) {
			Transform child = gameObject.transform.GetChild (i);
			{
				child.gameObject.SetActive(false);
			}
		}
	}

	public void unKO()
	{
		isKO = false;
		gameObject.GetComponent<SpriteRenderer>().color = new Color (1f,1f,1f,1f);
		for (int i = 0; i < gameObject.transform.childCount; i++) {
			Transform child = gameObject.transform.GetChild (i);
			{
				child.gameObject.SetActive(true);
			}
		}
	}

	protected override void Die ()
	{
		base.Die ();
		monsterUI.Unselect ();

		GameObject[] gameObjectOverlay = GameObject.FindGameObjectsWithTag ("Overlay");
		foreach (GameObject Overlay in gameObjectOverlay) {
			if (Overlay.GetComponent<InfoRoom> ().H == roomH && Overlay.GetComponent<InfoRoom> ().W == roomW) {
				Overlay.GetComponent<InfoRoom> ().containsMonster = false;
				break;
			}
		}

		if (isHeart)
		{
			env.gameOver = true;
		}
	}

	public override void StatsOverlay()
	{
		GameObject.Find ("StatsOverlay").GetComponent<MouseOverActor> ().SetStats (hp, hpmax, attack, stamina, staminamax, kills);
	}


}
