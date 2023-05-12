using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    Vector3 spawnPosition;
    public Material projectileMaterial;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0) createProjectile();
    }

    void createProjectile()
    {
        GameObject newProjectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        newProjectile.transform.parent = this.gameObject.transform;
        newProjectile.gameObject.transform.position = spawnPosition;
        Debug.Log(newProjectile.transform.position);
        newProjectile.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        newProjectile.tag = "projectile";
        newProjectile.AddComponent<Rigidbody>();
        newProjectile.GetComponent<Rigidbody>().mass = 1;
        newProjectile.GetComponent<Rigidbody>().useGravity = false;
        newProjectile.AddComponent<SphereCollider>();
        newProjectile.GetComponent<SphereCollider>().radius = 0.5f;
        newProjectile.AddComponent<MeshRenderer>();
        newProjectile.GetComponent<MeshRenderer>().material = projectileMaterial;
        Projectile script = newProjectile.AddComponent<Projectile>();
        script.explosionPrefab = explosionPrefab;
    }
}
