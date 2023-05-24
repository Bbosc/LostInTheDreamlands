using UnityEngine;

public class Sword : MonoBehaviour
{
    protected float force_multiplicator = 15.0f;
    GameObject[] projectiles;
    GameObject[] enemies;
    ObjectAnchor sword_anchor;
    float dist = 0.0f;
    public bool tutorial_completed = false;
    bool activate = false;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

    // We need the Collision object instead of the collider for its contatct points attributes
    // It makes it way easier to manage the trajectory of the propulsed object
    private void OnCollisionEnter(Collision col)
    {
        // if sword encountered a black sphere, then propulse it squarely w.r.t the contact surface
        if (col.gameObject.CompareTag("projectile"))
        {
            col.rigidbody.AddForce(-col.GetContact(0).normal * force_multiplicator, ForceMode.Impulse);
            col.rigidbody.useGravity = true;
        }
        // if sword encountered a skeleton
        if (col.gameObject.CompareTag("enemy"))
        {
            col.gameObject.GetComponentInParent<Enemy>().Dead = true;
        }

    }


    private void OnCollisionExit(Collision collision)
    {
        // when the collision ended, make sure to snap back the sword position into the controller
        // otherwise there can be some shift in the sword's position
        if (collision.gameObject.CompareTag("projectile") || collision.gameObject.CompareTag("enemy"))
        {
            transform.position = sword_anchor.get_controller_position();
        }
    }

    public void GrabSword(ObjectAnchor sword)
    {
        // gameObject.GetComponent<Rigidbody>().useGravity = true;
        // Update the list of available projectiles and ennemies
        projectiles = GameObject.FindGameObjectsWithTag("projectile");
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        sword_anchor = sword; // update the default sword position when in hand
        transform.position = sword_anchor.get_controller_position();
        bool activate = false;

        for (int i = 0; i < projectiles.Length; i++)
        {
            dist = Vector3.Distance(sword.gameObject.transform.position, projectiles[i].gameObject.transform.position);
            if (dist < 0.8)
            {
                foreach (Collider col in sword.collisionBoxes) { col.enabled = true; }
                transform.position = sword_anchor.get_controller_position();
                activate = true;
            }
            else
            {
        // activate/desactivate colliders if the sword is close/far enough from the ennemies or the projectiles
                if (activate == false)
                {
                    foreach (Collider col in sword.collisionBoxes) { col.enabled = false; }
                    transform.position = sword_anchor.get_controller_position();
                }

            }
        }


        for (int i = 0; i < enemies.Length; i++)
        {
            dist = Vector3.Distance(sword.gameObject.transform.position, enemies[i].gameObject.transform.position);
            if (dist < 2)
            {
                activate = true;
                foreach (Collider col in sword.collisionBoxes) { col.enabled = true; }
                transform.position = sword_anchor.get_controller_position();
            }
            else
            {
                if (activate == false)
                {
                    foreach (Collider col in sword.collisionBoxes) { col.enabled = false; }
                    transform.position = sword_anchor.get_controller_position();
                }
            }

        }
    }
        public bool is_tutorial_completed() { return tutorial_completed; }
}















//         activateSwordColliders(sword, projectiles, 0.8f);
//         activateSwordColliders(sword, enemies, 2.0f);

        
//     }

//     // We activate the sword colliders only when the sword is close to an object it can interact with,
//     // otherwise it could collide with an unwanted object, e.g. a tree, and provoke a shift in the sword's position
//     void activateSwordColliders(ObjectAnchor sword, GameObject[] interactables, float activatingDistance)
//     {
//         for (int i = 0; i < interactables.Length; i++)
//         {
//             dist = Vector3.Distance(sword.gameObject.transform.position, interactables[i].gameObject.transform.position);
//             if (dist < activatingDistance)
//             {
//                 foreach (Collider col in sword.collisionBoxes) { col.enabled = true; }
//                 activate = true;
//             }
//             else
//             {
//                 if (activate == false)
//                 {
//                     foreach (Collider col in sword.collisionBoxes) { col.enabled = false; }
//                     transform.position = sword_anchor.get_controller_position();
//                 }

//             }
//         }
//     }


