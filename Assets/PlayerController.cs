using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GetAxisRaw는 1, 0, -1을 받는 함수이다.
// GetAxis는 0 ~ 1, -1 ~ 0의 소수점까지 받는 함수이다.
// deltaTime은 프레임과 프레임사이의 간격이다. 

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float Rotate;
    
    public GameObject BulletObject;
    [SerializeField] private Animator Anim;

    private void Awake()
    {
        Anim = transform.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Speed = 5;
        Rotate = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // 키 입력을 받아온다.
        float fHor = Input.GetAxisRaw("Horizontal"); // 좌.우 키
        float fVer = Input.GetAxisRaw("Vertical"); // 위.아래 키

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
            // 불렛발사
            Anim.SetBool("BoolFire", true);
            GameObject Obj = Instantiate(BulletObject);
            Rigidbody Rigid = Obj.transform.GetComponent<Rigidbody>();

            Obj.transform.LookAt(transform.forward);
            Rigid.AddForce(transform.forward * 1500.0f);
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            Anim.SetBool("BoolFire", false);
        }
    }
}
