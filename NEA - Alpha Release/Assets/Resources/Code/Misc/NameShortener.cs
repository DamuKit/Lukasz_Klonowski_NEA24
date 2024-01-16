using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameShortener : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.name = (this.gameObject.name.Substring (0, 4));
	}
}
