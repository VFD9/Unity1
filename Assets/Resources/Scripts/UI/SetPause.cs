using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SetPause : MonoBehaviour
{
	public AudioClip OnPauseSound;
	public AudioClip OffPauseSound;

	public bool Pause;

	private void Start()
	{
		Pause = false;
		transform.GetChild(0).gameObject.SetActive(false); // Pause_Popup
	}

    private void Update()
    {
		setPause();
		DeadPause();
	}

	private void setPause()
    {
		if (Input.GetKeyDown(KeyCode.P) && Pause == false)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;

			UIManager.Instance.Pause_popup.gameObject.SetActive(true);

			Pause = true;
			PlayerManager.Instance.Gunitem.enabled = false;
			PlayerManager.Instance.pms.enabled = false;
			PlayerManager.Instance.mls.enabled = false;
			PlayerManager.Instance.gun.GetComponent<GunScript>().enabled = false;

			Manager.Instance.soundManager.PlaySound(OnPauseSound);
			Time.timeScale = 0;
			return;
		}
		else if (Input.GetKeyDown(KeyCode.P) && Pause == true)
        {
			if (UIManager.Instance.Setting_popup.gameObject.activeInHierarchy == true 
				|| UIManager.Instance.Quit_popup.gameObject.activeInHierarchy == true)
				return;

			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;

			UIManager.Instance.Pause_popup.gameObject.SetActive(false);

			Pause = false;
			PlayerManager.Instance.Gunitem.enabled = true;
			PlayerManager.Instance.pms.enabled = true;
			PlayerManager.Instance.mls.enabled = true;
			PlayerManager.Instance.gun.GetComponent<GunScript>().enabled = true;

			Manager.Instance.soundManager.PlaySound(OffPauseSound);
			Time.timeScale = 1;
			return;
		}
	}

	public void DeadPause()
	{
		if (PlayerManager.Instance.playerstat.currenthp == 0 && Pause == false)
		{
			Pause = true;
			PlayerManager.Instance.Gunitem.enabled = false;
			PlayerManager.Instance.playerstat.enabled = false;
			PlayerManager.Instance.pms.enabled = false;
			PlayerManager.Instance.mls.enabled = false;

			return;
		}
	}

	public void offPause()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			if (Pause == true)
			{
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;

				UIManager.Instance.Pause_popup.gameObject.SetActive(false);

				Pause = false;
				PlayerManager.Instance.Gunitem.enabled = true;
				PlayerManager.Instance.pms.enabled = true;
				PlayerManager.Instance.mls.enabled = true;
				PlayerManager.Instance.gun.GetComponent<GunScript>().enabled = true;

				Manager.Instance.soundManager.PlaySound(OffPauseSound);
				Time.timeScale = 1;
				return;
			}
		}
	}

	public void clickquit()
	{
#if UNITY_EDITOR
		if (EventSystem.current.IsPointerOverGameObject())
			UnityEditor.EditorApplication.isPlaying = false;
#else
		if (EventSystem.current.IsPointerOverGameObject())
			Application.Quit();
#endif
	}
	
    public void RestartScene()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			
			Pause = false;
			PlayerManager.Instance.Gunitem.enabled = true;
			PlayerManager.Instance.playerstat.enabled = true;
			PlayerManager.Instance.mls.enabled = true;
			PlayerManager.Instance.pms.enabled = true;
			PlayerManager.Instance.gun.GetComponent<GunScript>().enabled = true;

			UIManager.Instance.Dead_popup.gameObject.SetActive(false);

			SceneManager.LoadScene("GameScene");
		}
	}

	public void noRestart()
    {
		if(EventSystem.current.IsPointerOverGameObject())
			UnityEditor.EditorApplication.isPlaying = false;
	}
}
