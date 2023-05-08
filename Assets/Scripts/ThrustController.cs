using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustController : MonoBehaviour
{
    
    public Vector3 currentForce;
    public Vector3 SpeedApply;
    private Rigidbody rigidBody;

    [SerializeField]
    private JetpackInput jet01, jet02;

    [SerializeField]
    private float addForceMulti = 1f;
    [SerializeField]
    private float maxVelocity = 10f;
    private Vector3 velocity = Vector3.zero;

    private float gravity = 0f;

    private CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        gravity = GetComponent<OVRPlayerController>().GravityModifier;

    }

    public void OnEnable()
    {
        jet01.EvtThrustChanged += ChangeForce;
        jet02.EvtThrustChanged += ChangeForce;

    }
    
    public void ChangeForce(Vector3 newForce)
    {
        currentForce = (jet01.CurrentThrust + jet02.CurrentThrust) /2f * addForceMulti;


    }



    public Vector3 TestVeloctity;



    private void FixedUpdate()
    {


        // if (currentForce != Vector3.zero)
        // {
        //     rigidBody.AddForce(currentForce);

        //     Vector3 velocity = rigidBody.velocity;
        //     Vector3 velocity_test = rigidBody.velocity;
        //     velocity = new Vector3(Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity), 
        //                         Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity), 
        //                         Mathf.Clamp(velocity.z, -maxVelocity, maxVelocity));
        //     GetComponent<OVRPlayerController>().GravityModifier = 0f;
        //     character.enabled = false;
        //     rigidBody.velocity = velocity;
        //     TestVeloctity = velocity;
        //     velocity_test = rigidBody.velocity;
        //     Debug.Log("force");
        //     Debug.Log(currentForce);
        //     Debug.Log("velo");
        //     Debug.Log(velocity);

        // }else{
        //     character.enabled = true;
        //     Debug.Log("gravity");
        //     Debug.Log(gravity);
        //     character.Move(Vector3.zero);
        //     GetComponent<OVRPlayerController>().GravityModifier = gravity;
        // }

        if (currentForce != Vector3.zero) 
        {   
            SpeedApply = currentForce/100f;
            GetComponent<OVRPlayerController>().GravityModifier = 0;
            velocity += SpeedApply;
            velocity = new Vector3(Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity), 
                    Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity), 
                    Mathf.Clamp(velocity.z, -maxVelocity, maxVelocity));
            Debug.Log(velocity);
            character.Move(velocity * Time.deltaTime);
        }else{

            velocity -= SpeedApply;
            velocity = new Vector3(Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity), 
                    Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity), 
                    Mathf.Clamp(velocity.z, -maxVelocity, maxVelocity));

            // check if velocity is close to zero in all 3 axis
            if (Mathf.Abs(velocity.x) < 10f)
            {
                SpeedApply.x = 0;
                Debug.Log("X");
            }
            if (Mathf.Abs(velocity.y) < 10f)
            {
                SpeedApply.y = 0;
                Debug.Log("Y");
            }
            if (Mathf.Abs(velocity.z) < 10f)
            {
                SpeedApply.z = 0;
                Debug.Log("Z");
            }
            if (character.collisionFlags != CollisionFlags.None)
            {
                velocity = Vector3.zero;
            }
            Debug.Log(velocity);
            character.Move(velocity * Time.deltaTime);

            GetComponent<OVRPlayerController>().GravityModifier = gravity/2;
            // character.Move(Vector3.zero);
        }

    }

    private void OnDisable()
    {
        jet01.EvtThrustChanged -= ChangeForce;
        jet02.EvtThrustChanged -= ChangeForce;

        // Debug.Log("force");
        // Debug.Log(currentForce);

    }



}
