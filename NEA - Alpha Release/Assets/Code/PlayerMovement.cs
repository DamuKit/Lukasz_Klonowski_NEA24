using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	float speed;
	Animator Animation;
	string direction;
	// Use this for initialization
	void Start () {
		speed = 0.05f;
		direction = "down";
		Animation = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.A) == true) {
			this.transform.Translate (-speed, 0, 0);
			direction = "left";
			Animation.SetBool ("left", true);
			Animation.SetBool ("right", false);
		} else if (Input.GetKey (KeyCode.D) == true) {
			this.transform.Translate (speed, 0, 0);
			direction = "right";
			Animation.SetBool ("right", true);
			Animation.SetBool ("left", false);
		} else {
			Animation.SetBool ("right", false);
			Animation.SetBool ("left", false);
		}
			
		if (Input.GetKey (KeyCode.W) == true) {
			this.transform.Translate (0, speed, 0);
			direction = "up";
			Animation.SetBool ("up", true);
			Animation.SetBool ("down", false);
		} else if (Input.GetKey (KeyCode.S) == true) {
			this.transform.Translate (0, -speed, 0);
			direction = "down";
			Animation.SetBool ("down", true);
			Animation.SetBool ("up", false);
		} else {
			Animation.SetBool ("down", false);
			Animation.SetBool ("up", false);
		}
		if(Input.mousePosition.x + 960>=0 & Input.mousePosition.y + 540 >= 0)
		{
			Debug.Log ("x");
			Debug.Log (Input.mousePosition.x - 1920 / 2);
			Debug.Log ("y");
			Debug.Log (Input.mousePosition.y - 1080 / 2);
		}
	}
}
