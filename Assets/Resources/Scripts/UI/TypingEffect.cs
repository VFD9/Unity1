using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] private Text UItext;
	private string m_text;

	void Start()
    {
		UItext.color = new Color(UItext.color.r, UItext.color.g, UItext.color.b, 0.0f);
		m_text = UItext.text;
		StartCoroutine(_typing());
    }

    IEnumerator _typing()
	{
		yield return new WaitForSeconds(1.0f);
		UItext.color = new Color(UItext.color.r, UItext.color.g, UItext.color.b, 255.0f);

		for (int i = 0; i < m_text.Length; ++i)
		{
			UItext.text = m_text.Substring(0, i + 1);
			yield return new WaitForSeconds(0.15f);
		}
	}
}
