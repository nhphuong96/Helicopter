using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

	public static float scrollSpeed = 0;


	// Update is called once per frame
	void Update ()
	{
		Vector2 offset = new Vector2 (Time.time * (scrollSpeed / 10), 0);
		GetComponent<Renderer> ().material.mainTextureOffset = offset;
	}
}
