using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlocker : MonoBehaviour {
	private bool loaded = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (loaded == false & GameObject.Find ("Enemies").transform.childCount != 0) {
			loaded = true;
		}
		if (GameObject.Find ("Enemies").transform.childCount == 0 & loaded == true) {
			Debug.Log ("destroyed barrier");
			Destroy (this.gameObject);
		}
	}
}
