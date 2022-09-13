using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /*
    private Vector3 RightForward = new Vector3();
    private Vector3 LeftForward = new Vector3();

    private Vector3 RightForward = new Vector3();
    private Vector3 LeftForward = new Vector3();
    */

    private Vector3[] Forward = new Vector3[4];
    private List<string> ObstacleList = new List<string>();

    //private bool[] Check = new bool[4];
    //private float Angle;

    //--------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------

    private Vector3 Direction = new Vector3();
    [SerializeField] private GameObject NodeList;

    Point Target;

    private void Awake()
	{
        NodeList = GameObject.Find("PointList");
	}

	void Start()
    {
        ObstacleList.Add("Player");
        ObstacleList.Add("Enemy");
        ObstacleList.Add("Wall");

        Forward[0] = new Vector3(-1.0f, 0.0f, 2.0f);
        Forward[1] = new Vector3(-0.5f, 0.0f, 5.0f);
        Forward[2] = new Vector3(0.5f, 0.0f, 5.0f);
        Forward[3] = new Vector3(1.0f, 0.0f, 2.0f);

        //Angle = 0.0f;

        //--------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------

        Direction = new Vector3(0.0f, 0.0f, 0.0f);

        Target = NodeList.transform.GetChild(0).GetComponent<Point>();

        transform.gameObject.GetComponent<Rigidbody>().useGravity = false;

        transform.GetComponent<BoxCollider>().isTrigger = true;
        transform.GetComponent<SphereCollider>().isTrigger = false;
    }

    void Update()
    {
        Move();
        /*
        RaycastHit hit;

        for (int i = 0; i < 4; ++i)
            Check[i] = false;

        Angle = 0.0f;

        for (int i = 0; i < 4; ++i)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Forward[i]), out hit, Vector3.Distance(transform.position, Forward[i]) + 1.5f))
            {
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
        */
    }

    private void Move()
    {
        Direction = (Target.transform.position - transform.position).normalized; // normalized는 방향 벡터로 만들어주는 함수이다.
        transform.position += Direction * 5.0f * Time.deltaTime;
    }

	private void OnTriggerEnter(Collider other)
	{
        if (string.Equals(other.name, Target.transform.name))
            Target = Target.Node;
	}
}
