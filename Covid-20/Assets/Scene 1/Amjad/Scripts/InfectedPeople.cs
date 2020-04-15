using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedPeople : MonoBehaviour
{
    public Transform player;
    private float movementSpeed ;


    private void Awake()
    {
        transform.position = new Vector3(Random.Range(-43 , 43), 0, Random.Range(20 , 100));
    }

    void Start()
    {
        movementSpeed = Random.Range(15, 18);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, player.position) < 15.0f)
        transform.position = Vector3.MoveTowards(transform.position, player.position, 0.01f * movementSpeed);
    }
}
