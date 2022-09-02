using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3[] Forward = new Vector3[4];

    /*
    private Vector3 RightForward = new Vector3();
    private Vector3 LeftForward = new Vector3();

    private Vector3 RightForward = new Vector3();
    private Vector3 LeftForward = new Vector3();
    */

    private List<string> ObstacleList = new List<string>();

    private bool[] Check = new bool[4];

    private float Angle;
    
    void Start()
    {
        ObstacleList.Add("Player");
        ObstacleList.Add("Enemy");
        ObstacleList.Add("Wall");

        Forward[0] = new Vector3(-1.0f, 0.0f, 2.0f);
        Forward[1] = new Vector3(-0.5f, 0.0f, 5.0f);
        Forward[2] = new Vector3(0.5f, 0.0f, 5.0f);
        Forward[3] = new Vector3(1.0f, 0.0f, 2.0f);

        Angle = 0.0f;
    }
    
    void Update()
    {
        RaycastHit hit;

        for (int i = 0; i < 4; ++i)
            Check[i] = false;

        Angle = 0.0f;

        for (int i = 0; i < 4; ++i)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Forward[i]), out hit, Vector3.Distance(transform.position, Forward[i]) + 1.5f))
            {
                //Debug.Log(Mathf.Sqrt(Vector3.Distance(transform.position, Forward[i])));

                foreach(var str in ObstacleList)
                {
                    if (hit.transform.tag == str)
                    {
                        Check[i] = true;

                        switch (i)
                        {
                            case 0:
                                Angle += 1.0f;
                                break;

                            case 1:
                                Angle += 0.5f;
                                break;

                            case 2:
                                Angle -= 0.5f;
                                break;

                            case 3:
                                Angle -= 1.0f;
                                break;
                        }
                    }
                }
            }

            if (Check[i])
                Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(Forward[i])), Color.red);
            else
                Debug.DrawLine(transform.position, transform.position + (transform.TransformDirection(Forward[i])), Color.green);
        }
        transform.Rotate(transform.up, Angle);
    }
}
