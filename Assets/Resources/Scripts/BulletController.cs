using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// void Update()는 DelatTime에 의해 시간이 지연될 수 있다.
// void FixedUpdate()는 실행순서가 일정한 함수이다.
// void LateUpdate()는 Update() 이후에 실행되는 함수이다.

public class BulletController : MonoBehaviour
{
    //[SerializeField] private float Speed;
    [SerializeField] private Vector3 FirePoint;
    private GameObject BoomObject;

    private void Awake()
    {
        FirePoint = GameObject.Find("FirePoint").transform.position;

        BoomObject = ((GameObject)Resources.Load("Particles/Explosion"));
    }

    private void OnEnable()
    {
        //GameObject EffectObj = Instantiate(BoomEffect);
        //EffectObj.transform.position = FirePoint;
        //Destroy(EffectObj, 0.5f);
    }

    void Start()
    {
        //Speed = 30.0f;
        transform.position = FirePoint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            GameObject Obj = Instantiate(BoomObject);
            Obj.transform.position = this.transform.position;
            Destroy(transform.gameObject);
        }

        if (collision.transform.tag == "Enemy")
        {
            GameObject Obj = Instantiate(BoomObject);
            Obj.transform.position = this.transform.position;
            Destroy(transform.gameObject);
            Destroy(collision.gameObject);
        }
    }
}

/*
const int Max = (180 / 5) + 1;
private float Angle;
private Vector3[] Points = new Vector3[Max];
[SerializeField] private GameObject Point_Obj;

void Start()
{
    Angle = 0.0f;

    for (int i = 0; i < Max; ++i)
    {
        Angle += 5.0f;

        Points[i].x = transform.position.x + Mathf.Cos(Angle * 180.0f / Mathf.PI) * 5.0f;
        Points[i].y = transform.position.y;
        Points[i].z = transform.position.z + Mathf.Sin(Angle * 180.0f / Mathf.PI) * 5.0f;

        GameObject _Obj = Instantiate(Point_Obj);
        _Obj.transform.position = Points[i];
    }
}

void Update()
{
    Angle = 0.0f;

    for (int i = 0; i < Max; ++i)
        Debug.DrawLine(transform.position, Points[i], Color.green);
}
*/