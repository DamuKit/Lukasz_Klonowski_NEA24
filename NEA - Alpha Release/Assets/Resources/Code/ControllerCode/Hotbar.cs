/*Created: Sprint 7 - Last Edited Sprint 7
This script’s purpose is to make the hotbar visible even if the parent object is hidden. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour {
	public StatsStorage stats;
	bool hidden;

	// initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		hidden = true;
	}
	
	// Update per frame
	void Update () {
		//check if the game is paused or not & hide offscreen if paused
		if (stats.pause == 0 & hidden == false) {
			this.gameObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + 1000);
			hidden = true;
		} else if (stats.pause == 1 & hidden == true) {
			this.gameObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y - 1000);
			hidden = false;
		}
	}
}
