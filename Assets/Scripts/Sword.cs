using UnityEngine;

public class Sword : MonoBehaviour
{
    protected float force_multiplicator = 20.0f;
    ObjectAnchor anchor;
    Collision lastCollision = null;
    Collider[] colliders = null;
    int colliderCount = 0;
    bool is_available = true;
    bool not_projectile = false;
    
    private void Start()
    {
        anchor = GetComponent<ObjectAnchor>();
        colliders = gameObject.GetComponentsInChildren<Collider>();
        //for (int i = 0; i < colliders.Length; i++) { colliders[i].enabled = true; }
    }

    private void Update()
    {
        is_available = anchor.is_available();


       
    }

    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "projectile")
        {
            col.rigidbody.AddForce(-col.GetContact(0).normal * force_multiplicator, ForceMode.Impulse);
            col.rigidbody.useGravity = true;
        }

    }

    public void GrabSword(ObjectAnchor sword, Projectile[] projectiles, float threshold = 0.5f)
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            threshold = projectiles[i].gameObject.transform.localScale.x;
            Debug.LogWarningFormat("target distance : {0}", threshold);
            Debug.LogWarningFormat("actual distance : {0}", Vector3.Distance(gameObject.transform.position, projectiles[i].transform.position));
            if (Vector3.Distance(gameObject.transform.position, projectiles[i].transform.position) < threshold + 0.2)
            {
                for (int j = 0; j < colliders.Length; j++) { colliders[j].enabled = true; }
            } else
            {
                for (int j = 0; j < colliders.Length; j++) { colliders[j].enabled = false; }
            }
            if (Vector3.Distance(gameObject.transform.position, projectiles[i].transform.position) > 100)
            {
                Destroy(projectiles[i]);
                break;
            }
        }

    }
}
