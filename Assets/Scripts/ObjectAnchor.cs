using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectAnchor : MonoBehaviour
{

	[Header("Grasping Properties")]
	public float graspingRadius = 0.1f;
	public Rigidbody heldObjRB;
	public Collider[] collisionBoxes;
	protected HandController hand_controller = null;
	protected Transform initial_transform_parent;

	void Start()
	{
		initial_transform_parent = transform.parent;
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

	}

	public void detach_from(HandController hand_controller)
	{
		if (this.hand_controller != hand_controller) return;

		this.hand_controller = null;
		heldObjRB.transform.parent = null;

		heldObjRB.useGravity = true;
		heldObjRB.constraints = RigidbodyConstraints.None;
		heldObjRB.drag = 1;

		foreach (Collider v in collisionBoxes) { v.enabled = true; }
	}

	public bool is_available() { return hand_controller == null; }

	public float get_grasping_radius() { return graspingRadius; }

	public HandController.HandType get_hand_type() { return hand_controller.handType; }

	public Vector3 get_controller_position() { return hand_controller.transform.position; }
	public Quaternion get_controller_orientation() { return hand_controller.transform.rotation; }

	public HandController get_controller() { return hand_controller; }
}