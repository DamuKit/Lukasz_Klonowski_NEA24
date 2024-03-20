/*Created: Sprint 5 - Last Edited Sprint 5
This script’s purpose is to make the “You died” screen have scrolling text. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scrollingText : MonoBehaviour {
	float scroll;
	// Initialization
	void Start () {
		scroll = 1;
	}
	
	// Update once per frame
	void Update () {
		// Move the attached gameobject upwards constantly
		this.gameObject.transform.Translate (0, 5 * Mathf.Log(scroll, 2) * Time.deltaTime, 0);
		scroll += 1000 / scroll;
	}

}
