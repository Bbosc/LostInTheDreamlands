using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("target"))
        {
            Debug.LogWarning("entered collision");
            GetComponent<Rigidbody>().isKinematic = true;
            GameObject.Find("Bow_").GetComponent<Bow>().tutorial_completed = true;
        }
    }
}
