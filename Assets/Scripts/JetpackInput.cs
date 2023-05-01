using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class JetpackInput : MonoBehaviour
{
    [SerializeField]
    private OVRInput.Controller m_controller = OVRInput.Controller.None;

    private float currentThrustInput;
    private Vector3 currentThrust;
    public float maxThrust = 100f;
    public Vector3 Orientation = Vector3.up;

    public Vector3 CurrentThrust { get => currentThrust; }

    public event Action<Vector3> EvtThrustChanged = delegate { };


    // Start is called before the first frame update
    void Start()
    {
        ThrustInputChanged(0f);   
    }

    // Update is called once per frame
    void Update()
    {
        float newInput = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, m_controller);
        ThrustInputChanged(newInput);
    }
    
    public void ThrustInputChanged(float newInput)
    {
        currentThrustInput = newInput;
        Orientation = transform.forward;
        currentThrust = currentThrustInput * Orientation;
        
        EvtThrustChanged(currentThrust);
    }

}
