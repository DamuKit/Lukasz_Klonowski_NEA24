using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	public StatsStorage stats;
	int hidden;
	Vector3 location;
	// Use this for initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		hidden = 0;
		location = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) == true && stats.pause == 0 & stats.menu == 1) {
			stats.pause = 1;
		}
		else if (Input.GetKeyDown (KeyCode.Escape) == true && stats.pause == 1 & stats.menu == 1) {
			stats.pause = 0;

		}

	}
}
