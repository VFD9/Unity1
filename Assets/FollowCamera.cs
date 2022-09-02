using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{ 
	private void Awake()
    {
        main = GetComponent<Camera>();
    }

    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 Offset;
    private Camera main;

    private Vector3 StartPos = new Vector3();
    private Vector3 EndPos = new Vector3();

    private Vector3 CameraAngles;
    private Quaternion CameraQuaternion;


    void Start()
    {
        Offset = new Vector3(0.0f, 5.0f, -15.0f);
        transform.Rotate(10.0f, 0.0f, 0.0f);

        CameraAngles = new Vector3();
        CameraQuaternion = new Quaternion();

    }

    void Update()
    {
        transform.position = Vector3.Lerp(
              transform.position,
              Target.position + Offset,
              Time.deltaTime * 2.0f);

        if (Input.GetMouseButtonDown(1))
        {
            CameraAngles = transform.rotation.eulerAngles;
        }

        if (Input.GetMouseButton(1))
        {
            CameraAngles.y += Input.GetAxis("Mouse X") * 5.0f;
            CameraQuaternion = Quaternion.Euler(CameraAngles);
        }

        if (Input.GetMouseButtonUp(1))
        {
            CameraAngles.y = 0;
            CameraQuaternion = Quaternion.Euler(CameraAngles);
        }

        transform.rotation = Quaternion.Slerp(
                transform.rotation,
                CameraQuaternion,
                Time.deltaTime * 10.0f);
    }
}
