/*Created: Sprint - Last Edited Sprint 
This script’s purpose is to make the “You died” screen have scrolling text. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scrollingText : MonoBehaviour {
	float scroll;
	// Use this for initialization
	void Start () {
		scroll = 1;
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.Translate (0, 5 * Mathf.Log(scroll, 2) * Time.deltaTime, 0);
		scroll += 1000 / scroll;
	}

}
