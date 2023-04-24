using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Transform HealthBar;
    public Transform ExpBar;
    public Transform Levelup;
    public Text currenthp;
    public Text totalhp;
    public Text Levelcount;

    public Transform Pause_popup;
    public Transform Setting_popup;
    public Transform Quit_popup;
    public Transform Dead_popup;
    public Transform Operation_popup;
    public Light Directionlight;
    public Slider slider;

    public Transform PlayerImage;
}
