﻿/*Created: Sprint 1 - Last Edited Sprint 1
Purpose: This script manages to movement of the camera to always move with the character to be on screen no matter what. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	public int locX;
	public int locY;
	public GameObject Player;
	public PlayerMovement playerMovement;
	// Use this for initialization
	void Start () {
		locX = 0;
		locY = 0;
		Debug.Log (locX);
		//Debug.Log ("E");
		//Debug.Log (playerMovement.camerasizex);
	}

	// Update is called once per frame
	void Update () {
		/* Comment: This section allows the camera to move with the player by comparing their locations. */
		//Debug.Log (Player.transform.position.y + " " + ((locY) * 2 * playerMovement.camerasizey + playerMovement.camerasizey) + " " + ((locY) * 2 * playerMovement.camerasizey - playerMovement.camerasizey));
		if (Player.transform.position.y > ((locY) * 2 * playerMovement.camerasizey + playerMovement.camerasizey)) {
			locY += 1 ;
			this.gameObject.transform.SetPositionAndRotation (new Vector3 (((locX) * 2 * playerMovement.camerasizex), ((locY) * 2 * playerMovement.camerasizey), -10), new Quaternion (0, 0, 0, 0));
		}
		if (Player.transform.position.y < ((locY) * 2 * playerMovement.camerasizey - playerMovement.camerasizey)) {
			locY -= 1 ;
			this.gameObject.transform.SetPositionAndRotation (new Vector3 (((locX) * 2 * playerMovement.camerasizex), ((locY) * 2 * playerMovement.camerasizey), -10), new Quaternion (0, 0, 0, 0));
		}
		if (Player.transform.position.x > ((locX) * 2 * playerMovement.camerasizex + playerMovement.camerasizex)) {
			locX += 1 ;
			this.gameObject.transform.SetPositionAndRotation (new Vector3 (((locX) * 2 * playerMovement.camerasizex), ((locY) * 2 * playerMovement.camerasizey), -10), new Quaternion (0, 0, 0, 0));
		}
		if (Player.transform.position.x < ((locX) * 2 * playerMovement.camerasizex - playerMovement.camerasizex)) {
			locX -= 1;
			this.gameObject.transform.SetPositionAndRotation (new Vector3 (((locX) * 2 * playerMovement.camerasizex), ((locY) * 2 * playerMovement.camerasizey), -10), new Quaternion (0, 0, 0, 0));
		}

	}
}
