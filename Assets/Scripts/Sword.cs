using UnityEngine;

public class Sword : MonoBehaviour
{
    protected float force_multiplicator = 20.0f;
    public GameObject hitAnimation;
    Collider[] colliders = null;
    
    private void Start()
    {
        colliders = gameObject.GetComponentsInChildren<Collider>();
    }


    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "projectile")
        {
            col.rigidbody.AddForce(-col.GetContact(0).normal * force_multiplicator, ForceMode.Impulse);
            col.rigidbody.useGravity = true;
            GameObject anim = Instantiate(hitAnimation, col.rigidbody.transform.position, Quaternion.identity);
            anim.transform.SetParent(col.collider.transform);
            //heldObjRB.transform.parent = col.collider.transform;
        }

    }

    public void GrabSword(ObjectAnchor sword, Projectile[] projectiles, float threshold = 0.5f)
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            threshold = projectiles[i].gameObject.transform.localScale.x;
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
