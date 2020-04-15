using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public bool taken = false;
    public GameObject particleEffect;

    public enum collectedItems {  Mask, Syringe, Antibiotic };

    public collectedItems item;

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") && (!taken))
        {
            // mark as taken so doesn't get taken multiple times
            taken = true;

            // if explosion prefab is provide, then instantiate it
            if (particleEffect)
            {
                Instantiate(particleEffect, transform.position, transform.rotation);
            }

            PlayerController player = other.gameObject.GetComponent<PlayerController>(); 

            switch (item)
            {
                case collectedItems.Mask:
                    player.MakeInvulnerable();
                    break;

                case collectedItems.Syringe:
                    player.SpeedUp();
                    break;

                case collectedItems.Antibiotic:
                    player.AddResistance();
                    break;
            }

            // destroy the pickup
            Destroy(gameObject);
        }
    }
}
