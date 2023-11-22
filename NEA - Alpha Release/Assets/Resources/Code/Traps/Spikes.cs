﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
	int state;
	bool activate;
	Animator Animation;
	public PlayerMovement Player;
	bool playerIN;
	bool hit;

	// Use this for initialization
	void Start () {
		state = 0;
		activate = false;
		hit = false;
		Animation = GetComponent<Animator>();
		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement>();
		playerIN = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (activate == true) {
			state += 1;
			if (state >= 80 & state <=110 & hit == false & playerIN == true) {
				Player.hp -= 0.2f * Player.maxhp;
				hit = true;
				Debug.Log(state);
			}
		}
		if (state >= 150) {
			activate = false;
			hit = false;
			state = 0;
			Animation.SetBool ("Active", false);
		}
	}

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
	private void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			playerIN = false;
		}
	}
			
}
