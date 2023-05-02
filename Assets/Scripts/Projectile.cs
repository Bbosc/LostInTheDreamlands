using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody projRB;
    static int force_multiplicator = 20;
    void Start()
    {
        projRB = this.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void launch(Transform launchPosition)
    {
        projRB.AddForce(-launchPosition.position.normalized * force_multiplicator, ForceMode.Impulse);
        projRB.useGravity = true;
    }
}
