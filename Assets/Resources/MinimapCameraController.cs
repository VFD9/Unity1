using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    public GameObject Target;
    public Transform PlayerImage;

    private void FixedUpdate()
	{
        if (Target != null)
        {
            transform.position = new Vector3(
              Target.transform.position.x,
              100.0f,
              Target.transform.position.z);

            PlayerImage.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -Target.transform.localEulerAngles.y));
        }
    }
}
