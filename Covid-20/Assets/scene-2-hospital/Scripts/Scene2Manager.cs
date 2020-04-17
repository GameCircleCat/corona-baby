using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Manager : MonoBehaviour
{
    public static Scene2Manager Instance = null;

    public PatientObject[] patients = null;
    [SerializeField] private MedkitWidget[] m_Medkits = null;
    [SerializeField] private TMPro.TextMeshProUGUI m_globalTimeText = null;
    [SerializeField] private int m_MaxMedkitBudget = 4;

    private float m_UpdateMedsAt = 0;
    private float m_totalTime = 0;
    private PatientObject m_CurrentPatient = null;

    private bool m_Running = false;

    private void Awake() {
        Instance = this;

        SetCurrentPatient(null);

        foreach (var patient in patients) {
            bool active = Random.Range(0, 100) > 50;
            patient.gameObject.SetActive(active);
            if (active) {
                patient.health = Random.Range(40, 80);
                patient.alive = 0;
            } else {
                StartCoroutine(RegenPatient(patient, 60));
            }
        }

        int i = 0;
        foreach (var medkit in m_Medkits) {
            int index = i++;
            medkit.button.onClick.AddListener(() => UseMedkit(index));
        }

        m_Running = true;
    }

    private void FixedUpdate() {
        if (!m_Running)
            return;

        // update meds
        m_UpdateMedsAt -= Time.fixedDeltaTime;
        if (m_UpdateMedsAt <= 0) {
            m_UpdateMedsAt = 1.5f * 60;
            for (int i = 0; i < m_Medkits.Length; i++) {
                m_Medkits[i].available = m_MaxMedkitBudget;
                m_Medkits[i].text = $"{m_MaxMedkitBudget} / {m_MaxMedkitBudget}";
            }
        }

        // update patients
        foreach (var patient in patients) {
            if (!patient.gameObject.activeSelf)
                continue;

            patient.health -= 100 / 60.0f * Time.fixedDeltaTime;
            patient.alive += Time.fixedDeltaTime * 1000;

            if (patient.health <= 0) {
                OnLose();
                return;
            } else if (patient.alive >= 60000) {
                OnPatientRecovered(patient);
            }
        }

        // update global time
        m_totalTime += Time.fixedDeltaTime;
        m_globalTimeText.text = GetGlobalTimingText();

        // raise time scale
        Time.timeScale = 1 + m_totalTime / 60;
    }

    private string GetGlobalTimingText() {
        int hours = (int)(m_totalTime / 3600);
        int minutes = (int)(m_totalTime / 60) - hours * 60;
        int seconds = (int)m_totalTime - hours * 3600 - minutes * 60;

        if (hours > 0) {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
        } else {
            return string.Format("{0:D2}:{1:D2}", minutes, seconds);
        }
    }

    private void OnLose() {
        Time.timeScale = 1;
        m_Running = false;
        SetCurrentPatient(null);

        Debug.Log("Survived for " + GetGlobalTimingText());

        // todo; integrate with main scene
    }

    private void OnPatientRecovered(PatientObject patient) {
        patient.gameObject.SetActive(false);

        // regen a random patient at same place after one minute
        StartCoroutine(RegenPatient(patient, 60));
    }

    private IEnumerator RegenPatient(PatientObject patient, int seconds) {
        yield return new WaitForSeconds(seconds);
        if (m_Running) {
            patient.alive = 0;
            patient.health = Random.Range(40, 80);
            patient.gameObject.SetActive(true);
        }
    }

    public void UseMedkit(int medkitIndex) {
        if (!m_Running || m_CurrentPatient == null || m_Medkits[medkitIndex].available == 0)
            return;

        var medkit = m_Medkits[medkitIndex];
        m_CurrentPatient.health = Mathf.Min(100, m_CurrentPatient.health + medkit.health);

        medkit.available--;
        medkit.text = $"{medkit.available} / {m_MaxMedkitBudget}";
        medkit.button.interactable = (m_CurrentPatient != null) && medkit.available > 0;
    }

    public void SetCurrentPatient(PatientObject patient) {
        m_CurrentPatient = m_Running ? patient : null;
        foreach (var medkit in m_Medkits)
            medkit.button.interactable = (m_CurrentPatient != null) && medkit.available > 0;
    }
}
