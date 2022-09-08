using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Point : MonoBehaviour
{
    [HideInInspector] public Point Node;

	private void Awake()
	{
        SphereCollider Coll = transform.GetComponent<SphereCollider>();
        Coll.radius = 0.5f;
	}

	private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.0f);

        if (Node)
            Gizmos.DrawLine(transform.position, Node.transform.position);
    }
}
