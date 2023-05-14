using UnityEngine;

public class ObjectAnchor_antoine : MonoBehaviour {

	[Header( "Grasping Properties" )]
	public float graspingRadius = 0.1f;

	// Store initial transform parent
	protected Transform initial_transform_parent;
	void Start () {
		initial_transform_parent = transform.parent;
	}


	// Store the hand controller this object will be attached to
	protected HandController_antoine hand_controller = null;
	protected distanceGrab dist_grab = null;

	public Transform trackingSpace;

	public void attach_to ( HandController_antoine hand_controller ) {
		// Store the hand controller in memory
		this.hand_controller = hand_controller;
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;

		// Set the object to be placed in the hand controller referential
		transform.SetParent( hand_controller.transform );
	}

	public void attach_to_distance ( distanceGrab dist_grab ) {
		// Store the hand controller in memory
		this.dist_grab = dist_grab;
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;

		// Set the object to be placed in the hand controller referential
		transform.SetParent( dist_grab.transform );
	}

	public void detach_from ( HandController_antoine hand_controller, HandController_antoine.HandType handType ) {
		// Make sure that the right hand controller ask for the release
		if ( this.hand_controller != hand_controller ) return;

		// Set the object to be placed in the original transform parent
		transform.SetParent( initial_transform_parent );

		// Detach the hand controller
		this.hand_controller = null;

		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.useGravity = true;
		rigidbody.isKinematic = false;
		if ( handType == HandController_antoine.HandType.LeftHand ) {
			rigidbody.velocity = trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
		} else {
			rigidbody.velocity = trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
		}
	}

	public void detach_from_distance ( distanceGrab dist_grab, distanceGrab.HandType handType ) {
		// Make sure that the right hand controller ask for the release
		if ( this.dist_grab != dist_grab ) return;

		// Set the object to be placed in the original transform parent
		transform.SetParent( initial_transform_parent );

		// Detach the hand controller
		this.dist_grab = null;

		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.useGravity = true;
		rigidbody.isKinematic = false;
		Debug.Log("Trow object");
		if ( handType == distanceGrab.HandType.LeftHand ) {
			Debug.Log("Throwing");
			rigidbody.velocity = trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
			rigidbody.velocity *= 10;
		} else {
			rigidbody.velocity = trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
			rigidbody.velocity *= 10;
		}
	}

	public bool is_available () { return hand_controller == null; }

	public float get_grasping_radius () { return graspingRadius; }
}