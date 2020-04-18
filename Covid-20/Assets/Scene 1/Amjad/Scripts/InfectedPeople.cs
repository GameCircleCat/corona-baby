using UnityEngine;
using UnityEngine.AI; 

public class InfectedPeople : MonoBehaviour
{

    public float patrolTime = 15;
    public float aggroRange = 10;
    public Transform[] waypoints; 

    int index;
    float speed, agentSpeed; 

    Animator animator; 
    NavMeshAgent agent; 
    Transform player;

    private float movementSpeed ;

    private void Awake()
    {
        //transform.position = new Vector3(Random.Range(-19 , 19), 0, Random.Range(-2 , 70)); // to randomise the position of the object in the start of the game.
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) { agentSpeed = agent.speed; }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);
        movementSpeed = Random.Range(15, 18);

        InvokeRepeating("Tick", 0, 0.5f);

        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", Random.Range(0, patrolTime), patrolTime);
        }
    }

    void Update()
    {
        //if(animator)
        //    animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }

    void Tick()
    {
        agent.destination = waypoints[index].position;
        agent.speed = agentSpeed / 2;

        if (player != null && Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            agent.destination = player.position;
            agent.speed = agentSpeed;
        }
    }

}
