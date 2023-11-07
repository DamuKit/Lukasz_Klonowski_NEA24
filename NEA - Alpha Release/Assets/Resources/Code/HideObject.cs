using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour {
	int hidden;
	Vector3 location;
	// Use this for initialization
	void Start () {
		hidden = 0;
		location = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.SetPositionAndRotation (location + new Vector3(0,1000,0) * hidden, Quaternion.identity);
		if (Input.GetKeyDown (KeyCode.Tab) == true && hidden == 0) {
			hidden = 1;
		}
		else if (Input.GetKeyDown (KeyCode.Tab) == true && hidden == 1) {
			hidden = 0;

		}
	}
}
