using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
	public static GamePlayController instance;

	private float rate;

	private bool isUpLevel;

	private int score;
	private static int bestScore;

	[SerializeField]
	private Button instructionButton, continueButton;

	[SerializeField]
	private Text scoreText, bestScoreText, gameOverText;

	[SerializeField]
	private Text levelText;

	[SerializeField]
	private Button restartButton;


	// Use this for initialization
	void Awake ()
	{
		rate = 1;
		isUpLevel = false;
		BackgroundScroller.scrollSpeed = 0;	
		_MakeInstance ();
		StartCoroutine (upLevel ("Level 1"));
		if (bestScore != 0) {
			_SetBestScore (bestScore);
		}
	}

	void Update ()
	{
		if (HelicopterController.helicopterInstance.isAlive == true) {
			_upLevel ();
		}
		if (!isUpLevel && BackgroundScroller.scrollSpeed != 0) {
			score++;
			_SetScore (score);
		}

	}

	void _MakeInstance ()
	{
		if (instance == null) {
			instance = this;
		}
	}

	// Update is called once per frame
	public void _InstructionButton ()
	{
		BackgroundScroller.scrollSpeed = 5 * rate;
		Enemy.enemySpeed = 5 * rate;
		HelicopterController.helicopterInstance.speed = 3;
		HelicopterController.helicopterInstance.rbHelicopter.gravityScale = 1;
		EnemySpawner.isSpawning = true;
		EnemySpawner.flag = true;
		isUpLevel = false;
		instructionButton.gameObject.SetActive (false);
	}

	public void _upLevel ()
	{
		if (score == 1000) {
			isUpLevel = true;
			EnemySpawner.flag = false;
			rate = 1.2f;
			score++;
			StartCoroutine (upLevel ("Level 2"));
		} else if (score == 2500) {
			isUpLevel = true;
			EnemySpawner.flag = false;
			rate = 1.5f;
			score++;
			StartCoroutine (upLevel ("Level 3"));
		} else if (score == 4000) {
			isUpLevel = true;
			EnemySpawner.flag = false;
			rate = 1.8f;
			score++;
			StartCoroutine (upLevel ("Level 4"));
		} else if (score == 5500) {
			isUpLevel = true;
			EnemySpawner.flag = false;
			rate = 2.0f;
			score++;
			StartCoroutine (upLevel ("Level 5"));
		} else if (score == 8000) {
			isUpLevel = true;
			EnemySpawner.flag = false;
			rate = 2.2f;
			StartCoroutine (upLevel ("Level 6"));
		} else if (score == 9500) {
			isUpLevel = true;
			EnemySpawner.flag = false;
			rate = 2.5f;
			score++;
			StartCoroutine (upLevel ("Level 7"));
		} else if (score == 12000) {
			isUpLevel = true;
			EnemySpawner.flag = false;
			rate = 3.0f;
			score++;
			StartCoroutine (upLevel ("Level 8"));
		} else if (score == 14000) {
			isUpLevel = true;
			EnemySpawner.flag = false;
			rate = 3.2f;
			score++;
			StartCoroutine (upLevel ("Level 9"));
		}
		if (score == 20000) {
			isUpLevel = true;
			EnemySpawner.flag = false;
			rate = 3.8f;
			score++;
			StartCoroutine (upLevel ("Beast Mode"));
		}



	}

	public void _SetScore (int score)
	{
		this.score = score;
		scoreText.text = "" + score;
	}

	public int _GetScore ()
	{
		return score;
	}

	public void _SetBestScore (int score)
	{
		bestScore = score;
		bestScoreText.text = "" + score;
	}

	public int _GetBestScore ()
	{
		return bestScore;
	}

	public void _RestartGamePlay ()
	{		
		Application.LoadLevel ("GamePlay");
	}

	public void _DisplayGameOver ()
	{
		StartCoroutine (delay ());
	}

	IEnumerator delay ()
	{
		yield return new WaitForSeconds (1);
		gameOverText.gameObject.SetActive (true);
		restartButton.gameObject.SetActive (true);
	}

	IEnumerator upLevel (string level)
	{
		EnemySpawner.isSpawning = false;

		yield return new WaitForSeconds (4);

		levelText.gameObject.SetActive (true);
		levelText.text = level;

		HelicopterController.helicopterInstance.rbHelicopter.gravityScale = 0;
		HelicopterController.helicopterInstance.rbHelicopter.velocity = new Vector2 (0, 0);

		yield return new WaitForSeconds (2);
		levelText.gameObject.SetActive (false);
		instructionButton.gameObject.SetActive (true);
	}

	public void _HideGameOver ()
	{
		gameOverText.gameObject.SetActive (false);
		restartButton.gameObject.SetActive (false);
	}
		
}
