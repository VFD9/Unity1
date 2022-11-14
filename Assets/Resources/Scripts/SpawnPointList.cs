using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointList : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPoint;

    List<GameObject> PointList = new List<GameObject>();

    void Start()
    {
        transform.name = "SpawnPointList";

        for (int i = 0; i < 10; ++i)
        {
            GameObject Obj = Instantiate(SpawnPoint);
            
            Obj.transform.parent = transform;
            Obj.transform.name = "Point" + (i + 1);

            Obj.transform.position = new Vector3(
                Random.Range(-30.0f, -5.0f),
                Random.Range(10.0f, 25.0f),
                Random.Range(-25.0f, -5.0f));

             PointList.Add(Obj);
        }
    }
}
