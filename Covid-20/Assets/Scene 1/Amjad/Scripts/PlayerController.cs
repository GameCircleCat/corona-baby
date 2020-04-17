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

    #region Khalid
    [Range(0, 1)]
    [SerializeField]
    float speedUpRate = 0.5f;
    [SerializeField]
    float speedUpTime = 10.0f;
    [SerializeField]
    float invulnerabilityTime = 10.0f;
    [SerializeField]
    float infectionRateIncrease = 0.5f;
//    [SerializeField]
//    float infectionRateDecrease = 0.0f;

//    float infection; 
    float health = 100.0f;
 //   float resistance = 100.0f;
    float timePassed = 0.0f;

    bool isInvulnerable = false;
    #endregion

    void Start()
    {
//        infection = infectionRateIncrease - infectionRateDecrease; 
        m_RigidBody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        Mov.x = Input.GetAxis("Horizontal") * movementSpeed;

        Mov.z = Input.GetAxis("Vertical") * movementSpeed;

        transform.Translate(Mov * Time.deltaTime , Space.World);

        transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.LookRotation(Mov) , .15f);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Infected")
        {
            health -= infectionRateIncrease;
            if (health <= 0)
                GameManager.gm.Lose();  
            GameManager.gm.UpdateSLiderValue(health); 
        }
    }


    #region Khalid
    public void MakeInvulnerable()
    {
        timePassed = 0.0f;
        StartCoroutine("Invulnerable");
    }

    public void SpeedUp()
    {
        timePassed = 0.0f;
        StartCoroutine("SpeedingUp");
    }

    public void AddResistance()
    {
        // infectionRateDecrease += infectionRateIncrease / 2.0f; 
        health += 20.0f;
        if (health > 100)
            health = 100.0f;
    }
    #endregion

    IEnumerator SpeedingUp()
    {
        float speed = movementSpeed; 
        movementSpeed *= (speedUpRate + 1.0f) ;

        //if (backgroundMusic)
        //    backgroundMusic.pitch = 1.25f;

        while (timePassed < speedUpTime)
        {

            timePassed += Time.deltaTime;

            yield return null;
        }

       movementSpeed = speed;

        //if (backgroundMusic)
        //    backgroundMusic.pitch = 1f;
    }

    IEnumerator Invulnerable()
    {
        if (!isInvulnerable)
            isInvulnerable = true;

        // Create something like a flash animation to give the effect of invulnerability 
        // Could give that effect by make something like a sphere shield around the player 
        // GetComponent<Animator>().SetBool("Invulnerable", isInvulnerable);


        while (timePassed < invulnerabilityTime)
        {
            timePassed += Time.deltaTime;

            yield return null;
        }

        isInvulnerable = false;

        //GetComponent<Animator>().SetBool("Invulnerable", isInvulnerable);

    }
}
