/*Created: Sprint 7 - Last Edited Sprint 7
This script’s purpose is to trigger certain objects and UI elements to hide when unpausing. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	public StatsStorage stats;
	int hidden;
	Vector3 location;
	// Initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		hidden = 0;
		location = this.gameObject.transform.position;
	}
	
	// Update once per frame
	void Update () {
		// Check when escape is pressed & change the state of the menu
		if (Input.GetKeyDown (KeyCode.Escape) == true && stats.pause == 0 & stats.menu == 1) {
			stats.pause = 1;
		}
		else if (Input.GetKeyDown (KeyCode.Escape) == true && stats.pause == 1 & stats.menu == 1) {
			stats.pause = 0;

		}

	}
}
