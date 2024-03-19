/*Created: Sprint 7 - Last Edited Sprint 7
This script’s purpose is to allow the pet to interact between the movement script and the pet interaction script. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetInteraction : MonoBehaviour {
	public float focusDistance;
	public float angle;
	// Use this for initialization
	void Start () {
		angle = -1;
	}

	// Update is called once per frame
	void Update () {
		focusDistance = 1000;
		angle = -1;
	}

	public void Focus(float[] Info) {
		if(focusDistance > Info[0]){
			focusDistance = Info[0];
			angle = Info[1];
		}
	}
}
