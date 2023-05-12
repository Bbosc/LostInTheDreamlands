using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    Transform canva = null;
    GameObject[] panels;
    GameObject[] interactors;
    PanelManager pManager;
    OVRCameraRig cameraRig;
    int activePanelIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        canva = gameObject.GetComponentInChildren<RectTransform>();
        pManager = canva.GetComponent<PanelManager>();
        panels = GameObject.FindGameObjectsWithTag("panel");
        interactors = GameObject.FindGameObjectsWithTag("interactor");
        foreach (GameObject t in panels) { t.SetActive(false); }
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
        //if (OVRInput.Get(OVRInput.Button.One))
        //{
        //    hidePanel();
        //    activePanelIndex += 1;
        //    showPanel();
        //}
        //if (OVRInput.Get(OVRInput.Button.Two))
        //{
        //    hidePanel();
        //    activePanelIndex -= 1;
        //    showPanel();
        //}

        if (Input.GetKeyDown("a"))
        {
            Debug.Log("should go to the next one");
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
