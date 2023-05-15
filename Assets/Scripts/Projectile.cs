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
        spawnPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void launch(Transform launchPosition)
    {
        projRB.AddForce(-launchPosition.position.normalized * force_multiplicator, ForceMode.Impulse);
        projRB.useGravity = true;
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


    private void OnCollisionEnter(Collision col){

        Debug.Log("BALL COLLISION");
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "enemyboss"){
            // GameObject.Find('Skeleton').GetComponentInChildren<Enemy>().Dead = true;
            col.gameObject.GetComponentInParent<EnemyBoss>().Dead = true;
        }
    }
}
