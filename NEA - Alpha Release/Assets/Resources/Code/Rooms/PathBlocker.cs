/*Created: Sprint 3 - Last Edited Sprint 6
This script’s purpose is to prevent the player from moving offscreen until all enemies are defeated. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlocker : MonoBehaviour {
	private bool loaded = false;
	public StatsStorage stats;
	public CameraMovement camMov;
	string location;

	// Initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		camMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement> ();
		location = stats.Locations[stats.Locations.Count-1];
	}
	
	// Update once per frame
	void Update () {
		// Checks the current position matches the room this is supposed to be in and deletes if they dont match
		if (location != (camMov.locX + "." + camMov.locY)) {
			Destroy (this.gameObject);
		}
		// Checks if the room is fully loaded to prevent being deleted before enemies load
		if (loaded == false & GameObject.Find ("Enemies").transform.childCount != 0) {
			loaded = true;
		}
		// deletes the object if there are no more enemies
		if (GameObject.Find ("Enemies").transform.childCount == 0 & loaded == true) {
			Destroy (this.gameObject);
		}
	}

}
