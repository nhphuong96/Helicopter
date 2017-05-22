using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Vector3 tempScale = transform.localScale;

		float worldHeight = Camera.main.orthographicSize * 2f;
		float worldWidth = worldHeight * Screen.width / Screen.height;

		tempScale.y = worldHeight;
		tempScale.x = worldWidth;



		transform.localScale = tempScale;
	}

}
