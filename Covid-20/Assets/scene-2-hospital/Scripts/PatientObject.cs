using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatientObject : MonoBehaviour
{
    [SerializeField] private GameObject m_GoodGO  = null;
    [SerializeField] private GameObject m_MidGO = null;
    [SerializeField] private GameObject m_BadGO = null;

    [SerializeField] private UnityEngine.UI.Slider m_HealthSlider = null;
    [SerializeField] private UnityEngine.UI.Image m_HealthBarImage = null;

    [SerializeField] private float m_Health = 100;

    private float m_Alive = 0;

    public float health {
        get => m_Health;
        set {
            if (m_Health == value)
                return;

            m_Health = value;
            m_HealthSlider.value = value;

            if (m_Health >= 67) {
                m_GoodGO.SetActive(true);
                m_MidGO.SetActive(false);
                m_BadGO.SetActive(false);
                m_HealthBarImage.color = Color.green;
            } else if (m_Health >= 34) {
                m_GoodGO.SetActive(false);
                m_MidGO.SetActive(true);
                m_BadGO.SetActive(false);
                m_HealthBarImage.color = Color.blue;
            } else {
                m_GoodGO.SetActive(false);
                m_MidGO.SetActive(false);
                m_BadGO.SetActive(true);
                m_HealthBarImage.color = Color.red;
            }
        }
    }

    public float alive {
        get => m_Alive;
        set => m_Alive = value;
    }

    public void OnMouseDown() {
        Debug.Log("MouseDown");
    }
}
