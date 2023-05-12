using UnityEngine;

public class Sword : MonoBehaviour
{
    protected float force_multiplicator = 10.0f;
    GameObject[] projectiles;
    ObjectAnchor sword_anchor;
    float dist = 0.0f;


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("projectile"))
        {
            col.rigidbody.AddForce(-col.GetContact(0).normal * force_multiplicator, ForceMode.Impulse);
            col.rigidbody.useGravity = true;
        }

    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
            transform.position = sword_anchor.get_controller_position();
        }
    }

    public void GrabSword(ObjectAnchor sword)
    {
        projectiles = GameObject.FindGameObjectsWithTag("projectile");
        Debug.LogWarning(projectiles.Length);
        sword_anchor = sword;
        for (int i = 0; i < projectiles.Length; i++)
        {
            dist = Vector3.Distance(sword.gameObject.transform.position, projectiles[i].gameObject.transform.position);
            if (dist < 0.8)
            {
                foreach (Collider col in sword.collisionBoxes) { col.enabled = true; }
            }
            else
            {
                foreach (Collider col in sword.collisionBoxes) { col.enabled = false; }
                transform.position = sword_anchor.get_controller_position();
            }

        }

    }
}
