using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	float angle;
	float dashDirection;
	float speed;
	public float duration;
	float durationLim;
	Animator Animation;
	public CameraMovement camMov;
	string direction;
	bool moving;
	public bool dashing;
	public float camerasizex;
	public float camerasizey;
	// Use this for initialization
	void Start () {
		speed = 0.05f;
		dashDirection = 0f;
		durationLim = 15;
		duration = durationLim;
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

		Angle();
		Debug.Log (angle);

		Animation.SetBool ("walk", false);
		if (Input.GetKey (KeyCode.A) == true && dashing == false) {
			this.transform.Translate (-speed, 0, 0);
			moving = true;
			Animation.SetBool ("walk", true);
		} else if (Input.GetKey (KeyCode.D) == true  && dashing == false) {
			this.transform.Translate (speed, 0, 0);
			moving = true;
			Animation.SetBool ("walk", true);
		}

		if (Input.GetKey (KeyCode.W) == true  && dashing == false) {
			this.transform.Translate (0, speed, 0);
			moving = true;
			Animation.SetBool ("walk", true);
		} else if (Input.GetKey (KeyCode.S) == true  && dashing == false) {
			this.transform.Translate (0, -speed, 0);
			moving = true;
			Animation.SetBool ("walk", true);
		}
		if (Input.GetKeyDown (KeyCode.LeftShift) == true  && dashing == false) {
			dashing = true;
			Animation.SetBool ("dash", true);
			Animation.Play ("Dash");
		}

		if (dashing == true && duration > 0) {
			moving = true;
			if (duration == durationLim) {
				dashDirection = angle;
			}
			duration -= 1;
			if(Input.GetKey(KeyCode.LeftShift) == true)
			{
				duration += 0.3f;
			}
			if (dashDirection >= 337.5 | dashDirection <= 22.5) {
				this.transform.Translate (0, 3 * speed, 0);
			} else if (dashDirection >= 22.5 && dashDirection <= 67.5) {
				this.transform.Translate (1.5f * speed, 1.5f * speed, 0);
			} else if (dashDirection >= 67.5 && dashDirection <= 112.5) {
				this.transform.Translate (3 * speed, 0, 0);
			} else if (dashDirection >= 112.5 && dashDirection <= 157.5) {
				this.transform.Translate (1.5f * speed, -1.5f * speed, 0);
			} else if (dashDirection >= 157.5 && dashDirection <= 202.5) {
				this.transform.Translate (0, -3 * speed, 0);
			} else if (dashDirection >= 202.5 && dashDirection <= 247.5) {
				this.transform.Translate (-1.5f * speed, -1.5f * speed, 0);
			} else if (dashDirection >= 247.5 && dashDirection <= 292.5) {
				this.transform.Translate (-3 * speed, 0, 0);
			} else if (dashDirection >= 292.5 && dashDirection <= 337.5) {
				this.transform.Translate (-1.5f * speed, 1.5f * speed, 0);
			}

		} else {
			duration = durationLim;
			Animation.SetBool ("dash", false);
			dashing = false;
		}


		if(Input.mousePosition.x-(Display.main.systemWidth/2)>=0 & Input.mousePosition.y-(Display.main.systemHeight/2)>= 0)
		{
			//Debug.Log ("e");
		}

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