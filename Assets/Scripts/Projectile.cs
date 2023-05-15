using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 spawnPosition;
    public Material projectileMaterial;
    public GameObject explosionPrefab;

    void Start()
    {
        spawnPosition = this.transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, spawnPosition) > 50)
        {
            destroyProjectile();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("target"))
        {
            explode(collision.GetContact(0).point);
            GameObject.Find("Sword_").GetComponent<Sword>().tutorial_completed = true;
            destroyProjectile();
        }

        if (col.gameObject.tag == "enemyboss"){
            col.gameObject.GetComponentInParent<EnemyBoss>().Dead = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
       if (collision.gameObject.CompareTag("target"))
        {
        }
    }

    public void destroyProjectile()
    {
        for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        Destroy(gameObject);
        
    }


    void explode(Vector3 position)
    {
        Destroy(Instantiate(explosionPrefab, position, Quaternion.identity), 1f);
    }

}
