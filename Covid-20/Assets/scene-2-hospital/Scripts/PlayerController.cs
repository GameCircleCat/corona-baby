using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController m_CharCtrl = null;

    private Vector2 movement = Vector2.zero;

    private void Update() {
        movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate() {
        // proceed movement
        if (Mathf.Approximately(movement.sqrMagnitude, 0))
            return;

        m_CharCtrl.Move(new Vector3(movement.x, 0, movement.y) * Time.fixedDeltaTime * 10);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "patient") {
            Scene2Manager.Instance.SetCurrentPatient(other.GetComponent<PatientObject>());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "patient") {
            Scene2Manager.Instance.SetCurrentPatient(null);
        }
    }
}
