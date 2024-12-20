using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class objectToMove : MonoBehaviour
{
    public bool objectInTheContainer;
    protected Transform initial_transform_parent;
    //public ParentConstraint parentConstraint;
    protected ParentConstraint parentConstraint;

    Collider m_Collider;

    // Start is called before the first frame update
    void Start()
    {
        //MeshRendered meshWater = GetComponent<MeshRendered>();
        objectInTheContainer = false;
        parentConstraint = GetComponent<ParentConstraint>();
        initial_transform_parent = transform.parent;
        m_Collider = GetComponent<Collider>();
        Debug.Log("Object created");
    }

    private void OnTriggerEnter(Collider container) {
        Debug.Log("Get colision");
        if(container.gameObject.transform.tag == "Container") {
            objectInTheContainer = true;
            //transform.SetParent( container.gameObject.transform, true );
            parentConstraint.translationAtRest = Vector3.zero;
            parentConstraint.translationOffsets = new Vector3[] {Vector3.zero};
            parentConstraint.constraintActive = true;
            m_Collider.enabled = false;
            
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log("Collision with container");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (objectInTheContainer) {
            //if (OVRInput.Get( OVRInput.RawButton.Y )) {
            if (Vector3.Angle(transform.forward, Vector3.down) < 80) {
                // Set the object to be placed in the original transform parent
		        //transform.SetParent( initial_transform_parent, true );
                parentConstraint.constraintActive = false;
                m_Collider.enabled = true;
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                objectInTheContainer = false;
            }
            //meshWater.enabled = true;
        } 
    }
}
