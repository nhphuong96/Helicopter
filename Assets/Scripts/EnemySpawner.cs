using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	[SerializeField]
	private GameObject enemyRespawn;
	public static bool isSpawning = false;

	public static bool flag = false;
	// Use this for initialization
	void Start ()
	{
		//StartCoroutine (Spawner ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isSpawning) {
			return;	
		} else {
			isSpawning = false;
			StartCoroutine (Spawner ());
		}
	}

	IEnumerator Spawner ()
	{
		yield return new WaitForSeconds (Random.Range (1f, 3f));
		Vector3 temp = enemyRespawn.transform.position;
		temp.y = Random.Range (-2.0f, 3.5f);
		temp.x = this.transform.position.x;
		Instantiate (enemyRespawn, temp, Quaternion.identity);
		if (flag == true) {
			StartCoroutine (Spawner ());
		}
	}
}
