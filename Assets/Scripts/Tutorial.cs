using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // This script should manage the events related to the tutorial, such as the canvas containing the explanations
    // and the activation of the corresponding activity, depending on what the player has achieved so far
    Transform canva = null;
    string[] panelNames = {"Introduction", "Introduction (1)", "Introduction (2)", "Introduction (3)", "Grabbing",
                                "Prompt", "Sword", "Bow", "DistanceGrab", "Jetpack"};
    List<GameObject> panels = new List<GameObject>();
    GameObject[] interactors;
    int activePanelIndex = 0;
    bool pushed_previous_frame = false;
    Colonel colonel;
    int tutorial_stage = 0;
    int previous_stage = 0;

    // Start is called before the first frame update
    void Start()
    {
        colonel = FindObjectOfType<Colonel>();
        canva = gameObject.GetComponentInChildren<RectTransform>();
        foreach (string name in panelNames)
        {
            panels.Add(GameObject.Find(name));
        }
        interactors = GameObject.FindGameObjectsWithTag("interactor");
        foreach (GameObject t in panels) { t.SetActive(false); }
        foreach (GameObject t in interactors) { t.SetActive(false); }

      
        showPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            // get the tutorial stage from the colonel instance
            // hide previous panel and skip to the next one if a new stage of the tutorial
            // has been completed
            tutorial_stage = colonel.get_tutorial_state();
            if (tutorial_stage != previous_stage)
            {
                hidePanel();
                activePanelIndex += 1;
                showPanel();
                pushed_previous_frame = true;
                previous_stage = tutorial_stage;
            }
            // If it's still the introduction, use A and B to navigate the informations panels
            if (activePanelIndex < 5)
            {
                if (OVRInput.Get(OVRInput.Button.One) && (!pushed_previous_frame))
                {
                    hidePanel();
                    activePanelIndex += 1;
                    showPanel();
                    pushed_previous_frame = true;
                }
                if (OVRInput.Get(OVRInput.Button.Two) && (!pushed_previous_frame))
                {
                    hidePanel();
                    activePanelIndex -= 1;
                    showPanel();
                    pushed_previous_frame = true;
                }
                if (!OVRInput.Get(OVRInput.Button.Two) && !OVRInput.Get(OVRInput.Button.One)) pushed_previous_frame = false;
            }
        }
    }

    // the panels already exist, so we only have to hide or show them depending on the tutorial stage
    void showPanel()
    {
        if (activePanelIndex > 0) set_panel_position(panels[activePanelIndex-1].name);
        panels[activePanelIndex].SetActive(true);
        if (activePanelIndex < 5)
            panels[activePanelIndex].GetComponentInChildren<Button>().onClick.AddListener(hidePanel);
        activateSupport(panels[activePanelIndex].name, true);
    }

    void hidePanel()
    {
        if (panels[activePanelIndex] != null) panels[activePanelIndex].SetActive(false);
        activateSupport(panels[activePanelIndex].name, false);
    }

    // Depending on the panel name, set the corresponding stand active or inactive
    void activateSupport(string name, bool value)
    {
        switch (name)
        {
            case ("Sword"):
                foreach (GameObject go in interactors)
                {
                    if ((go.name == "SwordSupport") || (go.name == "BallSupport"))
                    {
                        go.SetActive(value);
                    }
                }
                break;
            case ("Bow"):
                foreach (GameObject go in interactors)
                {
                    if (go.name == "BowSupport")
                    {
                        go.SetActive(value);
                        break;
                    }
                }
                break;
            case ("Prompt"):
                foreach (GameObject go in interactors)
                {
                    if (go.name == "AxeSupport")
                    {
                        go.SetActive(value);
                        break;
                    }

                }
                break;
            case ("DistanceGrab"):
                foreach (GameObject go in interactors)
                {
                    if (go.name == "DistanceGrabSupport")
                    {
                        go.SetActive(value);
                        break;
                    }
                }
                break;
            case ("Jetpack"):
                foreach (GameObject go in interactors)
                {
                    if (go.name == "JetpackSupport")
                    {
                        go.SetActive(value);
                        break;
                    }

                }
                break;
            default:
                break;
        }
    }

    // To make sure the player sees them, the panels position of the (n + 1)-th interaction shows at the n-th stand
    // we also want it to be slightly off the support's position to avoid having the panel onto the player
    // we make the panel looks at the player by using LookAt, since it's more user-friendly
    void set_panel_position(string support_name)
    {
        foreach (GameObject go in interactors)
        {
            if (go.name == support_name + "Support")
            {
                canva.transform.position = go.transform.position + 2*Vector3.up + 2*Vector3.forward;
                canva.transform.LookAt(GameObject.Find("OVRPlayerController").transform);
                canva.transform.Rotate(0, 180, 0);
            }
            if ((support_name == "Prompt") && (go.name == "AxeSupport"))
            {
                canva.transform.position = go.transform.position + 2 * Vector3.up + 2*Vector3.forward;
                canva.transform.LookAt(GameObject.Find("OVRPlayerController").transform);
                canva.transform.Rotate(0, 180, 0);
            }
        }
    }
}
