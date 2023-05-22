using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Bucket : MonoBehaviour
{

    bool teleport;
    // Start is called before the first frame update
    void Start()
    {
        teleport = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("bottomWell")) && (teleport == false))
        {
            teleport = true;
            Vector3 pos_relative = new Vector3(0f, 1f, 0f);
            collision.gameObject.transform.position = transform.position + pos_relative;
        }

    }


}
