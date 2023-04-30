using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Bow properties")]
    public GameObject arrow;
    public float launchForce = 10.0f;
    public Transform shotPoint;
    public bool arrow_knocked = false;


    public void GrabBow(ObjectAnchor bow)
    {
        Vector3 handLeftPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 handRightPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        if (Mathf.Abs(Vector3.Distance(handLeftPosition, handRightPosition)) < 0.2f)
        {
            arrow_knocked = true;
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }
        if (Mathf.Abs(Vector3.Distance(handLeftPosition, handRightPosition)) > 0.4f && arrow_knocked)
        {
            Shoot(bow);
            arrow_knocked = false;
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }

    void Shoot(ObjectAnchor bow)
    {
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        newArrow.AddComponent<Rigidbody>();
        newArrow.GetComponent<Rigidbody>().isKinematic = false;
        newArrow.GetComponent<Rigidbody>().velocity = (transform.forward) * launchForce;

    }
}
