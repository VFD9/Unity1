using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointList : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPoint;

    List<GameObject> PointList = new List<GameObject>();

    void Start()
    {
        for(int i = 0; i < 10; ++i)
        {
            GameObject Obj = Instantiate(SpawnPoint);

            Obj.transform.position = new Vector3(
                Random.Range(-25.0f, 25.0f),
                0.0f,
                Random.Range(-25.0f, 25.0f));

            PointList.Add(Obj);
        }
    }
}
