using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectAnchor : MonoBehaviour
{

	[Header("Grasping Properties")]
	public float graspingRadius = 0.1f;
	public Rigidbody heldObjRB;
	public Collider[] collisionBoxes;
	public Transform trackingSpace;
	protected HandController hand_controller = null;
	protected Transform initial_transform_parent;
	bool tutorial_completed = false;
	protected distanceGrab dist_grab = null;

	void Start()
	{
		initial_transform_parent = transform.parent;
		trackingSpace = GameObject.Find("TrackingSpace").transform;
		heldObjRB = GetComponent<Rigidbody>();
		collisionBoxes = GetComponentsInChildren<Collider>();
	}


	public void attach_to(HandController hand_controller)
	{
		this.hand_controller = hand_controller;
		transform.SetParent(hand_controller.transform);
		heldObjRB.transform.parent = hand_controller.transform;
		heldObjRB.useGravity = false;
		//heldObjRB.drag = 10;
		heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

		for (int i = 0; i < collisionBoxes.Length; i++) { collisionBoxes[i].enabled = false; }
		if (heldObjRB.gameObject.name == "Axe") tutorial_completed = true;
	}

	public void detach_from(HandController hand_controller, HandController.HandType handType)
	{
		if (this.hand_controller != hand_controller) return;

		this.hand_controller = null;
		heldObjRB.transform.parent = null;

		heldObjRB.useGravity = true;
		heldObjRB.constraints = RigidbodyConstraints.None;
		heldObjRB.drag = 1;

		heldObjRB.isKinematic = false;
		if (handType == HandController.HandType.LeftHand)
		{
			heldObjRB.velocity = trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
		}
		else
		{
			heldObjRB.velocity = trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
		}

		foreach (Collider v in collisionBoxes) { v.enabled = true; }
	}

	public void attach_to_distance(distanceGrab dist_grab)
	{
		// Store the hand controller in memory
		this.dist_grab = dist_grab;
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;

		// Set the object to be placed in the hand controller referential
		transform.SetParent(dist_grab.transform);
	}

	public void detach_from_distance(distanceGrab dist_grab, distanceGrab.HandType handType)
	{
		// Make sure that the right hand controller ask for the release
		if (this.dist_grab != dist_grab) return;

		// Set the object to be placed in the original transform parent
		transform.SetParent(initial_transform_parent);

		// Detach the hand controller
		this.dist_grab = null;

		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.useGravity = true;
		rigidbody.isKinematic = false;
		if (handType == distanceGrab.HandType.LeftHand)
		{
			rigidbody.velocity = trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
			rigidbody.velocity *= 2;
		}
		else
		{
			rigidbody.velocity = trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
			rigidbody.velocity *= 2;
		}
	}

	public bool is_available() { return hand_controller == null; }

	public float get_grasping_radius() { return graspingRadius; }

	public HandController.HandType get_hand_type() { return hand_controller.handType; }

	public Vector3 get_controller_position() { return hand_controller.transform.position; }
	public Quaternion get_controller_orientation() { return hand_controller.transform.rotation; }

	public HandController get_controller() { return hand_controller; }

	public bool is_tutorial_completed() { return tutorial_completed; }
}