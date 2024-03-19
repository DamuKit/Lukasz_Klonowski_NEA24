/*Created: Sprint 5 - Last Edited Sprint 8
This script’s purpose is to shorten the names of certain objects to make them work better with code. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameShortener : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.name = (this.gameObject.name.Substring (0, 4));
	}
}
