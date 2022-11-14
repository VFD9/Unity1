using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustumLine : MonoBehaviour
{
	private Camera mainCamera;
	public Shader TargetShader;

	private Vector3[] CameraFrustum = new Vector3[4];
	[SerializeField] private List<MeshRenderer> RendererList = new List<MeshRenderer>();
	[SerializeField] private List<GameObject> CullingList = new List<GameObject>();

	[SerializeField] private LayerMask mask;
	[SerializeField] private float Distance;

	[Range(0.0f, 1.0f)]
	public float X, Y, CX, CY;

	private void Awake()
	{
		X = 0.45f;
		Y = 0.45f;
		CX = 0.1f;
		CY = 0.1f;

		//Distance = 13.0f;
		Distance = 1.0f;

		mainCamera = transform.GetComponent<Camera>();
	}

    private void FixedUpdate()
	{
		mainCamera.CalculateFrustumCorners(
			new Rect(X, Y, CX, CY),
			mainCamera.farClipPlane, // 어디까지 쏘는건지에 대해 표시해주는것(범위)
			Camera.MonoOrStereoscopicEye.Mono,
			CameraFrustum);

		CullingList.Clear();

		for (int i = 0; i < CameraFrustum.Length; ++i)
		{
			var worldSpaceCorner = mainCamera.transform.TransformVector(CameraFrustum[i]).normalized;
			Debug.DrawRay(mainCamera.transform.position, worldSpaceCorner * Distance, Color.black);

			// ** Ray
			Ray ray = new Ray(mainCamera.transform.position, worldSpaceCorner);

			RaycastHit[] hits = Physics.RaycastAll(ray, Distance, mask);

			foreach (RaycastHit hit in hits)
				CullingList.Add(hit.transform.gameObject);  // 레이캐스트인 hits와 부딪히는 오브젝트를 여기에 추가함
		}

		RendererList.Clear();

		foreach (GameObject Element in CullingList)
		{
			if (!Element.GetComponent<FindShader>())
				Element.AddComponent<FindShader>();

			//if(!TargetShader)
			//	TargetShader = Element.GetComponent<MeshRenderer>().sharedMaterial.shader;

			StartCoroutine(FindRenderer(Element));
		}

		foreach (MeshRenderer Element in RendererList) // 플레이어 카메라에 걸리는 객체를 투명하게 만들어줌
		{
			Element.material.shader = Shader.Find("Transparent/VertexLit");

			if (Element.material.HasProperty("_Color"))
			{
				Color color = Element.material.GetColor("_Color");
				
				StartCoroutine(SetColor(Element, color));
			}
		}
	}

	IEnumerator FindRenderer(GameObject _Obj)
	{
		int i = 0;

		do
		{
			if (_Obj.transform.childCount > 0)
				FindRenderer(_Obj.transform.GetChild(i).gameObject);

			MeshRenderer renderer = _Obj.transform.GetComponent<MeshRenderer>();

			if (renderer != null)
				RendererList.Add(renderer);

			i++;
		} while (i < _Obj.transform.childCount);

		yield return null;

		//for (int i = 0; i < _obj.transform.childCount; ++i)
		//{
		//	if (_obj.transform.childCount > 0)
		//		FindRenderer(_obj.transform.GetChild(i).gameObject);
		//
		//	MeshRenderer renderer = _obj.transform.GetChild(i).GetComponent<MeshRenderer>();
		//
		//	if (renderer != null)
		//		RendererList.Add(renderer);
		//}
	}

	IEnumerator Create()
	{
		while (true)
		{
			yield return new WaitForSeconds(1.0f);

			if (transform.childCount >= 1)
				continue;

			RendererList.Clear();

			// ** 게임 오브젝트 변경
			//FindRenderer();

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
		float fTime = 1.0f;

		while (fTime > 0.5f)
		{
			yield return null;

			fTime -= Time.deltaTime * 1.3f;

			meshRenderer.material.SetColor("_Color", new Color(color.r, color.g, color.b, fTime));
		}
	}
}
