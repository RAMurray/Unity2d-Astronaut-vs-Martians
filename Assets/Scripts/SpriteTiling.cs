using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class SpriteTiling : MonoBehaviour {

	public int offsetX = 2;

	public bool hasRightBuddy = false; //Has Sprite to the right of it.
	public bool hasLeftBuddy = false; //Has Sprite to the left of it.

	public bool reverseScale = false;

	private float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform;

	void Awake() {
		cam = Camera.main;
		myTransform = transform;

		}

	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {

		// Does sprite need buddies next to it?
		if (!hasLeftBuddy || !hasRightBuddy) {
			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height; //Calculates width of camera's total view.

			//Calculate X position where Camera can see edge of the sprite
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth/2) + camHorizontalExtend;

			//checking if edge of element if visible. If it is then call make new buddy sprite.
			if(cam.transform.position.x >= edgeVisiblePositionRight - offsetX && !hasRightBuddy)
			{
				MakeNewBuddy(1);
				hasRightBuddy = true;
			}
			else if(cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && !hasLeftBuddy)
			{
				MakeNewBuddy(-1);
				hasLeftBuddy = true;

			}
		}
	
	}

	//Create buddy sprite on side of sprite for tiling effect.
	void MakeNewBuddy(int direction)
	{
		//calculating position for buddy sprite.
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * direction, myTransform.position.y, myTransform.position.z);
		//Instantating new buddy sprite and then storing it in variable.
		Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;

		//if not tilable reverse x size of object to make the tranisition seam look better.
		if (reverseScale) {
				newBuddy.localScale = new Vector3 (newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
		}

		newBuddy.parent = myTransform.parent;

		if(direction > 0 ) { newBuddy.GetComponent<SpriteTiling>().hasLeftBuddy = true; }
		else { newBuddy.GetComponent<SpriteTiling>().hasRightBuddy = true; }


	}
}
