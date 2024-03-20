/*Created: Sprint 5 - Last Edited Sprint 7
This script’s purpose is to deal damage to the player upon them standing on it too long. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
	int state;
	bool activate;
	Animator Animation;
	public PlayerMovement Player;
	bool playerIN;
	bool hit;

	// Initialization
	void Start () {
		state = 0;
		activate = false;
		hit = false;
		Animation = GetComponent<Animator>();
		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement>();
		playerIN = false;
	}
	
	// Update once per frame
	void Update () {
		// initiate trap activation
		if (activate == true) {
			state += 1;
			if (state >= 80 & state <=110 & hit == false & playerIN == true) {
				Player.SendMessage ("Damaged", 0.2f * Player.maxhp);
				hit = true;
			}
		}
		// damage player after a certain delay
		if (state >= 150) {
			activate = false;
			hit = false;
			state = 0;
			Animation.SetBool ("Active", false);
		}
	}

	// Check when the player is in the trigger & activate the trap
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player"){
			playerIN = true;
			}
		if (other.gameObject.tag == "Player" & activate == false) {
			activate = true;
			state = 0;
			Animation.SetBool ("Active", true);
		}
	}

	// Check if the player has left to not deal damage to them
	private void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			playerIN = false;
		}
	}
			
}
