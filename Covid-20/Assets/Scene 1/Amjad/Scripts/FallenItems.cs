using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenItems : MonoBehaviour
{
    Rigidbody m_RigidBody;

    public GameObject FallenItemsParent;


    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(transform.localPosition.y < 4)
        {
            transform.parent = FallenItemsParent.transform;
        }
    }
}
