using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    Transform canvas = null;
    GameObject activePanel = null;
    int activePanelIndex = -1;
    void Start()
    {
        canvas = this.gameObject.GetComponentInChildren<RectTransform>();
        showPanel("Sword");
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (activePanel != null) buttonHandler(activePanel);
        
    }

    void showPanel(string panelName)
    {
        for (int i = 0; i < canvas.childCount; i++) { if (canvas.GetChild(i).gameObject.name == panelName) activePanelIndex = i; }
        activePanel = canvas.GetChild(activePanelIndex).gameObject;
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
        activePanel.SetActive(false);
        activePanel = null;
    }

    void hideAllPanel()
    {
        for (int i = 0; i < canvas.childCount; i++) { canvas.GetChild(i).gameObject.SetActive(false); }
    }
}
