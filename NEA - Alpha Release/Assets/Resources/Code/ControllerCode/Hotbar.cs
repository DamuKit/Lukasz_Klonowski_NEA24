/*Created: Sprint - Last Edited Sprint 
This script’s purpose is to make the hotbar visible even if the parent object is hidden. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour {
	public StatsStorage stats;
	bool hidden;
	// Use this for initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		hidden = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (stats.pause == 0 & hidden == false) {
			this.gameObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + 1000);
			hidden = true;
		} else if (stats.pause == 1 & hidden == true) {
			this.gameObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y - 1000);
			hidden = false;
		}
	}
}
