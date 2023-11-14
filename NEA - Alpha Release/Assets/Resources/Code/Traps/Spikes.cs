using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
	int state;
	bool activate;
	Animator Animation;
	public PlayerMovement Player;
	bool hit;

	// Use this for initialization
	void Start () {
		state = 0;
		activate = false;
		hit = false;
		Animation = GetComponent<Animator>();
		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (activate == true) {
			state += 1;
		}
		if (state >= 150) {
			activate = false;
			hit = false;
			state = 0;
			Animation.SetBool ("Active", false);
		}
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player" & activate == false) {
			activate = true;
			state = 0;
			Animation.SetBool ("Active", true);
		}
		else if (other.gameObject.tag == "Player" &  activate == true & hit == false) {
			if (state >= 80 & state <=110) {
				Player.hp -= 0.2f * Player.maxhp;
				hit = true;
				Debug.Log(state);
			}
		}
	}
}
