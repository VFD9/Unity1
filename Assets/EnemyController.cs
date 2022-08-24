using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 RightForward;
    private Vector3 LeftForward;

    private bool RightCheck;
    private bool LeftCheck;

    private float Angle;

    // Start is called before the first frame update
    void Start()
    {
        RightForward = new Vector3(1.0f, 0.0f, 1.0f);
        LeftForward = new Vector3(-1.0f, 0.0f, 1.0f);

        RightCheck = false;
        LeftCheck = false;

        Angle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        RightCheck = false;
        LeftCheck = false;

        if (Physics.Raycast(transform.position, RightForward, out hit, 2.0f))
        {
            if (hit.transform.tag == "Wall")
            {
                RightCheck = true;
                Angle -= 0.5f;
            }
        }

        if (Physics.Raycast(transform.position, LeftForward, out hit, 2.0f))
        {
            if (hit.transform.tag == "Wall")
            {
                LeftCheck = true;
                Angle += 0.5f;
            }
        }

        if(LeftCheck)
            Debug.DrawLine(transform.position, transform.position + (LeftForward * 2.0f), Color.red);
        else
            Debug.DrawLine(transform.position, transform.position + (LeftForward * 2.0f), Color.green);

        if(RightCheck)
            Debug.DrawLine(transform.position, transform.position + (RightForward * 2.0f), Color.red);
        else
            Debug.DrawLine(transform.position, transform.position + (RightForward * 2.0f), Color.green);

        transform.RotateAround(transform.up, transform.position, Angle);
    }
}
