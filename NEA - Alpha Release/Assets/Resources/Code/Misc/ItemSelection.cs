﻿/*Created: Sprint - Last Edited Sprint 
This script’s purpose is to change the position of an object to show the slot being held in the player’s hand. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelection : MonoBehaviour {
	InventoryBehaviour Invbeh;
	// Use this for initialization
	void Start () {
		Invbeh = GameObject.Find ("Inventory").GetComponent<InventoryBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.name.Substring (0, 1) == "L") {
			this.gameObject.transform.localPosition = new Vector3 (-6.5f + (Invbeh.MainHandPosition * 1f), 7.5f, 10f);
		} else{
			this.gameObject.transform.localPosition = new Vector3(-6.5f + (Invbeh.OffHandPosition * 1f), 7.5f, 10f);
		}

	}
}
