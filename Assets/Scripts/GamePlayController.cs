using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
	public static GamePlayController instance;


	private int score;
	private static int bestScore;

	[SerializeField]
	private Button instructionButton;

	[SerializeField]
	private Text scoreText, bestScoreText, gameOverText;

	[SerializeField]
	private Button restartButton;


	// Use this for initialization
	void Awake ()
	{
		BackgroundScroller.scrollSpeed = 0;	
		_MakeInstance ();
		if (bestScore != 0) {
			_SetBestScore (bestScore);
		}
	}

	void Update ()
	{
		if (BackgroundScroller.scrollSpeed != 0) {
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
		BackgroundScroller.scrollSpeed = 5;
		HelicopterController.helicopterInstance.speed = 3;
		HelicopterController.helicopterInstance.rbHelicopter.gravityScale = 1;
		EnemySpawner.isSpawning = true;
		instructionButton.gameObject.SetActive (false);
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
		//SceneManager.LoadScene ("GamePlay");
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

	public void _HideGameOver ()
	{
		gameOverText.gameObject.SetActive (false);
		restartButton.gameObject.SetActive (false);
	}
		
}
