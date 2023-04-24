using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	[Tooltip("Current health")]
	public float curHealth { get; private set; }
	[HideInInspector] public float setcurHealth;

	[Tooltip("Total health")]
	public float totalHealth;

	[Tooltip("Should the object destroy on zero health")]
	public bool destroyOnZeroHealth = true;
	[Tooltip("Delay before destroying")]
	public float destroyDelay = 1;

	[Tooltip("Should their be explosion on destroying")]
	public bool explosionOnDestroy = true;
	[Tooltip("Explosion Prefab")]
	public GameObject[] explosionPrefab;

	[Tooltip("Should a sound play on damage")]
	public bool soundOnHit = true;
	[Tooltip("Damage sound ")]
	public AudioClip hitSound;

	[Tooltip("Should there be sound on death")]
	public bool soundOnDeath = true;
	[Tooltip("Death sound ")]
	public AudioClip deathSound;

	[Tooltip("Should there be effect on damage")]
	public bool effectOnHit = true;
	[Tooltip("Damage effect")]
	public GameObject HitEffect;

	[Tooltip("Components that should be disabled when object has zero health")]
	public Component [] disableComponents;

	public float giveExp;
	int percentage;

	private void Start()
	{
		curHealth = 80;
		setcurHealth = curHealth;
		totalHealth = 80;
		destroyDelay = 1.5f;
	}

	private void Update()
	{
		if (curHealth == 0)
			transform.SetParent(null);
	}

	public void ApplyDamage(float damage)
	{
		giveExp = Random.Range(35, 40);

		if (curHealth <= 0)
			return;

		if (curHealth - damage <= 0) {

			PlayerManager.Instance.playerstat.exp += giveExp;
			PlayerManager.Instance.playerstat.currentexp = giveExp;
			
			curHealth = 0;
			Death ();

		} 
		else 
		{
			if (soundOnHit) 
				Manager.Instance.soundManager.PlaySound (hitSound);

			if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IP_Scream_07"))
				damage = 0;

			curHealth -= damage;
		}
	}

	public float getDamage(float damage)
	{
		giveExp = Random.Range(35, 40);

		if (curHealth <= 0)
			return 0;

		if (curHealth - damage <= 0)
		{
			PlayerManager.Instance.playerstat.exp += giveExp;
			PlayerManager.Instance.playerstat.currentexp = giveExp;

			curHealth = 0;
			Death();
			return 0;
		}
		else
		{
			if (soundOnHit)
				Manager.Instance.soundManager.PlaySound(hitSound);

			if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IP_Scream_07"))
				damage = 0;

			curHealth -= damage;

			return curHealth;
		}
	}

	public void ApplyDamage(ArrayList arr)
	{
		giveExp = Random.Range(35, 40);
		float damage = (float) arr [0];
		Vector3 pos = (Vector3) arr [1];
		GameObject attacker = (GameObject) arr [2];

		if (curHealth <= 0)
			return;

		if (effectOnHit){
			GameObject obj = (GameObject) Instantiate (HitEffect, pos, Quaternion.identity);
			obj.transform.LookAt(PlayerManager.Instance.Player.transform);
		}

		if (soundOnHit)
			Manager.Instance.soundManager.PlaySound (hitSound);

		if (attacker && this.GetComponent<MonsterFightHandler>())
			this.GetComponent<MonsterFightHandler> ().SetTarget (attacker);

		if (curHealth - damage <= 0) 
		{
			curHealth = 0;
			Death ();
		} 
		else
			curHealth -= damage;
	}

	void Death()
	{
		if (explosionOnDestroy) 
		{
			percentage = Random.Range(1, 101);

			//if (percentage >= 1 && percentage < 26)
			//{
			//	GameObject Obj = Instantiate(explosionPrefab[0], transform.position, Quaternion.identity);
			//	Obj.name = "Ammo";
			//	Obj.transform.position = new Vector3(Obj.transform.position.x, 0.2f, Obj.transform.position.z);
			//}
			//else if (percentage >= 36 && percentage < 61)
			//{
			//	GameObject Obj = Instantiate(explosionPrefab[1], transform.position, Quaternion.identity);
			//	Obj.name = "Heal";
			//	Obj.transform.position = new Vector3(Obj.transform.position.x, 0.24f, Obj.transform.position.z);
			//}
			/*else*/ if (percentage >= 1 && percentage < 101)
			{
				GameObject Obj = Instantiate(explosionPrefab[2], transform.position, Quaternion.identity);
				Obj.name = "SpeedBoost";
				Obj.transform.position = new Vector3(Obj.transform.position.x, 0.25f, Obj.transform.position.z);
			}
		}

		if (soundOnDeath)
			Manager.Instance.soundManager.PlaySound (deathSound);

		for (int i = 0; i < disableComponents.Length; i++)
			Destroy (disableComponents [i]);

		if (this.GetComponent<Animator> ()) 
		{
			Animator anim = this.GetComponent<Animator> ();
			anim.applyRootMotion = true;
			anim.SetTrigger ("Dead");
		}

		if (destroyOnZeroHealth)
			Destroy (this.gameObject, destroyDelay);
	}
}
