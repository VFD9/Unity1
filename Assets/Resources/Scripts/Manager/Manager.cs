using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Global scripts holder
public class Manager : Singleton<Manager>
{
	public SoundManager soundManager;
	public GameObject Zombie;
	public GameObject HpPrefab;
	public TypingEffect typingeffect;
	public GameObject eventSystem;
}
