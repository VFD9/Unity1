using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTriggerHandler : MonoBehaviour 
{
	MonsterFightHandler mFight;

	void Start()
	{
		mFight = this.GetComponentInParent<MonsterFightHandler> ();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag ("Player"))
			mFight.SetTarget(col.gameObject);
	}
}
