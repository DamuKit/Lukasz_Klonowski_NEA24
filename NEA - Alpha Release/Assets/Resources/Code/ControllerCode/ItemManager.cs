﻿/*Created: Sprint - Last Edited Sprint 
This script’s purpose is to manage ground items, shortening their names and giving them to the player when touched. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
	
	public StatsStorage stats;
	public PlayerMovement Player;
	public InventoryBehaviour inventory;
	// Use this for initialization
	void Start () {
		
		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		inventory = GameObject.Find ("Inventory").GetComponent<InventoryBehaviour> ();
		switch (this.gameObject.name.Substring (1, 1)) {
		case("0"):
			this.gameObject.name = (this.gameObject.name.Substring (0, 4));
			break;
		case("1"):
			this.gameObject.name = (this.gameObject.name.Substring (0, 8));
			break;
		default:
			break;
		}

		//stats.Items [int.Parse (this.gameObject.name, 1)];
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			/*other.gameObject.SendMessage ("itemEffect", int.Parse(this.gameObject.name.Substring(1)));
			*/
			//Player.Items [int.Parse (this.gameObject.name.Substring (1))] +=1;
			//inventory.Locations [inventory.Locations.FindIndex (a => a == "")] = this.gameObject.name.Substring (1) + "001";
			//if (inventory.Locations.FindIndex (a => a != "") >= 6){
			Player.m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/pickupCoin"));
				switch (this.gameObject.name.Substring (1, 1)) {
				case("0"):
					inventory.items.Enqueue (this.gameObject.name.Substring (1,3) + "001");
					break;
				case("1"):
					inventory.items.Enqueue (this.gameObject.name.Substring (1,3) + "N" + this.gameObject.name.Substring (4,4));
					break;
				default:
					break;
				}

			Destroy (this.gameObject);
		//}
		}
	}
}
