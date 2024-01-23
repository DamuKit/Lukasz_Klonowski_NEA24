/*Created: Sprint - Last Edited Sprint 
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
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
		HPBar = gameObject.transform.Find("EnemyHP").GetComponent<EnemyHealth>();
		attack = GameObject.Find ("AttackHitBox").GetComponent<Attacking> ();
		IV = false;
		health = 20;
		end = false;
	}
	
	// Update is called once per frame
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
	void damaged(int dmg) {
		if (IV == false) {
			Player.m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/MeleeAttack"));
			Debug.Log ("damaged");

			gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
			health -= dmg;
			Debug.Log (health);
			IV = true;
		}
	}
}
