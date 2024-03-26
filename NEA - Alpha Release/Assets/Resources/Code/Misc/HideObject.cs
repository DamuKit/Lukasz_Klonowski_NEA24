﻿/*Created: Sprint 5 - Last Edited Sprint 7
This script’s purpose is to hide certain objects when unpaused. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour {
	Vector3 location;
	int MoveWithCamera = 0;
	public CameraMovement camMov;
	public StatsStorage stats;

	// Initialization
	void Start () {
		camMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement> ();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		location = this.gameObject.transform.position;
		if (this.name == "Inventory") {
			MoveWithCamera = 1;
		}
	}
	
	// Moves the attached object offscreen & onscreen to specific positions based on if the game is paused every frame
	void Update () {
		this.gameObject.transform.SetPositionAndRotation (new Vector2(location.x + camMov.locX * 24 * MoveWithCamera, location.y + camMov.locY * 16 * MoveWithCamera + 1000 * stats.pause) , Quaternion.identity);
	}
}
