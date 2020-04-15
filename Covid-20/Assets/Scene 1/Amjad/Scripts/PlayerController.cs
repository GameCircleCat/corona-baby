using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed = 1.0f;
    public float gravity = 10.0f;

    Rigidbody m_RigidBody;

    Vector3 Mov;


    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();

    }


    void FixedUpdate()
    {
        Mov.x = Input.GetAxis("Horizontal") * movementSpeed;

        Mov.z = Input.GetAxis("Vertical") * movementSpeed;


        transform.Translate(Mov * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Infected")
        {
            UIManager.UIMgr.slider.GetComponent<Slider>().value -= .0001f;
        }
    }
}
