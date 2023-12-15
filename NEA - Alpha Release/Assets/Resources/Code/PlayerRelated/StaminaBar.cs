using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour {
	float stamina;
	PlayerMovement player;
	Animator animation;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		animation = this.gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		stamina =  Mathf.RoundToInt((player.stamina * 1f / (player.maxStamina) * 1f ) * 100);
		animation.SetFloat ("stamina%", stamina);
	}
}
