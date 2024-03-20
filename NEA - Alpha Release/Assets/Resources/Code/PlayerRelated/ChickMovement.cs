/*Created: Sprint 7 - Last Edited Sprint 8
This script’s purpose is to manage the movement and damage of the chick pet. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickMovement : MonoBehaviour {
	public float angle;
	public float speed;
	public float baseSpeed;
	public float Damage;
	StatsStorage stats;
	PetInteraction interaction;
	PlayerMovement player;
	public CameraMovement camMov;
	string location;
	bool wonder;
	int delay;

	// Initialization
	void Start () {
		camMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement> ();
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		interaction = this.gameObject.GetComponent<PetInteraction> ();
		baseSpeed = 2.5f;
		angle = -10;
		Damage = 4;
		location = "0.0";
		wonder = false;
		delay = 0;
		Random.InitState (stats.seed + stats.seedoffset);
	}
	
	// Update once per frame
	void Update () {
		speed = baseSpeed * 0.5f * Time.deltaTime * stats.pause;
		if (wonder == true) {
			speed *= 0.5f;
		}
		if (location != (camMov.locX + "." + camMov.locY)) {
			location = (camMov.locX + "." + camMov.locY);
			this.gameObject.transform.position = player.transform.position;
		}
		// Check the direction of the player
		RaycastHit2D DetectPlayer = Physics2D.Raycast (this.gameObject.transform.position - new Vector3(0, 0.25f), (player.transform.position - transform.position - new Vector3(0, 0.1f))*2);
		if (delay > 0) {
			delay--;
			if (delay <= 0) {
				wonder = false;
			}
		}
		// Determine direction of closest enemy
		else if (interaction.focusDistance <= 5) {
			angle = interaction.angle;
		}
		// Determine direction of player
		else if(DetectPlayer.distance <= 10 & DetectPlayer.distance > 1f){
			angle = Mathf.Rad2Deg * (Mathf.Atan (Mathf.Abs ((0.5f + 0.5f * Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y)) * (player.transform.position.x - this.transform.position.x) / (player.transform.position.y - this.transform.position.y) + ((0.5f + 0.5f * -Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y))) * (player.transform.position.y - this.transform.position.y) / (player.transform.position.x - this.transform.position.x)))) + 45 * (2 - 2 * Mathf.Sign (player.transform.position.x - this.transform.position.x) + 1 - Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y));
		}
		// Randomise direction
		else{
			delay = Random.Range(40, 100);
			angle = Random.Range(-100, 360);
			wonder = true;
		}
		// Movement
		if (angle <= 360 & angle >= 337.5 | angle <= 22.5 & angle >= 0) {
			this.transform.Translate (0, speed, 0);
		} else if (angle >= 22.5 && angle <= 67.5) {
			this.transform.Translate (0.5f * speed, 0.5f * speed, 0);
		} else if (angle >= 67.5 && angle <= 112.5) {
			this.transform.Translate (speed, 0, 0);
		} else if (angle >= 112.5 && angle <= 157.5) {
			this.transform.Translate (0.5f * speed, -0.5f * speed, 0);
		} else if (angle >= 157.5 && angle <= 202.5) {
			this.transform.Translate (0, -speed, 0);
		} else if (angle >= 202.5 && angle <= 247.5) {
			this.transform.Translate (-0.5f * speed, -0.5f * speed, 0);
		} else if (angle >= 247.5 && angle <= 292.5) {
			this.transform.Translate (-speed, 0, 0);
		} else if (angle >= 292.5 && angle <= 337.5) {
			this.transform.Translate (-0.5f * speed, 0.5f * speed, 0);
		}
	}

	// Damage enemy upon collision 
	private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.SendMessage ("damaged", (Damage));
		}
	}
}
