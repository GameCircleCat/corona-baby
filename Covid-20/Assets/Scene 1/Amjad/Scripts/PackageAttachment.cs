using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageAttachment : MonoBehaviour
{
    public static PackageAttachment PgAttachment;

    Rigidbody m_RigidBody;

    public Transform player;
    public Transform Holder;

    [SerializeField]
    public bool GotAttached = false;
    public bool stillAttached = false;


    private void Start()
    {
        if (!PgAttachment)
        {
            PgAttachment = this;
        }

        m_RigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.parent = null;
            //this.GetComponent<Rigidbody>().isKinematic = false;
            stillAttached = false;
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            this.transform.parent = player;
            this.transform.position = Holder.position;
            UIManager.UIMgr.FirstMissionDoneUI.SetActive(true);
            GotAttached = true;
            stillAttached = true;
            m_RigidBody.constraints = RigidbodyConstraints.FreezeAll;
            this.transform.rotation = Quaternion.Euler(0 , 180 , 0);
        }

        else if (collision.transform.tag == "Infected")
        {
            this.transform.parent = null;
            m_RigidBody.constraints = RigidbodyConstraints.None;
            stillAttached = false;
        }
    }
}
