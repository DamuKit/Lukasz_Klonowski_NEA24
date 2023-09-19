using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	float angle;
	float speed;
	Animator Animation;
	public CameraMovement camMov;
	string direction;
	bool moving;
	bool dashing;
	public float camerasizex;
	public float camerasizey;
	// Use this for initialization
	void Start () {
		speed = 0.05f;
		direction = "down";
		moving = false;
		dashing = false;
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
		if (Input.GetKeyDown (KeyCode.LeftShift) == true) {
			dashing = true;
			Animation.SetBool ("dash", true);
			Animation.Play ("Dash");
		}

		if(dashing = true){
			moving = true;
		}


		if(Input.mousePosition.x-(Display.main.systemWidth/2)>=0 & Input.mousePosition.y-(Display.main.systemHeight/2)>= 0)
		{
			//Debug.Log ("e");
		}

		Angle();
		Debug.Log (angle);
		if (angle >= 45 && angle <= 135) {
			//Debug.Log((this.transform.position.x / camerasizex)* (Display.main.systemWidth/2));
			//Debug.Log (Input.mousePosition.x - (Display.main.systemWidth / 2));
			direction = "right";
			//Debug.Log ("Right");
			Animation.SetBool ("right", true);
			Animation.SetBool ("left", false);
			Animation.SetBool ("up", false);
			Animation.SetBool ("down", false);

		} else if (angle >= 225 && angle <= 315) {
			direction = "left";
			//Debug.Log ("Left");
			Animation.SetBool ("left", true);
			Animation.SetBool ("right", false);
			Animation.SetBool ("up", false);
			Animation.SetBool ("down", false);

		} else if (angle >= 315 | angle <= 45) {
			direction = "up";
			//Debug.Log ("up");
			Animation.SetBool ("up", true);
			Animation.SetBool ("right", false);
			Animation.SetBool ("left", false);
			Animation.SetBool ("down", false);

		} else if (angle >= 135 && angle <= 225) {
			direction = "down";
			//Debug.Log ("down");
			Animation.SetBool ("down", true);
			Animation.SetBool ("right", false);
			Animation.SetBool ("left", false);
			Animation.SetBool ("up", false);

		}
		//-(camMov.locX * camerasizex)
		//-(camMov.locY * camerasizey) (((this.transform.position.y -(camMov.locY * camerasizey))/ camerasizey)



	}
	public void Angle () {
		if (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2)) >= 0 & 0 <= Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2))) {
			angle = (Mathf.Atan (Mathf.Abs (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2))) / (Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2))))) * Mathf.Rad2Deg);
		}
		else if (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2)) >= 0 & 0 >= Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2))) {
			angle = (90 + Mathf.Atan ((Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2)))) / Mathf.Abs (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2)))) * Mathf.Rad2Deg);
		}
		else if (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2)) <= 0 & 0 >= Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2))) {
			angle = (180 + Mathf.Atan (Mathf.Abs (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2))) / (Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2))))) * Mathf.Rad2Deg);
		}
		else if (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2)) <= 0 & 0 <= Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2))) {
			angle = (270 + Mathf.Atan ((Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2)))) / Mathf.Abs (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2)))) * Mathf.Rad2Deg);
		}
	}
}