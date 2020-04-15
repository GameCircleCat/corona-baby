using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager UIMgr;

    public Slider slider;
    public Image fill;
    public Text Distance;

    [Header("GameObjects")]
    public Transform Player;
    public Transform Hospital;


    Color orange = new Color(1f, 0.64f, 0f);

    void Start()
    {
        if(!UIMgr)
        {
            UIMgr = this;
        }
    }

    void Update()
    {
        var sliderFill = slider.value;

        if(sliderFill > .75)
        {
            fill.color = Color.green;
        }

        else if(sliderFill > .5f && sliderFill < .75)
        {
            fill.color = Color.Lerp(Color.green, Color.yellow, 1);
        }

        else if(sliderFill > .25f && sliderFill < .5f)
        {
            fill.color = Color.Lerp(Color.yellow, orange , 1);
        }

        else if(sliderFill < .25)
        {
            fill.color = Color.Lerp(orange, Color.red, 1);
        }

        Distance.text = "Distance: " + Mathf.Ceil((Hospital.position.z - Player.position.z) * 10).ToString() + "m";
    }
}
