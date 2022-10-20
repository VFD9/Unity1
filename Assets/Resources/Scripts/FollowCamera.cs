using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{ 
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 Offset;

    // ** 부드러운 회전
    private Quaternion CameraQuaternion;
    private Vector3 CameraAngles;

    void Start()
    {
        Offset = new Vector3(Target.transform.position.x, 15.0f, Target.transform.position.y -1.5f);
        //transform.Rotate(50.0f, 0.0f, 0.0f);

        //CameraAngles.x = 30.0f;

        //transform.parent = Target.transform;

        CameraQuaternion = new Quaternion();
        CameraAngles = new Vector3(85.0f, 0.0f, 0.0f);
        CameraQuaternion = Quaternion.Euler(CameraAngles);
    }

    void Update()
    {
        Offset = new Vector3(Target.transform.position.x, 15.0f, Target.transform.position.y - 1.5f);

        transform.position = Offset;

        /*
        transform.position = Target.position + Offset;
        // ** 부드러운 이동
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
        */
    }
}
