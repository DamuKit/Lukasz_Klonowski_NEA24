using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	float speed;
	Animator Animation;
	string direction;
	bool moving;
	float camerasizex;
	float camerasizey;
	// Use this for initialization
	void Start () {
		speed = 0.05f;
		direction = "down";
		moving = false;
		camerasizex = 12f;
		camerasizey = 8f;
		Animation = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		moving = false;
		Animation.SetBool ("walk", false);
		if (Input.GetKey (KeyCode.A) == true) {
			this.transform.Translate (-speed, 0, 0);
			moving = true;
			Animation.SetBool ("walk", true);
		} else if (Input.GetKey (KeyCode.D) == true) {
			this.transform.Translate (speed, 0, 0);
			moving = true;
			Animation.SetBool ("walk", true);
		}
			
		if (Input.GetKey (KeyCode.W) == true) {
			this.transform.Translate (0, speed, 0);
			moving = true;
			Animation.SetBool ("walk", true);
		} else if (Input.GetKey (KeyCode.S) == true) {
			this.transform.Translate (0, -speed, 0);
			moving = true;
			Animation.SetBool ("walk", true);
		}

		if(Input.mousePosition.x-(Display.main.systemWidth/2)>=0 & Input.mousePosition.y-(Display.main.systemHeight/2)>= 0)
		{
			Debug.Log ("e");
		}
		if (Input.mousePosition.x - (Display.main.systemWidth / 2) - ((this.transform.position.x / camerasizex)* (Display.main.systemWidth/2))  > Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) - ((this.transform.position.y / camerasizey)* (Display.main.systemHeight/2)))) {
			//Debug.Log((this.transform.position.x / camerasizex)* (Display.main.systemWidth/2));
			//Debug.Log (Input.mousePosition.x - (Display.main.systemWidth / 2));
			direction = "right";
			Debug.Log ("Right");
			Animation.SetBool ("right", true);
			Animation.SetBool ("left", false);
			Animation.SetBool ("up", false);
			Animation.SetBool ("down", false);

		} else if (Input.mousePosition.x - (Display.main.systemWidth / 2) - ((this.transform.position.x / camerasizex)* (Display.main.systemWidth/2)) < -Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) - ((this.transform.position.y / camerasizey)* (Display.main.systemHeight/2)))) {
			direction = "left";
			Debug.Log ("Left");
			Animation.SetBool ("left", true);
			Animation.SetBool ("right", false);
			Animation.SetBool ("up", false);
			Animation.SetBool ("down", false);

		} else if (Mathf.Abs(Input.mousePosition.x - (Display.main.systemWidth / 2) - ((this.transform.position.x / camerasizex)* (Display.main.systemWidth/2))) < Input.mousePosition.y - (Display.main.systemHeight / 2)  - ((this.transform.position.y / camerasizey)* (Display.main.systemHeight/2))) {
			direction = "up";
			Debug.Log ("up");
			Animation.SetBool ("up", true);
			Animation.SetBool ("right", false);
			Animation.SetBool ("left", false);
			Animation.SetBool ("down", false);

		} else if (-Mathf.Abs(Input.mousePosition.x - (Display.main.systemWidth / 2) - ((this.transform.position.x / camerasizex)* (Display.main.systemWidth/2))) > Input.mousePosition.y - (Display.main.systemHeight / 2)  - ((this.transform.position.y / camerasizey)* (Display.main.systemHeight/2))) {
			direction = "down";
			Debug.Log ("down");
			Animation.SetBool ("down", true);
			Animation.SetBool ("right", false);
			Animation.SetBool ("left", false);
			Animation.SetBool ("up", false);

		}


	}
}