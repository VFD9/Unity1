using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
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

            Debug.DrawLine(transform.position, Points[i], Color.green);

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
}
