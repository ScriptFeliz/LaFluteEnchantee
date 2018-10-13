using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : BaseActor {

	public HeartUI heartUI;

	protected override void TakeDamage(int dmg)
	{
		base.TakeDamage (dmg);

		if (hp == 0)
		{
			Die ();
		}
	}

	protected override void Die ()
	{
		base.Die ();
		heartUI.Unselect ();

		env.gameOver = true;
	}
}
