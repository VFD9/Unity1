using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GetAxisRaw는 1, 0, -1을 받는 함수이다.
// GetAxis는 0 ~ 1, -1 ~ 0의 소수점까지 받는 함수이다.
// deltaTime은 프레임과 프레임사이의 간격이다. 

public class Player : MonoBehaviour
{
    public float Speed;
    public float Rotate;
    
    [SerializeField] private GameObject FirePointObject;
    private Animator Anim;
    private GameObject MissilePrefab;
    private GameObject SparkPrefab;
    private GameObject Head;

    private void Awake()
    {
        Anim = transform.GetComponent<Animator>();

        // ** 마우스 커서를 화면 중앙에서 이동하지 않게 하며, 안보이게 한다.
        //Cursor.lockState = CursorLockMode.Locked;
        
        MissilePrefab = ((GameObject)Resources.Load("Prefabs/Missile"));
        SparkPrefab = ((GameObject)Resources.Load("Particles/SparksHit"));

        Head = transform.Find("Tower").gameObject;
    }

    void Start()
    {
        Speed = 5;
        Rotate = 100.0f;
    }

    void Update()
    {
        // ** 키 입력을 받아온다.
        float fHor = Input.GetAxisRaw("Horizontal"); // 좌, 우 키
        float fVer = Input.GetAxisRaw("Vertical"); // 위, 아래 키

        // fHor * Time.deltaTime * PlayerSpeed

        transform.Rotate(
            0.0f,
            fHor * Time.deltaTime * Rotate,
            0.0f);

        transform.Translate(
          0.0f,
          0.0f,
          fVer * Time.deltaTime * Speed);
        
        Anim.SetFloat("Speed", fVer);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ** 불렛발사
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

        float MouseX = Input.GetAxis("Mouse X");

        if (Input.GetMouseButton(0))
        {
            Head.transform.Rotate(0.0f, MouseX * 200 * Time.deltaTime, 0.0f);

            /*
            Quaternion HeadQuaternion = Quaternion.Euler(new Vector3(0.0f, MouseX, 0.0f));

            Head.transform.rotation = Quaternion.Slerp(
                    Head.transform.rotation,
                    HeadQuaternion,
                    Time.deltaTime* 10.0f);
            */
        }
    }
}
