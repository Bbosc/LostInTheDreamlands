using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// log only warnings : adb logcat Unity:W *:S
// log all informations : adb logcat Unity:I *:S

public class Colonel : MonoBehaviour
{

    static protected ObjectAnchor[] anchors_in_the_scene;
    HandController leftHand;
    HandController rightHand;
    protected ObjectAnchor left_grasped = null;
    protected ObjectAnchor right_grasped = null;
    Bow bow;
    Sword sword;

    // Start is called before the first frame update
    void Start()
    {
        bow = GameObject.Find("Bow").GetComponent<Bow>();
        sword = GameObject.Find("Sword").GetComponent<Sword>();
        leftHand = GameObject.Find("LeftControllerAnchor").GetComponent<HandController>();
        rightHand = GameObject.Find("RightControllerAnchor").GetComponent<HandController>();

        if (anchors_in_the_scene == null) anchors_in_the_scene = GameObject.FindObjectsOfType<ObjectAnchor>();

    }

    // Update is called once per frame
    void Update()
    {
        left_grasped = leftHand.handle_controller_behavior(anchors_in_the_scene);
        right_grasped = rightHand.handle_controller_behavior(anchors_in_the_scene);
        
        if (left_grasped != null) InteractionManager(left_grasped);
        if (right_grasped != null) InteractionManager(right_grasped);
    }

    void InteractionManager(ObjectAnchor object_grasped)
    {
        switch (object_grasped.name)
        {
            case ("Bow"):
                bow.GrabBow(object_grasped);
                break;
            case ("Sword"):
                sword.GrabSword(object_grasped);
                break;
        }
    }
}
