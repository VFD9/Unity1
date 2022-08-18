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

    private void Awake()
    {
        FirePoint = GameObject.Find("FirePoint").transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        Speed = 25.0f;
        transform.position = FirePoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(
            0.0f, 0.0f, Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Cube")
            Destroy(this.gameObject);
    }
}
