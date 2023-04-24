using System.Collections;
using UnityEngine;

public class DestroyAfterTimeParticle : MonoBehaviour 
{
	[Tooltip("Time to destroy")]
	public float timeToDestroy = 2.0f;
	/*
	* Destroys gameobject after its created on scene.
	* This is used for particles and flashes.
	*/
	void Start () 
	{
		Destroy (gameObject, timeToDestroy);
	}
}
