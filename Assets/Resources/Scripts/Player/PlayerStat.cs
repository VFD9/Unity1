using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
	[HideInInspector] public int Level;
	[HideInInspector] public float exp;
	[HideInInspector] public float currentexp;
	[HideInInspector] public float totalexp;
	[HideInInspector] public float currenthp;
	[HideInInspector] public float maxhp;
	[HideInInspector] public float maxHPUP;
	[HideInInspector] public bool up;

	[SerializeField] private GameObject LevelUpEffect;
	public AudioClip _levelupSound;

    private void Awake()
    {
		Level = 1;
		exp = 0;
		currentexp = 0;
		totalexp = 100;
		currenthp = 100;
		maxhp = 100;		
	}

    private void Start()
    {
		LevelUpEffect.SetActive(false);
		StartCoroutine(LevelUP());
	}

    IEnumerator LevelUP()
	{
		while (true)
		{
			yield return null;

			maxHPUP = Random.Range(10, 13);

			if (exp >= totalexp)
			{
				exp -= totalexp;

				totalexp += Random.Range(15, 20);
				Level += 1;

				maxhp += maxHPUP;
				currenthp = maxhp;

				up = true;
				LevelUpEffect.SetActive(true);
				Manager.Instance.soundManager.PlaySound(_levelupSound);
				yield return new WaitForSeconds(2.5f);
				LevelUpEffect.SetActive(false);
				up = false;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Heal")
		{
			if (currenthp == maxhp)
			{
				maxhp += Random.Range(1, 5);
				currenthp = maxhp;
			}
			else if (maxhp > currenthp + 15)
				currenthp += 15;
			else if (currenthp + 15 > maxhp)
				currenthp = maxhp;

			Destroy(other.gameObject);
		}
	}
}
