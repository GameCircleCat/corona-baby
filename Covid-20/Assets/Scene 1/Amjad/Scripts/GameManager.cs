using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(UIManager.UIMgr.slider.value == 0)
        {
            Debug.Log("You Lost");
        }
    }
}
