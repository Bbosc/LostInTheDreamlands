using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colonel : MonoBehaviour
{

    static protected ObjectAnchor[] anchors_in_the_scene;
    HandController leftHand;
    HandController rightHand;
    protected ObjectAnchor left_grasped = null;
    protected ObjectAnchor right_grasped = null;
    Bow bow;
    Hammer hammer;

    // Start is called before the first frame update
    void Start()
    {
        bow = GameObject.Find("Bow").GetComponent<Bow>();
        hammer = GameObject.Find("Hammer").GetComponent<Hammer>();
        leftHand = GameObject.Find("LeftControllerAnchor").GetComponent<HandController>();
        rightHand = GameObject.Find("RightControllerAnchor").GetComponent<HandController>();

        if (anchors_in_the_scene == null) anchors_in_the_scene = GameObject.FindObjectsOfType<ObjectAnchor>();

        Debug.Log("ending the start sequence");
    }

    // Update is called once per frame
    void Update()
    {
        left_grasped = leftHand.handle_controller_behavior(anchors_in_the_scene);
        right_grasped = rightHand.handle_controller_behavior(anchors_in_the_scene);

        InteractionManager(leftHand, left_grasped);
        InteractionManager(rightHand, right_grasped);
    }

    void InteractionManager(HandController hand, ObjectAnchor object_grasped)
    {
        switch (object_grasped.name)
        {
            case ("Bow"):
                bow.GrabBow(object_grasped);
                break;
            case ("Hammer"):
                hammer.GrabHammer(object_grasped);
                break;
        }
    }
}