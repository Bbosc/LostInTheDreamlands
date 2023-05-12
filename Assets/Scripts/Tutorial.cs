using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    Transform canva = null;
    string[] panelNames = {"Introduction", "Introduction (1)", "Introduction (2)", "Introduction (3)",
                                "Bow", "Sword", "Grabbing", "Prompt", "DistanceGrab", "Jetpack"};
    List<GameObject> panels = new List<GameObject>();
    GameObject[] interactors;
    PanelManager pManager;
    OVRCameraRig cameraRig;
    int activePanelIndex = 0;
    bool pushed_previous_frame = false;
    // Start is called before the first frame update
    void Start()
    {
        
        canva = gameObject.GetComponentInChildren<RectTransform>();
        pManager = canva.GetComponent<PanelManager>();
        //panels = GameObject.FindGameObjectsWithTag("panel");
        foreach (string name in panelNames)
        {
            panels.Add(GameObject.Find(name));
        }
        interactors = GameObject.FindGameObjectsWithTag("interactor");
        foreach (GameObject t in panels) { t.SetActive(false); }
        foreach (GameObject t in panels) { Debug.LogWarning(t.name); }
        foreach (GameObject t in interactors) { t.SetActive(false); }

        cameraRig = FindObjectOfType<OVRCameraRig>();
        //canva.transform.parent = cameraRig.transform;
        //canva.transform.rotation = cameraRig.transform.rotation;
        //canva.transform.position = new Vector3(0, 2, 3);
        

        showPanel();
    }

    // Update is called once per frame
    void Update()
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




        if (Input.GetKeyDown("a"))
        {
            hidePanel();
            activePanelIndex += 1;
            showPanel();
        }
        if (Input.GetKeyDown("b"))
        {
            hidePanel();
            activePanelIndex -= 1;
            showPanel();
        }
        Debug.LogWarningFormat("active panel : {0}", panels[activePanelIndex].name);
    }

    void showPanel()
    {
        panels[activePanelIndex].SetActive(true);
        panels[activePanelIndex].GetComponentInChildren<Button>().onClick.AddListener(hidePanel);
        activateSupport(panels[activePanelIndex].name, true);
    }

    void hidePanel()
    {
        if (panels[activePanelIndex] != null) panels[activePanelIndex].SetActive(false);
        activateSupport(panels[activePanelIndex].name, false);
    }

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
            case ("Axe"):
                foreach (GameObject go in interactors)
                {
                    if (go.name == "AxeSupport")
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
}
