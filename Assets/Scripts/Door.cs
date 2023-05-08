using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    GameObject player;
    private float StopDist = 1.7f;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist < StopDist)
        {
            anim.SetBool("Opening", true);
        }


    }
    
}
