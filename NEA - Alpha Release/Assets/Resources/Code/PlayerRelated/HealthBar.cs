/*Created: Sprint 3 - Last Edited Sprint 8
This script’s purpose is to display the health of the player in clear view. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour {
	public StatsStorage stats;
	public PlayerMovement Player;
	Animator Animation;

	// Initialization
	void Start () {
		Animation = GetComponent<Animator>();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
	}
	
	// Update once per frame
	void Update () {
		// Change the animation based on the player's health
		Animation.SetFloat ("Hp%", Mathf.RoundToInt((Player.hp * 1f / (Player.maxhp) * 1f ) * 100));
		if (Player.hp <= 0 & Time.timeScale > 0) {
			// Slow down game speed over time
			stats.gameSpeed = stats.gameSpeed * 0.99f;
			if (stats.gameSpeed < 0.00000001) {
				stats.gameSpeed = 0;
			}
			StartCoroutine ("Dead");
		} else if (stats.gameSpeed < 1) {
			stats.gameSpeed = stats.gameSpeed * 1.1f;
		} else if (stats.gameSpeed > 1) {
			stats.gameSpeed = 1;
		}
	}

	// Load death scene after death lasts longer than two seconds
	public IEnumerator Dead(){
		yield return new WaitForSeconds (2f);
		if (Player.hp <= 0) {
			SceneManager.LoadScene ("text");
		}
	}
}