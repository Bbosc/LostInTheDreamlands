using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{

    public enum HandType : int { LeftHand, RightHand };
    [Header("Hand Properties")]
    public HandType handType;
    [Header("Player Controller")]
    public MainPlayerController playerController;
    protected ObjectAnchor object_grasped = null;



    public bool is_hand_closed()
    {
        if (handType == HandType.LeftHand) return
          OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5;     // Check that the middle finger is pressing

        else return
            OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5;   // Check that the middle finger is pressing
    }
    public ObjectAnchor handle_controller_behavior(ObjectAnchor[] anchors_in_the_scene)
    {

        bool hand_closed = is_hand_closed();

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

}