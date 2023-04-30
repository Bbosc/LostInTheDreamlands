using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    protected int tip_index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.GetContact(0).normal);
        //this.GetComponentsInChildren<Collider>()[tip_index].enabled = true;
        col.rigidbody.AddForce( - col.GetContact(0).normal * 20, ForceMode.Impulse);
        col.rigidbody.useGravity = true;

    }

    public void GrabSword(ObjectAnchor sword)
    {
        for (int i = 0; i < sword.collisionBoxes.Length; i++) { sword.collisionBoxes[i].enabled = true; }
        //hammer.collisionBoxes[1].isTrigger = false;
        //Debug.Log(hammer.collisionBoxes.Length);
        //hammer.collisionBoxes[tip_index].enabled = true; // activate only the endpoint
        //hammer.collisionBoxes[1].enabled = true; // activate only the endpoint
        //hammer.collisionBoxes[0].enabled = true;
    }
}
