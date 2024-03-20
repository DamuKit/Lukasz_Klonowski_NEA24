﻿/*Created: Sprint 7 - Last Edited Sprint 7
This script’s purpose is to allow the pet to interact between the movement script and the pet interaction script. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetInteraction : MonoBehaviour {
	public float focusDistance;
	public float angle;

	// Initialization
	void Start () {
		angle = -1;
	}

	// Update once per frame
	void Update () {
		// Reset direction and angle every frame
		focusDistance = 1000;
		angle = -1;
	}

	// 
	public void Focus(float[] Info) {
		// Update info to be based on closest enemy
		if(focusDistance > Info[0]){
			focusDistance = Info[0];
			angle = Info[1];
		}
	}
}
