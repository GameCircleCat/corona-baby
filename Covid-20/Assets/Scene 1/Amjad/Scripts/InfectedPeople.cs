using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class InfectedPeople : MonoBehaviour
{
    // Patrol
    [SerializeField] Transform[] m_WayPoints;
    bool m_bPatrolling;
    int m_DestIdx;
    bool m_bArrived;
    [SerializeField] float m_PatrolNextLag = 1.0f;

    // Attack and eye
    public Transform m_Target;
    Transform m_Eye;
    Vector3 m_TargetLastPos;
    [SerializeField] float m_AgroDisctance = 15.0f;



    Animator animator;
    NavMeshAgent agent;
    //Transform player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        m_Eye = transform.Find("Eye");
        m_TargetLastPos = transform.position;
    }
    bool CheckTarget()
    {
        //bool visible = false;

        //Ray ray = new Ray(m_Eye.position,
        //   m_Eye.forward);
        ////m_Target.position - m_Eye.position);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, m_AgroDisctance))
        //{
        //    if (hit.transform == m_Target)
        //    {
        //        visible = true;
        //        m_TargetLastPos = m_Target.transform.position;
        //    }
        //    else
        //    {
        //        visible = false;
        //    }
        //}
        //Debug.DrawLine(m_Eye.position, m_Eye.position + (m_Target.position - m_Eye.position).normalized * m_AgroDisctance);
        //return visible;
        if (m_Target != null && Vector3.Distance(transform.position, m_Target.position) < m_AgroDisctance)
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        //if(animator)
        //    animator.SetFloat("Speed", agent.velocity.magnitude);

        if (agent.pathPending)
            return;

        if (m_bPatrolling)
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                if (!m_bArrived)
                {
                    m_bArrived = true;
                    StartCoroutine("PatrolNext");
                }
            }
            else
            {
                m_bArrived = false;
            }
        }

        if (CheckTarget())
        {
            agent.SetDestination(m_Target.position);
            m_bPatrolling = false;
        }
        else
        {
            if (!m_bPatrolling)
            {
                agent.SetDestination(m_TargetLastPos);
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    m_bPatrolling = true;
                    StartCoroutine("PatrolNext");
                }
            }
        }
        if (m_bPatrolling)
        {
            animator.SetBool("Chasing", false);
        }
        else
        {
            animator.SetBool("Chasing", true);
        }
    }
    IEnumerator PatrolNext()
    {
        if (m_WayPoints.Length == 0)
            yield break;

        m_bPatrolling = true;
        yield return new WaitForSeconds(m_PatrolNextLag);

        m_bArrived = false;
        agent.destination = m_WayPoints[m_DestIdx].position;
        m_DestIdx = Random.Range(0, m_WayPoints.Length);

    }
    void Patrol()
    {
        //index = Random.Range(0, waypoints.Length);

        //index = index == m_NumOfPOints - 1 ? 0 : index + 1;
    }

    void Tick()
    {
        //agent.destination = waypoints[index].position;
        //agent.speed = agentSpeed / 2;

        //if (player != null && Vector3.Distance(transform.position, player.position) < aggroRange)
        //{
        //    agent.destination = player.position;
        //    agent.speed = agentSpeed;
        //}
    }

}
