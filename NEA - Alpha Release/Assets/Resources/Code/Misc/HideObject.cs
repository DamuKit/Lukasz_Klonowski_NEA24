/*Created: Sprint 5 - Last Edited Sprint 7
This script’s purpose is to hide certain objects when unpaused. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour {
	int hidden;
	Vector3 location;
	int MoveWithCamera = 0;
	public CameraMovement camMov;
	public StatsStorage stats;

	// Initialization
	void Start () {
		camMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement> ();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		hidden = 0;
		location = this.gameObject.transform.position;
		if (this.name == "Inventory") {
			MoveWithCamera = 1;
		}
	}
	
	// Update once per frame
	void Update () {
		this.gameObject.transform.SetPositionAndRotation (new Vector2(location.x + camMov.locX * 24 * MoveWithCamera, location.y + camMov.locY * 16 * MoveWithCamera + 1000 * stats.pause) , Quaternion.identity);
	}
}
