using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Options : MonoBehaviour
{
    public AudioClip ClickSound;
    public AudioClip closeSound;
    public AudioClip OnOffSound;
    public AudioClip CancelSound;
    public AudioClip clickIntensitySound;
    public AudioClip sliderSound;
    //public AudioClip YesSound;

    private Animator anim;
    private Animator OperAnim;

	private void Awake()
	{
        anim = GetComponent<Animator>();
        OperAnim = UIManager.Instance.Operation_popup.gameObject.GetComponent<Animator>();
    }

	private void Start()
	{
        anim.enabled = false;
        OperAnim.enabled = false;
        UIManager.Instance.Quit_popup.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (PlayerManager.Instance.gun != null)
        {
            PlayerManager.Instance.gun.transform.GetChild(0).GetComponent<AudioSource>().volume = UIManager.Instance.slider.value;
            PlayerManager.Instance.gun.transform.GetChild(1).GetComponent<AudioSource>().volume = UIManager.Instance.slider.value;
        }
    }

    public void showSetting()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            anim.enabled = true;
            anim.SetBool("Check", true);
        }
    }

    public void closeSetting()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            anim.SetBool("Check", false);
    }

    public void showOperation()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            OperAnim.enabled = true;
            OperAnim.SetBool("Close", true);
		}
    }

    public void closeOperation()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            OperAnim.SetBool("Close", false);
    }

    public void clicksound()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            Manager.Instance.soundManager.PlaySound(ClickSound);
    }

    public void clikconoff()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            Manager.Instance.soundManager.PlaySound(OnOffSound);
    }

    public void clickClose()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            Manager.Instance.soundManager.PlaySound(closeSound);
    }

    public void clickCancel()
    {
        if (EventSystem.current.IsPointerOverGameObject())
           Manager.Instance.soundManager.PlaySound(CancelSound);
    }

    public void OnDrop() // SoundSlider의 하위 객체 Handle을 클릭한 후 소리 조절
    {
        Manager.Instance.soundManager.PlaySound(sliderSound);
    }

    public void OnPointerUp() // SoundSlider의 하위 객체 Handle을 클릭하지 않고 임의의 위치에 클릭해서 소리 조절
    {
        Manager.Instance.soundManager.PlaySound(sliderSound);
    }

    public void clickHandle() // SoundSlider의 하위 객체 Handle의 위치를 바꾸지 않고 클릭만 함(소리 조절 안함)
    {
        Manager.Instance.soundManager.PlaySound(sliderSound);
    }

    public void intensity0()
	{
       UIManager.Instance.Directionlight.intensity = 0;
       Manager.Instance.soundManager.PlaySound(clickIntensitySound);
	}

    public void intensity1()
    {
        UIManager.Instance.Directionlight.intensity = 1;
        Manager.Instance.soundManager.PlaySound(clickIntensitySound);
    }

    public void intensity2()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            UIManager.Instance.Directionlight.intensity = 2;
            Manager.Instance.soundManager.PlaySound(clickIntensitySound);
        }
    }

    public void intensity3()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            UIManager.Instance.Directionlight.intensity = 3;
            Manager.Instance.soundManager.PlaySound(clickIntensitySound);
        }
    }

    public void intensity4()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            UIManager.Instance.Directionlight.intensity = 4;
            Manager.Instance.soundManager.PlaySound(clickIntensitySound);
        }
    }
}
