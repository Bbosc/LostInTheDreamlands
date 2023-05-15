using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : MonoBehaviour
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
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if (Dead == true)
        {
            Dying();
        }else{
            if (dist < StopDist)
            {
                StopEnemy();
            }else
            {
                GoToTarget();
            }
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
        anim.SetBool("IsDying", false);
        agent.isStopped = true; 
    }

    // private void OnCollisionEnter(Collision col){

    //     Debug.Log("ENEMy COLLISION");
    //     Debug.Log(col.gameObject.tag);
    //     if (col.gameObject.tag == "projectile"){
    //         Dead = true;
    //     }
    // }

}
