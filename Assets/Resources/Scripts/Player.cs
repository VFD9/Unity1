using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// GetAxisRaw는 1, 0, -1을 받는 함수이다.
// GetAxis는 0 ~ 1, -1 ~ 0의 소수점까지 받는 함수이다.
// deltaTime은 프레임과 프레임사이의 간격이다. 

public class Player : MonoBehaviour
{
    
}

/*
    public float Speed;
    public float Rotate;

    [SerializeField] private GameObject FirePointObject;
    //private Animator Anim;
    private GameObject MissilePrefab;
    private GameObject SparkPrefab;
    private GameObject Head;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private LayerMask TargetMask;

    private Vector3 offset;
    private Vector3 Movement;

    private void Awake()
    {
        //Anim = transform.GetComponent<Animator>();

        // ** 마우스 커서를 화면 중앙에서 이동하지 않게 하며, 안보이게 한다.
        //Cursor.lockState = CursorLockMode.Locked;
        
        MissilePrefab = ((GameObject)Resources.Load("Prefabs/Missile"));
        SparkPrefab = ((GameObject)Resources.Load("Particles/SparksHit"));

        //Head = transform.Find("Tower").gameObject;
    }

    void Start()
    {
        //Speed = 5.0f;
        //Rotate = 100.0f;
    }

    void Update()
    {
        //Movement = new Vector3(
        //    Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime,
        //    0.0f,
        //    Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime);
        //
        //transform.position += Movement;

        /*
        // ** 키 입력을 받아온다.

        // fHor * Time.deltaTime * PlayerSpeed
        
        transform.Rotate(
            0.0f,
            fHor * Time.deltaTime * Rotate,
            0.0f);

        transform.Translate(
          0.0f,
          0.0f,
          fVer * Time.deltaTime * Speed);

        //Anim.SetFloat("Speed", fVer);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
             ** 불렛발사
            Anim.SetBool("BoolFire", true);
            GameObject Obj = Instantiate(MissilePrefab);
            Rigidbody Rigid = Obj.transform.GetComponent<Rigidbody>();
            
            SparkPrefab.transform.position = FirePointObject.transform.position;
            
            Obj.transform.LookAt(FirePointObject.transform.forward);
            Rigid.AddForce(FirePointObject.transform.forward * 1500.0f);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Anim.SetBool("BoolFire", false);
        }

        Vector3 TargetPoint = Camera.main.WorldToScreenPoint(transform.position);

        Vector3 Direction = new Vector3((Input.mousePosition - TargetPoint).x, 0.0f,
            (Input.mousePosition - TargetPoint).y).normalized;

        transform.LookAt(Direction + transform.position);


        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("GetMouseButtonDown");
            RaycastHit hit;
        
            if (Physics.Raycast(
                FirePointObject.transform.position,
                FirePointObject.transform.forward,
                out hit, 1000.0f, TargetMask))
        	{
                GameObject Obj = Instantiate(Bullet);
        
                offset = new Vector3(
                    Random.Range(-0.25f, 0.25f),
                    Random.Range(-0.25f, 0.25f),
                    0.0f);
        
                Obj.transform.position = hit.point + offset;
        
        		Debug.DrawLine(FirePointObject.transform.position, hit.point);
        
                Destroy(Obj, 1.5f);
        	}
        }

        if (Input.GetMouseButton(0))
        {

            Head.transform.Rotate(0.0f, MouseX * 200 * Time.deltaTime, 0.0f);

            Quaternion HeadQuaternion = Quaternion.Euler(new Vector3(0.0f, MouseX, 0.0f));

            Head.transform.rotation = Quaternion.Slerp(
                    Head.transform.rotation,
                    HeadQuaternion,
                    Time.deltaTime* 10.0f);
         }
    }
*/