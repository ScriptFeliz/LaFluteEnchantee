using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour {
	public int orderModif;

	void Start()
	{
		float isox = transform.position.x;
		float isoy = transform.position.y;

		int posx = (int) (2 * isoy + isox)/2;
		int posy = (int) (2 * isoy - isox)/2;

		Renderer renderer = gameObject.GetComponent (typeof(Renderer)) as Renderer;
		renderer.sortingOrder = (int)(-posx*2 - posy*2) + orderModif;
	}
		
}
