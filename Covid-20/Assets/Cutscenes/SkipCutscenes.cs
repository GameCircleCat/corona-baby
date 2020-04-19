using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipCutscenes : MonoBehaviour
{
    VideoPlayer m_Videoplayer;

    private void Start()
    {
        m_Videoplayer = GetComponent<VideoPlayer>();
    }


    private void Update()
    {
        Invoke("IsItPlaying", 3);
    }
    public void SkipScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void IsItPlaying()
    {
        if (!m_Videoplayer.isPlaying)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
