using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class releaseCristal : MonoBehaviour
{
    //Get all fruits object
    static protected GameObject[] fruits_in_the_scene;
    static protected GameObject[] potion1_in_the_scene;
    static protected GameObject[] flower_in_the_scene;
    public Transform anchor_location;
    private Rigidbody rb;
    int MIN_DISTANCE_TO_ANCHOR = 1;

    //Potion recipe 
    int NBR_APPLE = 3;
    int NBR_FLOWER = 1;
    int NBR_POTION = 1;

    // Start is called before the first frame update
    void Start()
    {
        if ( fruits_in_the_scene == null ) fruits_in_the_scene = GameObject.FindGameObjectsWithTag("Fruit");
        if ( fruits_in_the_scene == null ) potion1_in_the_scene = GameObject.FindGameObjectsWithTag("Potion");
        if ( fruits_in_the_scene == null ) flower_in_the_scene = GameObject.FindGameObjectsWithTag("Flower");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // int nbr_of_fruits_at_anchor = 0;
        // for (int i = 0; i < fruits_in_the_scene.Length; ++i) {
        //     if (!(fruits_in_the_scene[i].GetComponent<objectToMove>().objectInTheContainer)) {
        //         if (Vector3.Distance(fruits_in_the_scene[i].transform.position, anchor_location.position) < 2) {
        //             ++nbr_of_fruits_at_anchor;
        //         }
        //     }
        // }
        int nbr_of_fruits_at_anchor = getNumberOfObjectsAtLocation(fruits_in_the_scene);
        int nbr_of_potions1_at_anchor = getNumberOfObjectsAtLocation(potion1_in_the_scene);
        int nbr_of_flowers_at_anchor = getNumberOfObjectsAtLocation(flower_in_the_scene);

        if (nbr_of_fruits_at_anchor >= NBR_APPLE && nbr_of_potions1_at_anchor >= NBR_POTION && nbr_of_flowers_at_anchor >= NBR_FLOWER) {
            rb.useGravity = true;
            MeshRenderer[] cristalsMesh = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer cristal in cristalsMesh) {
                cristal.enabled = true;
            }

            Light light = GetComponentInChildren<Light>();
            light.enabled = true;
        }
    }

    int getNumberOfObjectsAtLocation(GameObject[] listOfObjects) {
        int nbr_of_objects_at_anchor = 0;
        for (int i = 0; i < listOfObjects.Length; ++i) {
            if (!(listOfObjects[i].GetComponent<objectToMove>().objectInTheContainer)) {
                if (Vector3.Distance(listOfObjects[i].transform.position, anchor_location.position) < MIN_DISTANCE_TO_ANCHOR) {
                    ++nbr_of_objects_at_anchor;
                }
            }
        }
        return nbr_of_objects_at_anchor;
    }
}
