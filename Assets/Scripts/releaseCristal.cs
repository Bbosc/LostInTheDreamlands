using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class releaseCristal : MonoBehaviour
{
    //Get all fruits object
    static protected GameObject[] fruits_in_the_scene;
    public Transform anchor_location;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        if ( fruits_in_the_scene == null ) fruits_in_the_scene = GameObject.FindGameObjectsWithTag("Fruit");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        int nbr_of_fruits_at_anchor = 0;
        for (int i = 0; i < fruits_in_the_scene.Length; ++i) {
            if (Vector3.Distance(fruits_in_the_scene[i].transform.position, anchor_location.position) < 2) {
                ++nbr_of_fruits_at_anchor;
            };
        }

        if (nbr_of_fruits_at_anchor > 4) {
            rb.useGravity = true;
        }
    }
}
