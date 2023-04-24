using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementHandler : MonoBehaviour {

	Animator mAnim;
	MonsterFightHandler mFightHandler;

	[Header("Values")]
	public int score = 10;

	[HideInInspector]
	public float speed = 0;
	public float speedMagnitude = 0.01f;

	[Tooltip("Should the enemy rotate toward target")]
	[Header("Rotation")]
	public bool faceEnemy = true;
	[Tooltip("Rotating speed")]
	public float rotationSpeed = 50;
	[Tooltip("current distance from the target")]
	public float targetDistance = 50;
	[Tooltip("Range in which to attack the target")]
	public float attackDistance = 2;

	[HideInInspector]
	public Transform target;
	public AudioSource ZombieScream;

	private Health ZombieHealth;

	float timer = 0.0f;

	public enum AiState
	{
		IDLE,
		SLOW_CHASE,
		FAST_CHASE,
		ATTACK,
		DIE
	}

	public AiState enemyCurState;
	AiState enemyPrevState;
	public AiState defaultChaseState;

	private void Start()
	{
		mAnim = gameObject.GetComponent<Animator> ();
		mFightHandler = gameObject.GetComponent<MonsterFightHandler> ();
		ZombieHealth = gameObject.GetComponent<Health>();
	}

	private void Update()
	{
		if (enemyCurState == AiState.IDLE)
		{
			if (ZombieHealth.curHealth != ZombieHealth.setcurHealth)
			{
				timer += Time.deltaTime;

				if (timer > 0.1f)
				{
					timer = 0.0f;
					FindTarget();
					gameObject.transform.localRotation = Quaternion.LookRotation(target.position, transform.up);
					ZombieHealth.setcurHealth = ZombieHealth.curHealth;
					mAnim.SetBool("Damaged", false);
				}
				else
				{
					mAnim.SetBool("Damaged", true);
					mAnim.Play("IP_Scream_07");
					mAnim.SetFloat("Speed", 2);

					if (!ZombieScream.gameObject.GetComponent<AudioSource>().isPlaying)
						ZombieScream.PlayDelayed(0.2f);

					return;
				}
			}
		}
	}

	private void FixedUpdate()
	{
		if (!target)
			return;
		else
			FindTarget();

		if (enemyCurState != AiState.DIE) {

			if (!mAnim.applyRootMotion && speed >= 0) {

				this.transform.Translate (0, 0, speed*speedMagnitude);
			}

			CheckAttackStatus ();

			if (faceEnemy)
				FaceEnemy();
		}
	}

	public void SetState(AiState state)
	{
		if (state == enemyCurState || enemyCurState == AiState.DIE) // if both state to be set allready cur state than return
			return;

		enemyPrevState = this.enemyCurState;
		enemyCurState = state;

		switch (state) 
		{
		case AiState.IDLE:

			speed = 0;
			UpdateAnim_Speed ();

			break;

		case AiState.SLOW_CHASE:

			speed = 1;
			UpdateAnim_Speed ();

			break;

		case AiState.FAST_CHASE:

			speed = 2;
			UpdateAnim_Speed ();

			break;

		case AiState.ATTACK:

			speed = 0;
			UpdateAnim_Speed ();

			mFightHandler.Attack ();

			break;
		}
	}

	void UpdateAnim_Speed()
	{
		this.mAnim.SetFloat ("Speed", speed);
	}

	void CheckAttackStatus()
	{
		targetDistance = Vector3.Distance (this.gameObject.transform.position, target.transform.position);

		if (targetDistance <= attackDistance) {
			
			SetState (AiState.ATTACK);
		
		} else {
		
			if(enemyCurState != AiState.DIE)
				SetState (defaultChaseState);
		}
	}

	void FindTarget()
	{
		target = PlayerManager.Instance.Player.transform;
	}

	void FaceEnemy()
	{
		FindTarget();

		Quaternion targetRotation = Quaternion.LookRotation(target.position - this.transform.position, this.transform.up);
		targetRotation.x = 0; targetRotation.z = 0;
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}
}
