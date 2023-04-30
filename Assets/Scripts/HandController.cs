using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{

    public enum HandType : int { LeftHand, RightHand };
    [Header("Hand Properties")]
    public HandType handType;
    [Header("Player Controller")]
    public MainPlayerController playerController;
    static protected ObjectAnchor[] anchors_in_the_scene;
    public float shooting_force = 0.0f;
    protected bool is_hand_closed_previous_frame = false;
    protected ObjectAnchor object_grasped = null;
    LineRenderer line_r;

    [Header("Bow properties")]
    public GameObject arrow;
    public float launchForce = 10.0f;
    public Transform shotPoint;
    protected bool arrow_knocked = false;

    void Start()
    {
        if (anchors_in_the_scene == null) anchors_in_the_scene = GameObject.FindObjectsOfType<ObjectAnchor>();

    }


    bool is_hand_closed()
    {
        if (handType == HandType.LeftHand) return
          OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5;     // Check that the middle finger is pressing

        else return
            OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5;   // Check that the middle finger is pressing
    }

    void Update()
    {
        handle_controller_behavior();
        // if (Input.GetKey("space")) Shoot();
    }

    protected void handle_controller_behavior()
    {

        bool hand_closed = is_hand_closed();
        // if ( hand_closed == is_hand_closed_previous_frame ) return;
        // is_hand_closed_previous_frame = hand_closed;

        bool left_hand_closed = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5;
        bool right_hand_closed = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5;



        if (hand_closed && (object_grasped == null))
        {

            HandleGrabbing(anchors_in_the_scene);

        }
        else if ((object_grasped != null) && !hand_closed)
        {
            // Debug.LogWarningFormat("{0} released {1}", this.transform.parent.name, object_grasped.name );
            object_grasped.detach_from(this);
            object_grasped = null;
        }
        else if ((object_grasped != null) && (object_grasped.name == "Bow_01"))
        {
            GrabBow(object_grasped);
        }
        else if ((object_grasped != null) && (object_grasped.name == "Hammer_01"))
        {
            GrabHammer(object_grasped);
        }
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

            // if (object_grasped.name == "Bow_01") GrabBow(object_grasped);
        }

    }

    public void GrabBow(ObjectAnchor bow)
    {
        Vector3 handLeftPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 handRightPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        // Debug.LogWarningFormat("distance between the two controllers : {0}", Mathf.Abs(Vector3.Distance(handLeftPosition, handRightPosition)));
        if (Mathf.Abs(Vector3.Distance(handLeftPosition, handRightPosition)) < 0.2f)
        {
            arrow_knocked = true;
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }
        if (Mathf.Abs(Vector3.Distance(handLeftPosition, handRightPosition)) > 0.6f && arrow_knocked)
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
        // Debug.LogWarningFormat("bow orientation : {0} ; arrow orientation : {1}", transform.localEulerAngles, newArrow.transform.localEulerAngles);
    }

    void GrabHammer(ObjectAnchor hammer)
    {

        hammer.collisionBoxes[2].isTrigger = false; // activate only the endpoint

    }



}