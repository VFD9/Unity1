using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public void MakeDamageText(Vector3 _position, float _damage)
    {
		Vector3 offset = new Vector3(Random.Range(-0.7f, 0.7f), Random.Range(1.7f, 2.0f), 0.2f);

		GameObject Obj = Instantiate(PlayerManager.Instance.damagePrefab);

		Obj.transform.SetParent(GameObject.Find("DamageCanvas").transform);
		Obj.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

		Obj.transform.position = new Vector3(
			_position.x + offset.x,
			_position.y + offset.y,
			_position.z + offset.z);

		StartCoroutine(moveText(Obj));
	}

	public IEnumerator moveText(GameObject _Obj)
    {
		float timer = 0.0f;
		while (true)
		{
			yield return null;
			timer += Time.deltaTime;

			if (timer > 0.5f)
			{
				timer = 0.0f;
				Destroy(_Obj.gameObject);
				break;
			}
			else
			{
				_Obj.transform.Translate(0, 2.0f * Time.deltaTime, 0);
				ChangeAlpha(_Obj);
			}
		}
	}

	public void ChangeAlpha(GameObject _Obj)
    {
		_Obj.GetComponent<Text>().color = new Color(
				_Obj.GetComponent<Text>().color.r,
				_Obj.GetComponent<Text>().color.g,
				_Obj.GetComponent<Text>().color.b,
				_Obj.GetComponent<Text>().color.a - 2.5f * Time.deltaTime);
	}

	public void UpAlpha(GameObject _Obj)
	{
		_Obj.GetComponent<Text>().color = new Color(
				_Obj.GetComponent<Text>().color.r,
				_Obj.GetComponent<Text>().color.g,
				_Obj.GetComponent<Text>().color.b,
				_Obj.GetComponent<Text>().color.a + 2.5f * Time.deltaTime);
	}
}
