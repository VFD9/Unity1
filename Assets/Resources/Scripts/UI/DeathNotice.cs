using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class DeathNotice : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.Dead_popup.gameObject.SetActive(false);
    }

    private void Update()
    {
        Restartpopup();
    }

    private void Restartpopup()
    {
        if (PlayerManager.Instance.playerstat.currenthp == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            UIManager.Instance.Dead_popup.gameObject.SetActive(true);

            return;
        }
    }
}
