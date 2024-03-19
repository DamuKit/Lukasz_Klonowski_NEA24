/*Created: Sprint 6 - Last Edited Sprint 8
This script’s purpose the collision of the player against certain surfaces */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCollission : MonoBehaviour {
	public PlayerMovement Player;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
	}
	
	// Update is called once per frame

	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Walls" | other.gameObject.name == "T001") {
			Player.lockmovement [int.Parse (this.gameObject.name.Substring (0, 1))] = 0;
		}
	}
	private void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.name == "Walls" | other.gameObject.name == "T001") {
			Player.lockmovement [int.Parse (this.gameObject.name.Substring (0, 1))] = 1;
		}
	}
}
