using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    private GameObject Target;
    private RectTransform PlayerImage;

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        PlayerImage = GameObject.Find("Image").GetComponent<RectTransform>();
    }

    private void FixedUpdate()
	{
        if (Target != null)
        {
            transform.position = new Vector3(
              Target.transform.position.x,
              100.0f,
              Target.transform.position.z);

            PlayerImage.transform.eulerAngles = new Vector3(Target.transform.rotation.x, 0.0f, Target.transform.rotation.z);
        }
    }
}
