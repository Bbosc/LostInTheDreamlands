using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colonel : MonoBehaviour
{

    HandController handController;



    // Start is called before the first frame update
    void Start()
    {
        Bow bow = GameObject.Find("Bow").GetComponent<Bow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
