using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabHammer(ObjectAnchor hammer)
    {

        hammer.collisionBoxes[2].isTrigger = false; // activate only the endpoint

    }
}
