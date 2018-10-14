using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActor : MonoBehaviour {
	protected Environment env;
	protected GameSpeed speed;

	[SerializeField]
	public int hp, hpmax, attack, value;
	[SerializeField]
	protected GameObject healthBar, healthGO;

	public int attackSpeed = 100;
	public int mooveSpeed = 100;

	protected float cooldown;
	protected float cooldownTimer;

	public int damageDealt, kills;
	public bool isDead = false;
	public bool isKO = false;
	public bool isFighting = false;
	private int orderModif;
	public int roomH, roomW, roomPos;
	private int x, y;



	// Use this for initialization
	protected virtual void Start () {
		env = GameObject.Find ("Environment").GetComponent<Environment> ();
		speed = GameObject.Find ("CanvasDayGeneral").GetComponent<GameSpeed> ();
		cooldownTimer = 0;
		Position ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Position()
	{
		switch (roomPos)
		{
		case 2:
			x = 5;
			y = 2;
			break;
		case 4:
			x = 2;
			y = 5;
			break;
		case 6:
			x = 8;
			y = 5;
			break;
		case 5:
			x = 5;
			y = 5;
			break;
		case 8:
			x = 5;
			y = 8;
			break;
		default:
			x = 5;
			y = 5;
			break;
		}

		int posx = roomH * 10 + x;
		int posy = roomW * 10 + y;

		float isox = posx - posy;
		float isoy = (float)(posx + posy) / 2;

		transform.position = new Vector3 (isox, isoy, transform.position.z);
	}

	protected void Fight(BaseActor enemy)
	{
		if ((enemy != null) && (!enemy.isKO))
		{
			enemy.TakeDamage (attack);
			if (enemy.isDead) {
				kills += 1;
				isFighting = false;
			}
		} else
		{
			isFighting = false;
		}
	}

	protected virtual void TakeDamage(int dmg)
	{
		//Calcul des dégats (les dégats ne peuvent pas être inférieur à 1)
		if (dmg < 1)
		{
			dmg = 1;
		}

		hp -= dmg;
		hp = Mathf.Clamp (hp, 0, hpmax);
		SetHealthBar ();
	}

	protected void Regen(int regen)
	{
		hp += regen;
		hp = Mathf.Clamp (hp, 0, hpmax);
		SetHealthBar ();
	}


	//Fonction Gestion de la barre de vie
	public void SetHealthBar()
	{
		float myHealth = (float)hp / hpmax;	
		healthBar.transform.localScale = new Vector3 (myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}

	protected virtual void Die()
	{
		isDead = true;
		SelfDestroy();
	}

	public void SelfDestroy()
	{
		Destroy (gameObject);
	}

	public virtual void StatsOverlay()
	{
		GameObject.Find ("StatsOverlay").GetComponent<MouseOverActor> ().SetStats(hp,hpmax,attack,0,0,kills);
	}
}
