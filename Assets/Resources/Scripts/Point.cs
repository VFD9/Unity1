using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Point : MonoBehaviour
{
    private GameObject Target;

    [SerializeField] private List<MeshRenderer> RendererList = new List<MeshRenderer>();

    [HideInInspector] public Point Node;

    [SerializeField] private LayerMask mask;

	private void Awake()
	{
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.FreezeRotation;

        Target = Manager.Instance.Zombie;
    }
    
	void FindRenderer(GameObject _obj)
	{
        for(int i = 0; i < _obj.transform.childCount; ++i)
		{
            if (_obj.transform.childCount > 0)
                FindRenderer(_obj.transform.GetChild(i).gameObject);

            MeshRenderer renderer = _obj.transform.GetChild(i).GetComponent<MeshRenderer>();

            if (renderer != null)
                RendererList.Add(renderer);
		}
	}

	private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.0f);

        if (Node)
            Gizmos.DrawLine(transform.position, Node.transform.position);
    }
    // Point�� ��ġ Ȯ�ο�

	private void OnCollisionEnter(Collision collision)
	{
        int layer = (1 << collision.gameObject.layer);

        if ((layer & mask) == layer)
        {
            StartCoroutine(Create());
            return;
        }

        transform.position = new Vector3(
                 UnityEngine.Random.Range(-45.0f, -14.0f),
                 UnityEngine.Random.Range(3.0f, 5.0f),
                 UnityEngine.Random.Range(45.0f, 90.0f));

        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }
    
    IEnumerator Create()
	{
        while (true)
        {
            yield return new WaitForSeconds(3.0f);

            if (transform.childCount >= 1)
                continue;

            GameObject Obj = Instantiate(Target);

            Obj.transform.localScale = Vector3.Lerp(Obj.transform.localScale, new Vector3(UnityEngine.Random.Range(1.0f, 1.5f), UnityEngine.Random.Range(1.0f, 2.0f), 1), Time.deltaTime);
            Obj.transform.position = transform.position;
            Obj.transform.parent = transform;
            Obj.name = Target.name;

            RendererList.Clear();
            FindRenderer(Obj);

            foreach (MeshRenderer meshRenderer in RendererList)
            {
                meshRenderer.material.shader = Shader.Find("Transparent/VertexLit");

                if (meshRenderer.material.HasProperty("_Color"))
                {
                    Color color = meshRenderer.material.GetColor("_Color");

                    meshRenderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, 0.0f));

                    StartCoroutine(SetColor(meshRenderer, color));
                }
            }
        }
    }

    IEnumerator SetColor(MeshRenderer meshRenderer, Color color)
	{
        float fTime = 0.0f;

        while (fTime <= 255.0f)
        {
            yield return null;

            fTime += Time.deltaTime;

            meshRenderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, fTime));
        } 
	}
}
