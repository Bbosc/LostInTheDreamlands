using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // This script make an arrow stick to an object tagged 'target'
    // It also switch the tutorial_completed boolean of the bow part to true,
    // indicating that the player successfully used the bow for the first time.

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("target"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GameObject.Find("Bow_").GetComponent<Bow>().tutorial_completed = true;
        }
    }
}
