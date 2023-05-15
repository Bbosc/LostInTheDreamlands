using UnityEngine;

// Raycast video : https://www.youtube.com/watch?v=F60UIo7Y1YY

public class distanceGrab : MonoBehaviour
{
    // Store the hand type to know which button should be pressed
	public enum HandType : int { LeftHand, RightHand };
	[Header( "Hand Properties" )]
	public HandType handType;


    //FSM for grabing behaviour
    enum graspingState{IDLE, SHOW_RAYCAST, GRAB_OBJECT};
    graspingState state;

    //Raycast variables
    public float ray_max_len;
    public float ray_width;
    public LineRenderer displayedRay;
    public GameObject go;

    // Store all gameobjects containing an Anchor
    static protected ObjectAnchor[] anchors_in_the_scene;
	void Start () {
		// Prevent multiple fetch
		if ( anchors_in_the_scene == null ) anchors_in_the_scene = FindObjectsOfType<ObjectAnchor>();
        state = graspingState.IDLE;

        //Set up the line renderer
        // Vector3[] startLinePos = new Vector3[2] {Vector3.zero, Vector3.zero};
        Vector3 startLinePos = go.transform.position;
        displayedRay.SetPosition(1, startLinePos);
        displayedRay.enabled = false;
        displayedRay.positionCount = 2;
    }

    // This method checks that the hand is closed depending on the hand side
	protected bool is_hand_closed () {
		// Case of a left hand
		if ( handType == HandType.LeftHand ) return
			OVRInput.Get( OVRInput.RawButton.A )     // Check that the A is pressed
			&& OVRInput.Get( OVRInput.RawButton.B );    // Check that the B is pressed


		// Case of a right hand
		else return
			OVRInput.Get( OVRInput.RawButton.A )    // Check that the A is pressed
			&& OVRInput.Get(OVRInput.RawButton.B );  // Check that the B is pressed
	}

    // This method checks if the raycast should be displayed 
	protected bool is_ray_cast() {
		// Case of a left hand
		if ( handType == HandType.LeftHand ) return
			OVRInput.Get( OVRInput.RawButton.A );     // Check that the A is pressed

		// Case of a right hand
		else return
			OVRInput.Get( OVRInput.RawButton.A );   // Check that the A is pressed
    }

    void displayRayCast(Vector3 target, Vector3 direction, float len) {
        displayedRay.enabled = true;
        
        RaycastHit hit;
        Ray ray = new Ray(target, direction);

        //Set up end of the line renderer
        Vector3 endLineRenderer = target + (len*direction);

        if (Physics.Raycast(ray, out hit)) {
            endLineRenderer = hit.point;
        }

        displayedRay.SetPosition(0, target);
        displayedRay.SetPosition(1, endLineRenderer);
    }

    ObjectAnchor getObjectHit(Vector3 target, Vector3 direction) {
        RaycastHit hit;
        Ray ray = new Ray(target, direction);

        if (Physics.Raycast(ray, out hit)) {
            GameObject objectHit = hit.collider.gameObject;

            if (objectHit.GetComponent<ObjectAnchor>()) {
                return objectHit.GetComponent<ObjectAnchor>();
            }
        }
        return null;
    }

    // Automatically called at each frame
	void Update () { handle_distance_grabbing(); }

	// Store the object atached to this hand
	// N.B. This can be extended by using a list to attach several objects at the same time
	protected ObjectAnchor object_grasped = null;

	/// <summary>
	/// This method handles the linking of distant object anchors to this distance grabber
	/// </summary>
	protected void handle_distance_grabbing () {
        if (state == graspingState.IDLE) {
            bool show_raycast = is_ray_cast();
            
            if (show_raycast) {
                state = graspingState.SHOW_RAYCAST;
                //display raycast
                displayRayCast(transform.position, transform.forward, ray_max_len);
            }
        }

        if (state == graspingState.SHOW_RAYCAST) {
            bool hand_closed = is_hand_closed();
            bool show_raycast = is_ray_cast();

            if (hand_closed) {
                state = graspingState.GRAB_OBJECT;
                //grab the object pointed
                displayedRay.enabled = false;
                object_grasped = getObjectHit(transform.position, transform.forward);
                object_grasped.attach_to_distance( this );
            }

            if (show_raycast && !hand_closed) {
                //display raycast
                displayRayCast(transform.position, transform.forward, ray_max_len);
            }

            if (!show_raycast && !hand_closed) {
                //return to IDLE
                state = graspingState.IDLE;
                displayedRay.enabled = false;
            }
        }

        if (state == graspingState.GRAB_OBJECT) {
            bool hand_closed = is_hand_closed();

            if (!hand_closed) {
                state = graspingState.IDLE;
                //release the object
                displayedRay.enabled = false;
                object_grasped.detach_from_distance( this, handType );
            }
        }
    }
}
