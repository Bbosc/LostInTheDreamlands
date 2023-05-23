using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Bow properties")]
    public GameObject arrow;
    public float launchForce = 18.0f;
    public Transform shotPoint;
    bool arrow_knocked = false;
    public bool tutorial_completed = false;

    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            Shoot();
        }
    }

    public void GrabBow(ObjectAnchor bow)
    {
        Vector3 handLeftPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 handRightPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        if (Mathf.Abs(Vector3.Distance(handLeftPosition, handRightPosition)) < 0.2f)
        {
            arrow_knocked = true;
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }
        if ((Mathf.Abs(Vector3.Distance(handLeftPosition, handRightPosition)) > 0.3f) && arrow_knocked)
        {
            Shoot();
            arrow_knocked = false;
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }

    void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        newArrow.AddComponent<Rigidbody>();
        newArrow.GetComponent<Rigidbody>().isKinematic = false;
        newArrow.GetComponent<Rigidbody>().useGravity = true;
        newArrow.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        newArrow.GetComponent<Rigidbody>().transform.Rotate(-90, 0, 0, Space.Self);
        newArrow.GetComponent<Rigidbody>().velocity = (transform.right) * launchForce;
        newArrow.GetComponent<MeshCollider>().convex = true;
        newArrow.AddComponent<BoxCollider>();
        newArrow.AddComponent<Arrow>();
    }

    public bool is_tutorial_completed() { return tutorial_completed; }
}
