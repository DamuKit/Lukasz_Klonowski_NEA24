/*Created: Sprint - Last Edited Sprint 
This script’s purpose is to reveal certain information when fishing to the player. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingState : MonoBehaviour {
	Animator Animation;
	int state;
	public float Life;
	// Use this for initialization
	void Start () {
		Animation = this.gameObject.GetComponent<Animator> ();
		state = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ().FishingState;
		Animation.SetInteger ("State", state);
		Life = 3;
	}
	
	// Update is called once per frame
	void Update () {
		Life -= 1 * Time.deltaTime;
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, Mathf.Pow(Life / 3,5));
		if (Life <= 0) {
			Destroy (this.gameObject);
		}
	}
}
