/*Created: Sprint 7 - Last Edited Sprint 7
This script’s purpose is to display the stamina of the player in a clear position to view. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour {
	float stamina;
	PlayerMovement player;
	Animator animation;

	// Initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		animation = this.gameObject.GetComponent<Animator> ();
	}
	
	// Update once per frame
	void Update () {
		// Update the stamina animation to reflect on the current stamina
		stamina =  Mathf.RoundToInt((player.stamina * 1f / (player.maxStamina) * 1f ) * 100);
		animation.SetFloat ("stamina%", stamina);
	}
}
