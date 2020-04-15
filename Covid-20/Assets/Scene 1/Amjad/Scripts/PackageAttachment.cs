using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageAttachment : MonoBehaviour
{
    public Transform player;
    public Transform Holder;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.parent = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            this.transform.parent = player;
            this.transform.position = Holder.position;
            Debug.Log("Collided");
        }

        else if(collision.transform.tag == "Infected")
        {
            this.transform.parent = null;
        }
    }
}
