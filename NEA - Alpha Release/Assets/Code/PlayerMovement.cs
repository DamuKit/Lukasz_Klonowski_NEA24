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
		if (Input.GetKey (KeyCode.A) == true) {
			this.transform.Translate (-speed, 0, 0);
			moving = true;
		} else if (Input.GetKey (KeyCode.D) == true) {
			this.transform.Translate (speed, 0, 0);
			moving = true;
		}
			
		if (Input.GetKey (KeyCode.W) == true) {
			this.transform.Translate (0, speed, 0);
			moving = true;
		} else if (Input.GetKey (KeyCode.S) == true) {
			this.transform.Translate (0, -speed, 0);
			moving = true;
		}

		if(Input.mousePosition.x-(Display.main.systemWidth/2)>=0 & Input.mousePosition.y-(Display.main.systemHeight/2)>= 0)
		{
			Debug.Log ("e");
		}
		if (Input.mousePosition.x - (Display.main.systemWidth / 2) - ((this.transform.position.x / camerasizex)* (Display.main.systemWidth/2))  > Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) - ((this.transform.position.y / camerasizey)* (Display.main.systemHeight/2)))) {
			//Debug.Log ("Right" + (Input.mousePosition.x - (Display.main.systemWidth / 2) + this.transform.position.x) + (Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) + this.transform.position.y)));
			//Debug.Log((this.transform.position.x / camerasizex)* (Display.main.systemWidth/2));
			//Debug.Log (Input.mousePosition.x - (Display.main.systemWidth / 2));
			direction = "right";
			if (moving == true) {
				Animation.SetBool ("right", true);
			}
			Animation.SetBool ("left", false);
			Animation.SetBool ("up", false);
			Animation.SetBool ("down", false);

		} else if (Input.mousePosition.x - (Display.main.systemWidth / 2) - ((this.transform.position.x / camerasizex)* (Display.main.systemWidth/2)) < -Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) - ((this.transform.position.y / camerasizey)* (Display.main.systemHeight/2)))) {
			//Debug.Log ("Left" + (Input.mousePosition.x - (Display.main.systemWidth / 2) + this.transform.position.x) + (-Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) + this.transform.position.y)));
			direction = "left";
			if (moving == true) {
				Animation.SetBool ("left", true);
			}
			Animation.SetBool ("right", false);
			Animation.SetBool ("up", false);
			Animation.SetBool ("down", false);

		} else if (Mathf.Abs(Input.mousePosition.x - (Display.main.systemWidth / 2) - ((this.transform.position.x / camerasizex)* (Display.main.systemWidth/2))) < Input.mousePosition.y - (Display.main.systemHeight / 2)  - ((this.transform.position.y / camerasizey)* (Display.main.systemHeight/2))) {
			//Debug.Log ("Up" + (Mathf.Abs(Input.mousePosition.x - (Display.main.systemWidth / 2) + this.transform.position.x)) + (Input.mousePosition.y - (Display.main.systemHeight / 2) + this.transform.position.y));
			direction = "up";
			if (moving == true) {
				Animation.SetBool ("up", true);
			}
			Animation.SetBool ("right", false);
			Animation.SetBool ("left", false);
			Animation.SetBool ("down", false);

		} else if (-Mathf.Abs(Input.mousePosition.x - (Display.main.systemWidth / 2) - ((this.transform.position.x / camerasizex)* (Display.main.systemWidth/2))) > Input.mousePosition.y - (Display.main.systemHeight / 2)  - ((this.transform.position.y / camerasizey)* (Display.main.systemHeight/2))) {
			//Debug.Log ("Down" + (-Mathf.Abs(Input.mousePosition.x - (Display.main.systemWidth / 2) + this.transform.position.x)) + (Input.mousePosition.y - (Display.main.systemHeight / 2) + this.transform.position.y));
			direction = "down";
			if (moving == true) {
				Animation.SetBool ("down", true);
			}
			Animation.SetBool ("right", false);
			Animation.SetBool ("left", false);
			Animation.SetBool ("up", false);

		}
		if (moving == false) {
			Animation.SetBool ("down", false);
			Animation.SetBool ("right", false);
			Animation.SetBool ("left", false);
			Animation.SetBool ("up", false);
		}


	}
}