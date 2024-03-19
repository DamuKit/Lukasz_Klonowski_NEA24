/*Created: Sprint 4 - Last Edited Sprint 8
This script’s purpose is to manage ground items, shortening their names and giving them to the player when touched. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
	public StatsStorage stats;
	public PlayerMovement Player;
	public InventoryBehaviour inventory;

	// initialization
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
	}

	// Runs when touched by the player
	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			Player.m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/pickupCoin"));
			// Gives the player an item based on what this is attached to
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
		}
	}
}
