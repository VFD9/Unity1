using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 Offset;
    private Camera main;

    Vector3 StartPos = new Vector3();
    Vector3 EndPos = new Vector3();

    private void Awake()
	{
        main = GetComponent<Camera>();
    }

	void Start()
    {
        Offset = new Vector3(0.0f, 5.0f, -15.0f);
        transform.Rotate(10.0f, 0.0f, 0.0f);

        transform.parent = Target.transform;
    }

    void Update()
    {
        // 측면으로 돌릴때 부드럽게 이동하도록 코드 작성하기

        //부드러운 이동
        transform.position = Vector3.Lerp(
            transform.position,
            Target.position + Offset,
            Time.deltaTime * 2.0f);

        //transform.LookAt(Target);

        // 부드러운 회전
        Vector3 CameraAngles = transform.rotation.eulerAngles;
        CameraAngles.y += Input.GetAxis("Mouse X") * 10.0f;

        Quaternion CameraQuaternion = Quaternion.Euler(CameraAngles);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            CameraQuaternion,
            Time.deltaTime * 10.0f);

        //transform.RotateAround(Target.position, Vector3.up, CameraQuaternion.y);
        //transform.LookAt(Target);

        //if (Input.GetMouseButtonDown(1))
        //{
        //    StartPos = Input.mousePosition;
        //
        //if (Input.GetMouseButton(1))
        //{
        //    EndPos = Input.mousePosition;
        //}
        //
        //if (Input.GetMouseButtonUp(1))
        //{
        //    EndPos = Input.mousePosition;
        //
        //    StartPos = new Vector3(0.0f, 0.0f, 0.0f);
        //    EndPos = new Vector3(0.0f, 0.0f, 0.0f);
        //}

        Debug.Log(StartPos + " , " + EndPos);
    }
}
