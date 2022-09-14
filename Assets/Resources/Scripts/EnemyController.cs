using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3[] Forward = new Vector3[4];
    private List<string> ObstacleList = new List<string>();

    //--------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------

    private Vector3 Direction = new Vector3();
    [SerializeField] private GameObject NodeList;
    [SerializeField] private Point WayPoint;
    [SerializeField] private GameObject Player;

    private bool TargetColl;

    public float Angle;
    public float fTime;

    private void Awake()
	{
        NodeList = GameObject.Find("PointList");
        Player = GameObject.Find("Tank");
        WayPoint = NodeList.transform.GetChild(0).GetComponent<Point>();
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

        Direction = (WayPoint.transform.position - transform.position).normalized;
        //Direction = new Vector3(0.0f, 0.0f, 0.0f);

        WayPoint = NodeList.transform.GetChild(0).GetComponent<Point>();

        // 중력 해제
        transform.gameObject.GetComponent<Rigidbody>().useGravity = false;

        // isTrigger = 물리적 충돌처리가 진행되지 않음
        transform.GetComponent<BoxCollider>().isTrigger = true;
        transform.GetComponent<SphereCollider>().isTrigger = true;

        // 목표물이 범위내에 포착되었는지 확인
        TargetColl = false;

        Angle = 0.0f;
    }

    void Update()
    {
        /*
        RaycastHit hit;

        Vector3 dir = (Player.transform.position - transform.position).normalized;

        if (!Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity))
                TargetColl = hit.distance < 5.0f ? true : false;
        */

        TargetColl = Vector3.Distance(transform.position, Player.transform.position) < 5.0f ? true : false;

        if (TargetColl)
		{
            // 목표물이 범위내에 있다면 움직임을 멈춤
        }
        else
            Move();
    }

    private void Move()
    {
        // 방향 벡터를 구함
        Direction = (WayPoint.transform.position - transform.position).normalized; // normalized는 방향 벡터로 만들어주는 함수이다.

        // 방향으로 5.0의 속도만큼 움직임
        transform.position += Direction * 5.0f * Time.deltaTime;

        // 타겟을 바라봄
        transform.LookAt(transform.position + Direction);
    }

	private void OnTriggerEnter(Collider other)
	{
        // 충돌이 된 객체가 현재 타겟이 맞는지 확인
        if (string.Equals(other.name, WayPoint.transform.name))
            WayPoint = WayPoint.Node;
	}

    IEnumerator LerpRotation() // yield return 을 사용해줘야 함
	{
        float fTime = 0f;

        while (fTime < 1.5f)
		{
            fTime += Time.deltaTime;
            float fAngle = Mathf.Lerp(transform.eulerAngles.y, Angle, fTime) % 360.0f;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, fAngle, transform.eulerAngles.z);
            Debug.Log(fAngle);

            // yield return new WaitForSeconds(1.0f); 1.0f동안 기다리게 함(Sleep 함수와 동일하다, 하지만 Update() 함수는 계속 돌아감)
            yield return null; // yield return null 은 yield return Time.deltaTime 과 동일하다
        }
    }
}
