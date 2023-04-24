using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour
{
	[Tooltip("Furthest distance bullet will look for target")]
	public float maxDistance = 1000;
	RaycastHit hit;
	[Tooltip("Prefab of wall damange hit. The object needs 'LevelPart' tag to create decal on it.")]
	public GameObject decalHitWall;
	[Tooltip("Decal will need to be sligtly infront of the wall so it doesnt cause rendeing problems so for best feel put from 0.01-0.1.")]
	public float floatInfrontOfWall;
	[Tooltip("Blood prefab particle this bullet will create upoon hitting enemy")]
	public GameObject bloodEffect;
	[Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
	public LayerMask ignoreLayer;

	[HideInInspector] public float BulletDamage;
	[HideInInspector] public GameObject BulletTarget;
	[HideInInspector] public GunStyles CurrentGun;
    private Text bulletDamage;
	private GameObject damagePrefab;

	/*
	* Uppon bullet creation with this script attatched,
	* bullet creates a raycast which searches for corresponding tags.
	* If raycast finds somethig it will create a decal of corresponding tag.
	*/

	private void Start()
	{
		if (GameObject.FindGameObjectWithTag("Weapon") != null)
			CurrentGun = GameObject.FindGameObjectWithTag("Weapon").GetComponent<GunScript>().currentStyle;

		damagePrefab = Resources.Load("Prefabs/BulletDamage") as GameObject;
		bulletDamage = damagePrefab.GetComponent<Text>();

		maxDistance = 1000.0f;
		GunDamage(10.0f);
		StartCoroutine("Bullethit");
	}

    IEnumerator Bullethit()
    {
		while (true)
		{
			yield return null;
			if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, ~ignoreLayer))
			{
				if (decalHitWall)
				{
					if (hit.transform.Find("Zombie"))
					{
						BulletTarget = hit.collider.gameObject;
						bulletDamage.text = BulletDamage.ToString();
						Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
						//Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.collider.transform.forward));

						if (BulletTarget.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("IP_Scream_07"))
							bulletDamage.text = "면역";

						BulletTarget.GetComponent<Health>().getDamage(BulletDamage);
						PlayerManager.Instance.damageText.MakeDamageText(BulletTarget.transform.position, BulletDamage);

						Destroy(gameObject);
					}
					else
					{
						Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
						Destroy(gameObject);
					}
				}
				Destroy(gameObject);
			}
			Destroy(gameObject, 0.1f);
		}
	}

	void GunDamage(float Damage)
    {
		if (CurrentGun == GunStyles.automatic)
			BulletDamage = Damage;
		else if (CurrentGun == GunStyles.nonautomatic)
			BulletDamage = (Damage * 3.5f);
	}
}
