using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LodingBar : MonoBehaviour
{
    //[SerializeField] 
    private Image HealthBar = null;

    private float Crossline;

	private void Awake()
	{
        HealthBar = GameObject.Find("HealthBar").GetComponent<Image>();
    }

    IEnumerator Start()
    {
        Crossline = 0.7f; // 팁을 보여주기 위한 용도
        HealthBar.fillAmount = 0;
        float Frame = 0.5f;

        while(true)
		{
            if (Crossline > HealthBar.fillAmount)
            {
                if (HealthBar.fillAmount >= 0.85)
                    Frame = 4.0f;
                    
                HealthBar.fillAmount += Time.deltaTime;

                if (HealthBar.fillAmount >= 1.0f)
                    break;
            }
            else
            { 
                yield return new WaitForSeconds(Frame); 
                Crossline += 0.1f;
            }

            yield return null;
		}

        Debug.Log("next Scene");

        //SceneManager.LoadScene("mainMenuScene");
    }
}
