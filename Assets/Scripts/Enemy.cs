using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim;
    GameObject target;

    private float StopDist = 1.7f;
    public bool Dead = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Dead != true)
        {
            float dist = Vector3.Distance(transform.position, target.transform.position);

            if (dist < StopDist)
            {
                StopEnemy();
            }else
            {
                GoToTarget();
            }

        }else{
            Dead = true;
            Dying();
        }

        

        

    }

    void GoToTarget()
    {
        agent.isStopped = false;
        anim.SetBool("IsWalking", true);
        agent.SetDestination(target.transform.position);


    }
    
    void StopEnemy()
    {
        anim.SetBool("IsWalking", false);
        agent.isStopped = true; 
        
    }

    void Dying()
    {
        agent.isStopped = true; 
        anim.SetBool("IsDying", true);
    }
}
