/*Created: Sprint 2 - Last Edited Sprint 2
Purpose: This script manages the hp bar to reflect the player's health. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour {
	public StatsStorage stats;
	public PlayerMovement Player;
	Animator Animation;
	// Use this for initialization
	void Start () {
		Animation = GetComponent<Animator>();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
	}
	
	// Update is called once per frame
	void Update () {
		Animation.SetFloat ("Hp%", Mathf.RoundToInt((Player.hp * 1f / (Player.maxhp) * 1f ) * 100));

		if (Player.hp <= 0 & Time.timeScale > 0) {
			stats.gameSpeed = stats.gameSpeed * 0.99f;
			Debug.Log (stats.gameSpeed);
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

	public IEnumerator Dead(){
		yield return new WaitForSeconds (2f);
		if (Player.hp <= 0) {
			SceneManager.LoadScene ("text");
		}
	}
}