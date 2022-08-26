using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private float fTime;
    //[SerializeField] private GameObject Enemy;
    //[SerializeField] private GameObject EnemyObj;
    [SerializeField] private EnemyController Obj;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.0f);
    }

    void Start()
    {
        //fTime = Random.Range(5.0f, 10.0f);
        fTime = 0;
    }
    
    void Update()
    {
        //fTime -= Time.deltaTime;

        if(fTime <= 0)
        {
            fTime = Random.Range(5.0f, 10.0f);

            // Enemy »ý¼º
            EnemyController cObj = Instantiate(Obj);
            cObj.transform.parent = transform;
            
            cObj.transform.position = transform.position;
        }
    }
}
