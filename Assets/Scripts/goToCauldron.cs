using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class goToCauldron : MonoBehaviour
{
    protected Transform initial_transform_parent;
    //public ParentConstraint parentConstraint;
    protected ParentConstraint parentConstraint;
    public string tag_;

    Collider m_Collider;

    // Start is called before the first frame update
    void Start()
    {
        parentConstraint = GetComponent<ParentConstraint>();
        initial_transform_parent = transform.parent;
        m_Collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider container) {
        if(container.gameObject.transform.tag == tag_) {
            parentConstraint.translationAtRest = Vector3.zero;
            parentConstraint.translationOffsets = new Vector3[] {new Vector3(0.0f,0.5f,0.0f)};
            parentConstraint.constraintActive = true;
            m_Collider.enabled = false;
            
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
