using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    Transform canvas = null;
    GameObject activePanel = null;
    GameObject[] supports;
    int activePanelIndex = -1;
    void Start()
    {
        canvas = this.gameObject.GetComponentInChildren<RectTransform>();
        supports = GameObject.FindGameObjectsWithTag("support");
        Debug.Log(supports[0].name.Remove(supports[0].name.Length - 7));

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One)) hidePanel();
    }

    public void handlePanels(Vector3 playerPosition)
    {
        for (int i = 0; i < supports.Length; i++)
        {
            if (Vector3.Distance(playerPosition, supports[i].transform.position) < 1.0f)
            {
                showPanel(supports[i].name.Remove(supports[i].name.Length - 7));
            }
        }
    }

    Vector3 getSupport(string panelName)
    {
        return GameObject.Find(panelName + "Support").transform.position;

    }

    void showPanel(string panelName)
    {
        for (int i = 0; i < canvas.childCount; i++) { if (canvas.GetChild(i).gameObject.name == panelName) activePanelIndex = i; }
        activePanel = canvas.GetChild(activePanelIndex).gameObject;
        activePanel.transform.position = getSupport(panelName) + new Vector3(0f, 1.5f, 0f);
        activePanel.SetActive(true);
        Debug.Log(activePanel.GetComponentInChildren<Button>());
        activePanel.GetComponentInChildren<Button>().onClick.AddListener(hidePanel);
    }

    void showAllPanel(string panelName)
    {
        for (int i = 0; i < canvas.childCount; i++) { canvas.GetChild(i).gameObject.SetActive(true); }
    }

    void hidePanel()
    {
        if (activePanel != null) activePanel.SetActive(false);
        activePanel = null;
    }

    void hideAllPanel()
    {
        for (int i = 0; i < canvas.childCount; i++) { canvas.GetChild(i).gameObject.SetActive(false); }
    }
}
