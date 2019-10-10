using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] scenery; 		// An Array of all the background and foreground scenery to be parallaxed.
	private float[] parallaxScales;		//The proportion of the camera's movement to move the backgrounds by.
public float smoothing = 1f;			//How smooth the parallaxing is. IMPORTANT: Set it above 0 or it will not work

	private Transform cam;				//Reference to MainCamera's Transform.
	private Vector3 previousCamPosition; //Store position of camera in previous frame.

	//Call all functions before start but after all gameobjects been loaded (Great for refences)
	void Awake() {
		//set up Cam Reference.
		cam = Camera.main.transform;
		}

	// Use this for initialization
	void Start () {
		//The previous frame had current frame's camera position
		previousCamPosition = cam.position;

		//asigning coresponding parallaxScales
		parallaxScales = new float[scenery.Length];

		for (int i = 0; i < scenery.Length; i++) {
			parallaxScales[i] = scenery[i].position.z*-1;
				}
	
	}
	
	// Update is called once per frame
	void Update () {

		//for each scenery piece
		for (int i = 0; i <  scenery.Length; i++) {
				//the parallax is the opposite of the camer movement because previous frame was multiplied by the scale
				float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

				// set a target x position which is the current position pluse the parallax
			float bgTargetPosX = scenery[i].position.x + parallax;
				
			//creat target position which is the background's current position with it's target x position.
			Vector3 bgTargetPos = new Vector3(bgTargetPosX, scenery[i].position.y, scenery[i].position.z);

			//fade between current position and target position using lerp
			scenery[i].position = Vector3.Lerp (scenery[i].position, bgTargetPos, smoothing * Time.deltaTime);

		}

		//set previous camera position to camera's position at the end of frame.
		previousCamPosition = cam.position;
	
	}
}
