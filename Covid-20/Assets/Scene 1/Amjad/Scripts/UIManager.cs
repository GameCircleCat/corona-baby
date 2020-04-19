using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager UIMgr;

    public Slider slider;
    public Image fill;
    public Text Distance;
    public GameObject FirstMissionDoneUI;
    public GameObject SecondMissionDoneUI;
    public GameObject UI_PopUp;

    Color orange = new Color(1f, 0.66f, 0.11f);
    Color green = new Color(0f, .63f, .31f);
    Color yellow = new Color(1, .82f, 0);

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
        if (sliderFill > 75)
        {
            fill.color = green;
        }

        else if (sliderFill > 50 && sliderFill < 75)
        {
            fill.color = Color.Lerp(green, yellow, 1);
        }

        else if (sliderFill > 25 && sliderFill < 50)
        {
            fill.color = Color.Lerp(yellow, orange, 1);
        }

        else if (sliderFill < 25)
        {
            fill.color = Color.Lerp(orange, Color.red, 1);
        }

        // Display the distance and completed missions
        if (!PackageAttachment.PgAttachment.GotAttached)
        {
            Distance.text = "Distance: " + Mathf.Ceil( Vector3.Distance(package.position, Player.position)).ToString() + "m";
            //Distance.text = "Distance: " + Mathf.Ceil((package.position.z - Player.position.z)).ToString() + "m";
        }

        else
        {
            Distance.text = "Distance: " + Mathf.Ceil( Vector3.Distance(Hospital.position, Player.position)).ToString() + "m";
            //Distance.text = "Distance: " + Mathf.Ceil((Hospital.position.z - Player.position.z)).ToString() + "m";

            if (Mathf.Ceil(Vector3.Distance(Hospital.position, Player.position)) < 5 && PackageAttachment.PgAttachment.stillAttached == true)
            {
                UIManager.UIMgr.SecondMissionDoneUI.SetActive(true);
                // go to the next scene
                Invoke("LoadScene2", 1);
            }
        }


    }

    private void PopUp()
    {
        UI_PopUp.SetActive(true);
    }

    void LoadScene2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
