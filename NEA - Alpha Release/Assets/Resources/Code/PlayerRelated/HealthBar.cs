/*Created: Sprint 2 - Last Edited Sprint 2
Purpose: This script manages the hp bar to reflect the player's health. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	public PlayerMovement Player;
	Animator Animation;
	// Use this for initialization
	void Start () {
		Animation = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Animation.SetFloat ("Hp%", Player.hp);

		if (Player.hp <= 0 & Time.timeScale > 0) {
			Time.timeScale = Time.timeScale * 0.99f;
			Debug.Log (Time.timeScale);
			if (Time.timeScale < 0.00000001) {
				Time.timeScale = 0;
			}
		} else if (Time.timeScale < 1) {
			Time.timeScale = Time.timeScale * 1.1f;
		} else if (Time.timeScale > 1) {
			Time.timeScale = 1;
		}
	}
}