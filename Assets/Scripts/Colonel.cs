using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// log only warnings : adb logcat Unity:W *:S
// log all informations : adb logcat Unity:I *:S

public class Colonel : MonoBehaviour
{

    static protected ObjectAnchor[] anchors_in_the_scene;
    static protected Vector3[] anchors_spawn_position;
    
    HandController leftHand;
    HandController rightHand;
    protected ObjectAnchor left_grasped = null;
    protected ObjectAnchor right_grasped = null;
    Bow bow;
    Sword sword;
    List<bool> tutorials_passed = new List<bool>();
    int tutorial_stage = 0;
    bool reloaded = false;

    protected CharacterController character_controller;

    // Start is called before the first frame update
    void Start()
    {
        bow = GameObject.Find("Bow_").GetComponent<Bow>();
        sword = GameObject.Find("Sword_").GetComponent<Sword>();
        leftHand = GameObject.Find("LeftControllerAnchor").GetComponent<HandController>();
        rightHand = GameObject.Find("RightControllerAnchor").GetComponent<HandController>();
        if (anchors_in_the_scene == null) anchors_in_the_scene = FindObjectsOfType<ObjectAnchor>();
        List<Vector3> listOfSpawnPositions = new List<Vector3>();
        foreach (ObjectAnchor oa in anchors_in_the_scene) { listOfSpawnPositions.Add(oa.gameObject.transform.position); }
        anchors_spawn_position = listOfSpawnPositions.ToArray();
        for (int i = 0; i < 5; i ++) { tutorials_passed.Add(false); } // 5 for the number of interactions;
        character_controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
 
        left_grasped = leftHand.handle_controller_behavior(anchors_in_the_scene);
        right_grasped = rightHand.handle_controller_behavior(anchors_in_the_scene);

        if (left_grasped != null) InteractionManager(left_grasped);
        if (right_grasped != null) InteractionManager(right_grasped);

        
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (tutorials_passed[tutorial_stage]) tutorial_stage += 1;

            if (tutorials_passed[2] && !tutorials_passed[3])
            {
                GameObject.Find("TreeStump").transform.position = GameObject.Find("DistanceGrabSupport").transform.position;

                if (Vector3.Distance(GameObject.Find("Cubedist").gameObject.transform.position, GameObject.Find("TreeStump").gameObject.transform.position) < 0.8)
                {
                    tutorials_passed[3] = true;
                    tutorial_stage += 1;
                }
            }
            if (tutorials_passed[3])
            {
                if (Vector3.Distance(GameObject.Find("Fragment_Tuto").gameObject.transform.position, GameObject.Find("Cauldron_Tuto").gameObject.transform.position) < 1.5f)
                {
                    GameObject.Find("Fragment_Tuto").gameObject.transform.position = GameObject.Find("Cauldron_Tuto").gameObject.transform.position + new Vector3(0, 0.5f, 0);
                    SceneManager.LoadScene("Scenes/Game");
                    anchors_in_the_scene = null;
                    anchors_spawn_position = null;
                }
            }
        } else
        {
            if (!reloaded)
            {
                Start(); // if the scene has been changed but has not reloaded its attributes yet
                reloaded = true;
            }
        }
        manageRespawn(-20);
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

    void manageRespawn(float altitudeThreshold)
    {
        for (int i = 0; i < anchors_in_the_scene.Length; i++)
        {
            if (anchors_in_the_scene[i].gameObject.transform.position.y < altitudeThreshold)
            {
                anchors_in_the_scene[i].gameObject.transform.position = anchors_spawn_position[i];
                anchors_in_the_scene[i].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
       
        if (GameObject.Find("OVRPlayerController").transform.position.y < altitudeThreshold)
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                character_controller.Move(GameObject.Find("Temple").transform.position - transform.position);
            }
            else
            {
                character_controller.Move(new Vector3(0, 1.5f, 0) - transform.position);
            }
        }
    }

    public int get_tutorial_state() { return tutorial_stage; }
}
