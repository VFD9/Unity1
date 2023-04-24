using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterFightHandler : MonoBehaviour {

	Animator mAnim;
	MonsterMovementHandler mMovement;

	[Tooltip("Target to attack")]
	public GameObject target;

	[Tooltip("Wait after first attack. Enemy wont attack when it is ON ")]
	public bool waitingToAttack = false; 
	[Tooltip("Time to wait after one attack")]
	public float attackDelayTime; 

	[Tooltip("Damage on attack")]
	public float damage = 10;

	float time = 0;

	[Tooltip("Attacking sound")]
	public AudioClip attackSound;

	void Awake(){
	
		mAnim = this.GetComponent<Animator> ();
		mMovement = this.GetComponent<MonsterMovementHandler> ();
	}

    private void Start()
    {
		damage = 10;
    }

    void Update()
	{
		if (time > 0) {
		
			time -= Time.deltaTime;

			if (time <= 0) {
			
				waitingToAttack = false;
				Attack ();
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Bullet")
		{
			SetTarget(PlayerManager.Instance.Player);
		}
	}

	public void Attack()
	{
		if (!waitingToAttack && mMovement.enemyCurState == MonsterMovementHandler.AiState.ATTACK) { //enemy is not in wait mode

			time = attackDelayTime;
			waitingToAttack = true;

			UpdateAnimator ();

			if (PlayerManager.Instance.playerstat.currenthp > 0)
			{
				Manager.Instance.soundManager.PlaySound(attackSound);
				PlayerManager.Instance.playerstat.currenthp -= damage;
			}

			if (PlayerManager.Instance.playerstat.currenthp <= 0)
			{
				PlayerManager.Instance.playerstat.currenthp = 0;
				mMovement.enemyCurState = MonsterMovementHandler.AiState.IDLE;
				mMovement.defaultChaseState = MonsterMovementHandler.AiState.IDLE;
			}

		} 
		else 
			waitingToAttack = false;
	}

	void UpdateAnimator ()
	{
		mAnim.SetTrigger ("Attack");
		//Manager.Instance.gamePlayScript.player.SendMessage ("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
	}

	public void SetTarget(GameObject obj)
	{
		this.target = obj;
		this.mMovement.target = obj.transform;
	}

}
