using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{

    public enum HandType : int { LeftHand, RightHand };
    [Header("Hand Properties")]
    public HandType handType;
    [Header("Player Controller")]
    public MainPlayerController playerController;
    Bow bowObj;
    //static protected ObjectAnchor[] anchors_in_the_scene;
    //protected bool is_hand_closed_previous_frame = false;
    protected ObjectAnchor object_grasped = null;
    //LineRenderer line_r;

    //[Header("Bow properties")]
    //public GameObject arrow;
    //public float launchForce = 10.0f;
    //public Transform shotPoint;
    //protected bool arrow_knocked = false;

    void Start()
    {
        bowObj = GameObject.Find("Bow").GetComponent<Bow>();
        //if (anchors_in_the_scene == null) anchors_in_the_scene = GameObject.FindObjectsOfType<ObjectAnchor>();

    }


    public bool is_hand_closed()
    {
        if (handType == HandType.LeftHand) return
          OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5;     // Check that the middle finger is pressing

        else return
            OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5;   // Check that the middle finger is pressing
    }

    void Update()
    {

    }

    public ObjectAnchor handle_controller_behavior(ObjectAnchor[] anchors_in_the_scene)
    {

        bool hand_closed = is_hand_closed();
        // if ( hand_closed == is_hand_closed_previous_frame ) return;
        // is_hand_closed_previous_frame = hand_closed;

        bool left_hand_closed = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5;
        bool right_hand_closed = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5;



        //if (hand_closed && (object_grasped == null))
        //{

        //    HandleGrabbing(anchors_in_the_scene);

        //}
        //else if ((object_grasped != null) && !hand_closed)
        //{
        //    object_grasped.detach_from(this);
        //    object_grasped = null;
        //}
        //else if ((object_grasped != null) && (object_grasped.name == "Bow"))
        //{
        //    GrabBow(object_grasped);
        //}
        //else if ((object_grasped != null) && (object_grasped.name == "Hammer"))
        //{
        //    GrabHammer(object_grasped);
        //}
        if (hand_closed && (object_grasped == null)) HandleGrabbing(anchors_in_the_scene);
        if (!hand_closed && (object_grasped != null))
        {
            object_grasped.detach_from(this);
            object_grasped = null;
        }

        return object_grasped ;
    }

    public void HandleGrabbing(ObjectAnchor[] anchors_in_the_scene)
    {

        int best_object_id = -1;
        float best_object_distance = float.MaxValue;
        float oject_distance;

        // Iterate over objects to determine if we can interact with it
        for (int i = 0; i < anchors_in_the_scene.Length; i++)
        {

            // Skip object not available
            if (!anchors_in_the_scene[i].is_available()) continue;

            // Compute the distance to the object
            oject_distance = Vector3.Distance(this.transform.position, anchors_in_the_scene[i].transform.position);

            if (oject_distance < best_object_distance && oject_distance <= anchors_in_the_scene[i].get_grasping_radius())
            {
                best_object_id = i;
                best_object_distance = oject_distance;
            }
        }

        if (best_object_id != -1)
        {

            object_grasped = anchors_in_the_scene[best_object_id];
            object_grasped.attach_to(this);

        }

    }

    public void GrabBow(ObjectAnchor bow)
    {
        Vector3 handLeftPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 handRightPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        if (Mathf.Abs(Vector3.Distance(handLeftPosition, handRightPosition)) < 0.2f)
        {
            bowObj.arrow_knocked = true;
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }
        if (Mathf.Abs(Vector3.Distance(handLeftPosition, handRightPosition)) > 0.4f && bowObj.arrow_knocked)
        {
            Shoot(bow);
            bowObj.arrow_knocked = false;
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }

    void Shoot(ObjectAnchor bow)
    {
        Debug.Log("trying to shoot");
        GameObject newArrow = Instantiate(bowObj.arrow, bowObj.shotPoint.position, bowObj.shotPoint.rotation);
        newArrow.AddComponent<Rigidbody>();
        newArrow.GetComponent<Rigidbody>().isKinematic = false;
        newArrow.GetComponent<Rigidbody>().velocity = (transform.forward) * bowObj.launchForce;
     
    }

    void GrabHammer(ObjectAnchor hammer)
    {

        hammer.collisionBoxes[2].isTrigger = false; // activate only the endpoint

    }



}