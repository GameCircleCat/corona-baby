using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm = null;

    PlayerController player;

    private void Awake()
    {
        // setup reference to game manager
        if (gm == null)
            gm = this.GetComponent<GameManager>();

        DontDestroyOnLoad(this); 
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>(); 
    }

    void Update()
    {
        if(UIManager.UIMgr.slider.value == 0)
        {
            Debug.Log("You Lost");
        }
    }

    #region Khalid 
    public void Lose()
    {
        Debug.Log("You Lost");
    }

    public void UpdateSLiderValue(float health)
    {
        UIManager.UIMgr.slider.value = health;
    }
    #endregion
}
