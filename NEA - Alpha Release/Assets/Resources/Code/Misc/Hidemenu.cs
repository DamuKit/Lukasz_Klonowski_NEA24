/*Created: Sprint - Last Edited Sprint 
This script’s purpose is to hide the menu when necessary. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidemenu : MonoBehaviour {
	int hidden;
	Vector3 location;
	public CameraMovement camMov;
	public StatsStorage stats;
	// Use this for initialization
	void Start () {
		camMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement> ();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		hidden = 0;
		location = this.gameObject.transform.position;
	}

	// Update is called once per frame
	void Update () {
		this.gameObject.transform.SetPositionAndRotation (new Vector2(location.x + camMov.locX * 24, location.y + camMov.locY * 16 + 2000 * stats.menu) , Quaternion.identity);
	}
}
