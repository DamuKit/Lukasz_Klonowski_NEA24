/*Created: Sprint 7 - Last Edited Sprint 8
This script’s purpose is to allow the object to take damage from items and get destroyed. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleGrave : MonoBehaviour {
	PlayerMovement Player;
	int health;
	bool IV;
	public Attacking attack;
	public bool end;
	public EnemyHealth HPBar;

	// Initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
		HPBar = gameObject.transform.Find("EnemyHP").GetComponent<EnemyHealth>();
		attack = GameObject.Find ("AttackHitBox").GetComponent<Attacking> ();
		IV = false;
		health = 20;
		end = false;
	}
	
	// Manage Invincibility & destroy when zero health
	void Update () {
		if (IV == true & attack.Attack == false) {
			IV = false;
			gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		if (health <= 0) {
			Player.m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/explosion"));
			Destroy (this.gameObject);
		}
		HPBar.SendMessage ("HealthReport", health);
	}

	// Process damage from the player
	void damaged(int dmg) {
		if (IV == false) {
			Player.m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/MeleeAttack"));
			gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
			health -= dmg;
			IV = true;
		}
	}
}
