using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject SpawnPoint;
    [SerializeField] private Transform WinCanvas;
    [SerializeField] private Toggle MissionCheckbox;
    public AudioClip WinSound;

    private GameObject gun;

	void Start()
    {
        WinCanvas.gameObject.SetActive(false);
    }

	private void Update()
	{
        if (gun == null)
            gun = GameObject.FindGameObjectWithTag("Weapon");
	}

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.transform.CompareTag("Player"))
        {
            WinCanvas.gameObject.SetActive(true);
            SpawnPoint.SetActive(false);
            PlayerManager.Instance.playerstat.enabled = false;
            PlayerManager.Instance.mls.enabled = false;
            PlayerManager.Instance.Gunitem.enabled = false;
            PlayerManager.Instance.pms.enabled = false;

            PlayerManager.Instance.pms._walkSound.volume = 0;
            PlayerManager.Instance.pms._runSound.volume = 0;

            MissionCheckbox.isOn = true;

            Manager.Instance.soundManager.PlaySound(WinSound);

            StartCoroutine("win");
        }
	}

    IEnumerator win()
    {
        yield return new WaitForSeconds(3.0f);
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
