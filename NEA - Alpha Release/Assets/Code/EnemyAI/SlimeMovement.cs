﻿/*Created: Sprint 2 - Last Edited Sprint 3
Purpose: This script manages the movement of the basic green slime enemy. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour {
	bool onscreen = false;
	public CameraMovement camMov;
	public PlayerMovement Player;
	string location;
	public GameObject player;
	public GameObject Cam;
	float angle;
	float speed;
	int delay;
	// Use this for initialization
	void Start () {
		Cam = GameObject.FindGameObjectWithTag("MainCamera");
		camMov = Cam.GetComponent<CameraMovement> ();
		location = (camMov.locX + "." + camMov.locY);
		//Debug.Log (location);
		speed = 0.03f;
		player = GameObject.FindGameObjectWithTag ("Player");
		Player = player.GetComponent<PlayerMovement> ();
		delay = 0;

	}
	/* This code adds motion to the slime enemy if they originally spawned on the same screen as the player is currently on. */
	// Update is called once per frame
	void Update () {
			
		if (location == (camMov.locX + "." + camMov.locY)) {
			
			if (delay == 0) {
				RaycastHit2D DetectPlayer = Physics2D.Raycast (this.gameObject.transform.position - new Vector3(0, 0.1f), (player.transform.position - transform.position - new Vector3(0, 0.1f))*2);
				//Debug.DrawRay (transform.position, (player.transform.position - transform.position), Color.white, 10); 
				//Debug.Log (DetectPlayer.collider.name);
				if (DetectPlayer.collider.name == "Player") {
				
					//Debug.Log ("nearby");
					//Debug.Log (camMov.locX + "." + camMov.locY);
					//Debug.Log (location);;
					/* Calculates angle from enemy to player */
					angle = Mathf.Rad2Deg * (Mathf.Atan (Mathf.Abs ((0.5f + 0.5f * Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y)) * (player.transform.position.x - this.transform.position.x) / (player.transform.position.y - this.transform.position.y) + ((0.5f + 0.5f * -Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y))) * (player.transform.position.y - this.transform.position.y) / (player.transform.position.x - this.transform.position.x)))) + 45 * (2 - 2 * Mathf.Sign (player.transform.position.x - this.transform.position.x) + 1 - Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y));
					if (angle >= 337.5 | angle <= 22.5) {
						this.transform.Translate (0, speed, 0);
					} else if (angle >= 22.5 && angle <= 67.5) {
						this.transform.Translate (0.5f * speed, 0.5f * speed, 0);
					} else if (angle >= 67.5 && angle <= 112.5) {
						this.transform.Translate (speed, 0, 0);
					} else if (angle >= 112.5 && angle <= 157.5) {
						this.transform.Translate (0.5f * speed, -0.5f * speed, 0);
					} else if (angle >= 157.5 && angle <= 202.5) {
						this.transform.Translate (0, -speed, 0);
					} else if (angle >= 202.5 && angle <= 247.5) {
						this.transform.Translate (-0.5f * speed, -0.5f * speed, 0);
					} else if (angle >= 247.5 && angle <= 292.5) {
						this.transform.Translate (-speed, 0, 0);
					} else if (angle >= 292.5 && angle <= 337.5) {
						this.transform.Translate (-0.5f * speed, 0.5f * speed, 0);
					}
				} else {
					delay = 100;
				}
			} else {
				delay -= 1;
			}
				
		}
	}
	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			Player.hp -= 10;
			//Destroy (this.gameObject);
		}
	}
}