/*Created: Sprint 7 - Last Edited Sprint 7
This script’s purpose is to hide the menu when necessary. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidemenu : MonoBehaviour {
	Vector3 location;
	public CameraMovement camMov;
	public StatsStorage stats;

	// initialization
	void Start () {
		camMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement> ();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		location = this.gameObject.transform.position;
	}
	// Moves the attached object offscreen & onscreen to specific positions based on the state of the menu every frame
	void Update () {
		this.gameObject.transform.SetPositionAndRotation (new Vector2(location.x + camMov.locX * 24, location.y + camMov.locY * 16 + 2000 * stats.menu) , Quaternion.identity);
	}
}
