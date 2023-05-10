using UnityEngine;

public class Sword : MonoBehaviour
{
    protected float force_multiplicator = 20.0f;
    GameObject[] projectiles;
    ObjectAnchor sword_anchor;
    Vector3 basic_pos;
    float dist = 0.0f;

    private void Update()
    {
        projectiles = GameObject.FindGameObjectsWithTag("projectile");
        for (int i = 0; i < projectiles.Length; i++)
        {
            dist = Vector3.Distance(this.gameObject.transform.position, projectiles[i].gameObject.transform.position);
            if (dist < 0.6)
            {
                for (int j = 0; j < sword_anchor.collisionBoxes.Length; j++) { sword_anchor.collisionBoxes[j].enabled = true; }
            }
            else if (!sword_anchor.is_available())
            {
                for (int j = 0; j < sword_anchor.collisionBoxes.Length; j++) { sword_anchor.collisionBoxes[j].enabled = false; }
            }

        }
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "projectile")
        {
            col.rigidbody.AddForce(-col.GetContact(0).normal * force_multiplicator, ForceMode.Impulse);
            col.rigidbody.useGravity = true;
        }
        else
        {
            this.gameObject.GetComponentsInChildren<Collider>();
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "projectile")
        {
            //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GameObject.Find("Sword").transform.position = basic_pos;
            this.gameObject.transform.position = basic_pos;
        }
    }

    public void GrabSword(ObjectAnchor sword)
    {
        //GameObject[] projectiles = GameObject.FindGameObjectsWithTag("projectile");

        sword_anchor = sword;
        basic_pos = sword.gameObject.transform.position;
        //float dist = 0.0f;
        //for (int i = 0; i < projectiles.Length; i++)
        //{
        //    dist = Vector3.Distance(sword.gameObject.transform.position, projectiles[i].gameObject.transform.position);
        //    if (dist < 0.6)
        //    {
        //        for (int j = 0; j < sword.collisionBoxes.Length; j++) { sword.collisionBoxes[j].enabled = true; }
        //    } else
        //    {
        //        for (int j = 0; j < sword.collisionBoxes.Length; j++) { sword.collisionBoxes[j].enabled = false; }
        //    }

        //}

    }
}
