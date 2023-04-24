using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowZombieHP : MonoBehaviour
{
	[SerializeField] private GameObject hpCanvas;
	[SerializeField] private GameObject[] Zombies;
	[SerializeField] private List<GameObject> hpObj;

	private int length;
	float timer = 0;
	Vector3 _position;

	// ChangeHp(Zombies[i], hpObj[i]);
	// ShowHp(Zombies[i], hpObj[i]);

	private void Update()
	{
		if (length == 0)
			length = GameObject.FindGameObjectsWithTag("Respawn").Length;
		else
		{
			if (length != Zombies.Length)
			{
				Zombies = GameObject.FindGameObjectsWithTag("Enemy");

				foreach (GameObject element in Zombies)
				{
					if (element == null)
						Destroy(element);
				}
			}

			if (length == Zombies.Length && length != hpObj.Count)
			{
				GameObject zombie = Array.Find(Zombies, element => element.name == "Zombie");

				GameObject hp = Instantiate(Manager.Instance.HpPrefab);
				hp.transform.SetParent(hpCanvas.transform);
				hp.transform.localScale = new Vector3(0.00125f, 0.001f, 0.001f);

				hp.transform.position = new Vector3(
					zombie.transform.position.x,
					zombie.transform.position.y + 2.0f,
					zombie.transform.position.z);

				hp.gameObject.SetActive(false);
				hpObj.Add(hp);
			}
		}
		//else
		//{
		//	if (length != Zombies.Length)
		//		Zombies = GameObject.FindGameObjectsWithTag("Enemy");
		//	else if (length == Zombies.Length && length != hpObj.Length)
		//	{
		//
		//	}
		//	else if (length == Zombies.Length && length != hpObj.Length)
		//	{
		//		hpObj = Instantiate(Manager.Instance.HpPrefab);
		//		hpObj.transform.SetParent(hpCanvas.transform);
		//		hpObj.transform.localScale = new Vector3(0.00125f, 0.001f, 0.001f);
		//	
		//		hpObj.transform.position = new Vector3(
		//			 _Zombies.transform.position.x,
		//			 _Zombies.transform.position.y + 2.0f,
		//			 _Zombies.transform.position.z);
		//	
		//		hpObj.gameObject.SetActive(false);
		//	}
		//}
	}

	void MakeEnemyHp()
	{
		GameObject hp = Instantiate(Manager.Instance.HpPrefab);
		hp.transform.SetParent(hpCanvas.transform);
		hp.transform.localScale = new Vector3(0.00125f, 0.001f, 0.001f);


	}

	//void MakeEnemyHp(GameObject _Zombies, GameObject _hpObj)
	//{
	//	_hpObj = Instantiate(Manager.Instance.HpPrefab);
	//	_hpObj.transform.SetParent(hpCanvas.transform);
	//	_hpObj.transform.localScale = new Vector3(0.00125f, 0.001f, 0.001f);
	//
	//	_hpObj.transform.position = new Vector3(
	//		 _Zombies.transform.position.x,
	//		 _Zombies.transform.position.y + 2.0f,
	//		 _Zombies.transform.position.z);
	//
	//	_hpObj.gameObject.SetActive(false);
	//}

	void ChangeHp(GameObject _Zombie, GameObject _Hp)
	{
		Debug.Log("ChangeHp");
		if (_position != _Zombie.transform.position && _Hp != null)
		{
			_Hp.transform.position = new Vector3(
				_Zombie.transform.position.x,
				_Zombie.transform.position.y + 2.0f,
				_Zombie.transform.position.z);

			if (_Zombie.gameObject.GetComponent<Health>().curHealth > 0)
			{
				_Hp.gameObject.GetComponent<Image>().fillAmount = Mathf.Lerp(
					_Hp.gameObject.GetComponent<Image>().fillAmount,
					_Zombie.gameObject.GetComponent<Health>().curHealth / _Zombie.gameObject.GetComponent<Health>().totalHealth,
					Time.deltaTime * 10.0f);
			}
			else
				Destroy(_Hp, 1.0f);
		}
	}

	void ShowHp(GameObject _Zombie, GameObject _Hp)
	{
		timer += Time.deltaTime;
		Debug.Log("ShowHp");

		if (_Zombie.gameObject.GetComponent<Health>().curHealth != _Zombie.gameObject.GetComponent<Health>().setcurHealth)
		{
			_Hp.gameObject.SetActive(true);

			if (timer >= 1.5f)
			{
				timer = 0.0f;
				_Zombie.gameObject.GetComponent<Health>().setcurHealth = _Zombie.gameObject.GetComponent<Health>().curHealth;
			}

			if (_Zombie.gameObject.GetComponent<Health>().curHealth == 0) return;
			_Hp.gameObject.SetActive(false);
		}
	}
}
