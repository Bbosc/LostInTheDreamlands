using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Target : MonoBehaviour
{
    public GameObject door;
    public GameObject target_main;
    public GameObject explosionPrefab;
    // public GameObject Fragment;d
    // public GameObject cauldron;
    // ConstraintSource constraintSource;
    Animator anim_door;
    Animator anim_target;

    // Start is called before the first frame update
    void Start()
    {
        anim_door = door.GetComponent<Animator>();
        anim_target = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
       if (collision.gameObject.CompareTag("arrow"))
        {
            anim_target.SetTrigger("Hit_target");
            Destroy(Instantiate(explosionPrefab, transform.parent.position, Quaternion.identity), 1f);
            anim_door.SetBool("Open", true);
            Destroy(target_main);
            

        }
    }
}
