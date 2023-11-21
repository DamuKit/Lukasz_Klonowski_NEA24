using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlocker : MonoBehaviour {
	private bool loaded = false;
	public StatsStorage stats;
	public CameraMovement camMov;
	string location;
	// Use this for initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		camMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement> ();
		location = stats.Locations[stats.Locations.Count-1];
	}
	
	// Update is called once per frame
	void Update () {
		if (location != (camMov.locX + "." + camMov.locY)) {
			Destroy (this.gameObject);
		}
		if (loaded == false & GameObject.Find ("Enemies").transform.childCount != 0) {
			loaded = true;
		}
		if (GameObject.Find ("Enemies").transform.childCount == 0 & loaded == true) {
			Debug.Log ("destroyed barrier");
			Destroy (this.gameObject);
		}
	}

}
