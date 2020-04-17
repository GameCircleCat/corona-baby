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
    public GameObject FirstMissionDoneUI;
    public GameObject SecondMissionDoneUI;
    public GameObject UI_PopUp;

    Color orange = new Color(1f, 0.64f, 0f);

    [Header("GameObjects")]
    public Transform Player;
    public Transform Hospital;
    public Transform package;
    public GameObject UIGamePaused;

    void Start()
    {
        if (!UIMgr)
        {
            UIMgr = this;
        }

        Invoke("PopUp", 2);
    }

    void Update()
    {
        var sliderFill = slider.value;

        // controlling Health bar color 
        if (sliderFill > .75)
        {
            fill.color = Color.green;
        }

        else if (sliderFill > .5f && sliderFill < .75)
        {
            fill.color = Color.Lerp(Color.green, Color.yellow, 1);
        }

        else if (sliderFill > .25f && sliderFill < .5f)
        {
            fill.color = Color.Lerp(Color.yellow, orange, 1);
        }

        else if (sliderFill < .25)
        {
            fill.color = Color.Lerp(orange, Color.red, 1);
        }

        // Display the distance and completed missions
        if (!PackageAttachment.PgAttachment.GotAttached)
        {
            Distance.text = "Distance: " + Vector3.Distance(package.position, Player.position).ToString() + "m";
            //Distance.text = "Distance: " + Mathf.Ceil((package.position.z - Player.position.z)).ToString() + "m";
        }

        else
        {
            Distance.text = "Distance: " + Vector3.Distance(Hospital.position, Player.position).ToString() + "m";
            //Distance.text = "Distance: " + Mathf.Ceil((Hospital.position.z - Player.position.z)).ToString() + "m";
        }

        if (Mathf.Ceil((Hospital.position.z - Player.position.z)) == 0)
        {
            UIManager.UIMgr.SecondMissionDoneUI.SetActive(true);
            // go to the next scene
        }


    }

    private void PopUp()
    {
        UI_PopUp.SetActive(true);
    }

}
