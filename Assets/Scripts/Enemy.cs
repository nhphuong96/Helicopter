using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	public static float enemySpeed = 5;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		_EnemyMovement ();
	}

	void _EnemyMovement ()
	{
		Vector3 temp = transform.position;
		temp.x -= enemySpeed * Time.deltaTime;
		transform.position = temp;
	}

	public static float getSpeed ()
	{
		return enemySpeed;
	}

	public static void setSpeed (float speed)
	{
		enemySpeed = speed;
	}
}
