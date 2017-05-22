using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelicopterController : MonoBehaviour
{
	public static HelicopterController helicopterInstance;

	public int speed;
	private bool isClipFlyPlaying;

	public Rigidbody2D rbHelicopter;
	public Animator animator;

	[SerializeField]
	private AudioSource audioSource;
		
	[SerializeField]
	private AudioClip FlyClip, ExplosionClip;

	[SerializeField]
	private Button instructionButton;


	private bool isAlive, didFlap, isReady;

	public bool isRespawn;


	// Use this for initialization
	void Awake ()
	{
		isClipFlyPlaying = false;
		isAlive = true;
		isReady = false;
		isRespawn = false;
		instructionButton.gameObject.SetActive (false);
		rbHelicopter = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		_MakeInstance ();
	}

	void Start ()
	{
		//transform.position = new Vector3 (0, 0, 100);
	}


	void _MakeInstance ()
	{

		if (helicopterInstance == null) {
			helicopterInstance = this;
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (!isClipFlyPlaying) {
			audioSource.clip = FlyClip;
			audioSource.Play ();
			isClipFlyPlaying = true;
		}
		if (!audioSource.isPlaying) {
			isClipFlyPlaying = false;
		}
		if (isReady) {
			_HelicopterMovement ();
		} else {
			_TakeOff ();
		}
	}

	void _TakeOff ()
	{
		if (transform.position.y < 2) {
			transform.position = new Vector2 (-6, Time.timeSinceLevelLoad - 3);
		} else {
			instructionButton.gameObject.SetActive (true);
			isReady = true;
		}
	}

	void _HelicopterMovement ()
	{
		if (isAlive) {
			if (didFlap) {
				didFlap = false;
				rbHelicopter.velocity = new Vector2 (rbHelicopter.velocity.x, speed);
			}
		}
	}

	public void _Fly ()
	{
		didFlap = true;
	}

	void OnTriggerEnter2D (Collider2D target)
	{
		rbHelicopter.gravityScale = 0;
		rbHelicopter.velocity = new Vector2 (0, 0);
		if (target.gameObject.tag == "Enemy") {
			_Explosion ();
		}
	}

	void OnCollisionEnter2D (Collision2D target)
	{
		
		if (target.gameObject.tag == "Ground" && isAlive == true && isReady == true) {
			_Explosion ();
		}
	}

	void _SetupScore ()
	{
		if (GamePlayController.instance._GetBestScore () < GamePlayController.instance._GetScore ()) {
			GamePlayController.instance._SetBestScore (GamePlayController.instance._GetScore ());
		}
	}


	void _Explosion ()
	{
		animator.SetTrigger ("Explosion");
		audioSource.PlayOneShot (ExplosionClip);
		float animationLength, soundLength, timeDelayingDestroy;
		animationLength = animator.GetCurrentAnimatorStateInfo (0).length;
		soundLength = ExplosionClip.length;
		timeDelayingDestroy = animationLength;
		if (animationLength < soundLength) {
			timeDelayingDestroy = soundLength;
		}
		Destroy (gameObject, timeDelayingDestroy);
		isAlive = false;
		BackgroundScroller.scrollSpeed = 0;
		EnemySpawner.isSpawning = false;
		_SetupScore ();
		GamePlayController.instance._DisplayGameOver ();
	}
}
