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

	}
}
