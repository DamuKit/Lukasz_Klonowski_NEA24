/*Created: Sprint 2 - Last Edited Sprint 2
Purpose: This script manages the movement of the basic green slime enemy. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour {
	bool onscreen = false;
	public CameraMovement camMov;
	string location;
	// Use this for initialization
	void Start () {
		location = (camMov.locX + "." + camMov.locY);
		Debug.Log (location);
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
}
