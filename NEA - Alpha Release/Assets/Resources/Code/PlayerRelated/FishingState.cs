/*Created: Sprint 8 - Last Edited Sprint 8
This script’s purpose is to reveal certain information when fishing to the player. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingState : MonoBehaviour {
	Animator Animation;
	int state;
	public float Life;

	// Initialization
	void Start () {
		Animation = this.gameObject.GetComponent<Animator> ();
		state = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ().FishingState;
		Animation.SetInteger ("State", state);
		Life = 3;
	}
	
	// Update once per frame
	void Update () {
		// Determine appearance based on the state of catching the fish & disappear over a short time
		Life -= 1 * Time.deltaTime;
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, Mathf.Pow(Life / 3,5));
		if (Life <= 0) {
			Destroy (this.gameObject);
		}
	}
}
