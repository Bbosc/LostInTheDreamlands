using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    List<bool> tutorials_passed = new List<bool>();
    int tutorial_stage = 0;

    // Start is called before the first frame update
    void Start()
    {
        bow = GameObject.Find("Bow_").GetComponent<Bow>();
        sword = GameObject.Find("Sword_").GetComponent<Sword>();
        leftHand = GameObject.Find("LeftControllerAnchor").GetComponent<HandController>();
        rightHand = GameObject.Find("RightControllerAnchor").GetComponent<HandController>();

        if (anchors_in_the_scene == null) anchors_in_the_scene = FindObjectsOfType<ObjectAnchor>();

        for (int i = 0; i < 5; i ++) { tutorials_passed.Add(false); } // 5 for the number of interactions;
    }

    // Update is called once per frame
    void Update()
    {
        left_grasped = leftHand.handle_controller_behavior(anchors_in_the_scene);
        right_grasped = rightHand.handle_controller_behavior(anchors_in_the_scene);

        if (left_grasped != null) InteractionManager(left_grasped);
        if (right_grasped != null) InteractionManager(right_grasped);

        if (tutorials_passed[tutorial_stage]) tutorial_stage += 1;
    }

    void InteractionManager(ObjectAnchor object_grasped)
    {
        switch (object_grasped.name)
        {
            case ("Bow_"):
                bow.GrabBow(object_grasped);
                tutorials_passed[2] = bow.is_tutorial_completed();
                break;
            case ("Sword_"):
                sword.GrabSword(object_grasped);
                tutorials_passed[1] = sword.is_tutorial_completed();
                break;
            case ("Axe"):
                tutorials_passed[0] = object_grasped.is_tutorial_completed();
                break;
        }
    }

    public int get_tutorial_state() { return tutorial_stage; }
}
