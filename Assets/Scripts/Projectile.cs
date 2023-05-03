using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody projRB;
    Vector3 spawnPosition;
    public Material projectileMaterial;
    static int force_multiplicator = 20;
    void Start()
    {
        projRB = this.GetComponentInChildren<Rigidbody>();
        spawnPosition = GameObject.Find("BallSupport").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroyProjectile()
    {
        createProjectile();
        Destroy(this);
    }

    void createProjectile()
    {
        GameObject newProjectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        newProjectile.transform.position = spawnPosition;
        newProjectile.GetComponent<Rigidbody>().useGravity = false;
        newProjectile.AddComponent<MeshRenderer>();
        newProjectile.GetComponent<MeshRenderer>().material = projectileMaterial;
        newProjectile.AddComponent<Projectile>();
    }
}
