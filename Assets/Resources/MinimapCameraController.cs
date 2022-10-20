using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
	private void FixedUpdate()
	{
        transform.position = new Vector3(
            transform.position.x,
            100.0f,
            transform.position.z);
    }
}
