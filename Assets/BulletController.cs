using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// void Update()는 DelatTime에 의해 시간이 지연될 수 있다.
// void FixedUpdate()는 실행순서가 일정한 함수이다.
// void LateUpdate()는 Update() 이후에 실행되는 함수이다.

public class BulletController : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private Vector3 FirePoint;
    [SerializeField] private GameObject BoomObject;
    [SerializeField] private GameObject BoomEffect;

    private void Awake()
    {
        FirePoint = GameObject.Find("FirePoint").transform.position;
    }

    private void OnEnable()
    {
        GameObject EffectObj = Instantiate(BoomEffect);
        EffectObj.transform.position = FirePoint;
        Destroy(EffectObj, 0.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        Speed = 30.0f;
        transform.position = FirePoint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            GameObject Obj = Instantiate(BoomObject);
            Obj.transform.position = this.transform.position;

            Destroy(Obj, 0.5f);
            Destroy(this.gameObject);
        }
    }
}
