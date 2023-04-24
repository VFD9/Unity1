using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject Player;
    public PlayerStat playerstat;
    public PlayerMovementScript pms;
    public MouseLookingScript mls;
    public GunInventory Gunitem;
    public DamageText damageText;
    public GameObject damagePrefab;
    public GameObject gun;
    public GunScript gunscript;
    public List<GameObject> Buff;
    public float BuffTime = 2.0f;
    public bool onBuff;

	private void Start()
	{
        onBuff = false;
        StartCoroutine(PlayGame());
	}

    private IEnumerator PlayGame()
    {
        while (true)
        {
            yield return null;

            if (gun != null)
            {
                gun = GameObject.FindGameObjectWithTag("Weapon");
                gunscript = gun.GetComponent<GunScript>();
            }
            
            for (int i = Buff.Count; i >= 0; i--)
            {
                BuffTime -= Time.deltaTime;

                if (BuffTime <= 0.0f)
                {
                    BuffTime = 0;
                    Buff.Clear();
                    yield return new WaitForSeconds(0.1f);
                    onBuff = false;
                }
            }
        }
    }

    public void ResetBuffTime()
	{
        BuffTime = 3.0f;
	}

    public void AddBuff(GameObject _Obj)
	{
        Buff.Add(_Obj);
        onBuff = true;
        BuffTime += 3.0f;
	}
}
