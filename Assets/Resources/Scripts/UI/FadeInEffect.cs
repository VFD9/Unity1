using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInEffect : MonoBehaviour
{
    [SerializeField] private Image boxImage;

    void Start()
    {
        boxImage.color = new Color(boxImage.color.r, boxImage.color.g, boxImage.color.b, 0.0f);
        StartCoroutine(fadeEffect());
    }

    IEnumerator fadeEffect()
	{
        yield return null;

        while (true)
        {
            yield return null;
            boxImage.color = new Color(boxImage.color.r, boxImage.color.g, boxImage.color.b,
                boxImage.color.a + 0.7f * Time.deltaTime);
        }
	}
}
