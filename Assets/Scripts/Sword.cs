using UnityEngine;

public class Sword : MonoBehaviour
{
    protected float force_multiplicator = 15.0f;
    GameObject[] projectiles;
    ObjectAnchor sword_anchor;
    float dist = 0.0f;
    public bool tutorial_completed = false;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("projectile"))
        {
            col.rigidbody.AddForce(-col.GetContact(0).normal * force_multiplicator, ForceMode.Impulse);
            col.rigidbody.useGravity = true;
        }
        if (col.gameObject.tag == "enemy")
        {
            col.gameObject.GetComponentInParent<Enemy>().Dead = true;
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
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        projectiles = GameObject.FindGameObjectsWithTag("projectile");
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

    public bool is_tutorial_completed() { return tutorial_completed; }
}
