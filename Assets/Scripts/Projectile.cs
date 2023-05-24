using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 spawnPosition;
    public Material projectileMaterial;
    public GameObject explosionPrefab;
    public bool HitRock;

    void Start()
    {
        HitRock = false;
        spawnPosition = this.transform.position;
    }

    private void Update()
    {
        // We consider that any projectile 50 meters away from its spawn posiion is unusable
        // since it most likely fell. So we destroy it
        if (Vector3.Distance(this.transform.position, spawnPosition) > 50)
        {
            destroyProjectile();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        // In the tutorial : make projectile explode if enters in contact with an object tagged "target"
        if (collision.gameObject.CompareTag("target"))
        {
            explode(collision.GetContact(0).point);
            GameObject.Find("Sword_").GetComponent<Sword>().tutorial_completed = true; // marked this part of the tuto done
            destroyProjectile(); // destroy the projectile to complete explosion
        }
        // if object hits the boss skeleton, marked it as dead and make an explosion
        if (collision.gameObject.CompareTag("enemyboss")){
            collision.gameObject.GetComponentInParent<EnemyBoss>().Dead = true;
            explode(collision.GetContact(0).point);
        }
        if (collision.gameObject.tag == "rock"){
            HitRock = true;
        }
    }

    // we do not want to have unused projectile on the map, so we destroy them when needed
    public void destroyProjectile()
    {
        for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        Destroy(gameObject);
        
    }

    // this function instantiate an explosiion prefab at the position given in argument
    void explode(Vector3 position)
    {
        Destroy(Instantiate(explosionPrefab, position, Quaternion.identity), 1f);
    }

}
