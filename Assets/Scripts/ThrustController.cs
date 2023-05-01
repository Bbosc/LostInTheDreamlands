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
    private Vector3 Friction = Vector3.zero;

    private float gravity = 0f;

    private CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        gravity = GetComponent<OVRPlayerController>().GravityModifier;

        Friction.x = 3f;
        Friction.y = 3f;
        Friction.z = 3f;

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


    private void FixedUpdate()
    {


        if (currentForce != Vector3.zero) 
        {   
            
            SpeedApply = currentForce/100f;
            GetComponent<OVRPlayerController>().GravityModifier = 0;
            velocity += SpeedApply;
            velocity = velocity*0.9f;

            //velocity = (velocity - Friction);

            velocity = new Vector3(Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity), 
                    Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity), 
                    Mathf.Clamp(velocity.z, -maxVelocity, maxVelocity));
            Debug.Log(velocity);
            character.Move(velocity * Time.deltaTime);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);



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

            GetComponent<OVRPlayerController>().GravityModifier = gravity;
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }

    }

    private void OnDisable()
    {
        jet01.EvtThrustChanged -= ChangeForce;
        jet02.EvtThrustChanged -= ChangeForce;

    }



}
