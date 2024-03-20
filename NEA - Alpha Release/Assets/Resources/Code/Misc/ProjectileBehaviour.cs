/*Created: Sprint 7 - Last Edited Sprint 7
This script’s purpose is to manage the behaviour of projectiles and cause them to damage enemies. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {
	
	public float Damage;
	public float direction;
	public float speed;
	public float accelleration;
	public float bounces;
	public float pierce;
	Attacking properties;
	PlayerMovement Player;

	// Initialization
	void Start () {
		properties = GameObject.Find ("AttackHitBox").GetComponent<Attacking> ();
		Player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		Damage = properties.ProjectileStats [0];
		direction = Player.angle + Random.Range(properties.ProjectileStats[1] * -1, properties.ProjectileStats[1]);
		speed = properties.ProjectileStats[2];
		accelleration = properties.ProjectileStats[3];
		bounces = properties.ProjectileStats[4];
		pierce = properties.ProjectileStats[5];
		this.gameObject.transform.Rotate (0, 0, -direction);
	}
	
	// Update once per frame
	void Update ()
	{
		this.gameObject.transform.position = this.gameObject.transform.position + this.gameObject.transform.up * speed * Time.deltaTime;
		speed = speed + (accelleration * Time.deltaTime);
		if (speed <= 0) {
			Destroy (this.gameObject);
		}
	}

	// Runs when touching objects other than the player
	private void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.name.Substring(0,1)!= "P" && other.gameObject.tag != "PlayerPart" && other.gameObject.name.Substring(0,1) != "I"){
			if (bounces >  0) {
				// Reverses the direction
				direction = (direction + 180);
				if (direction > 360) {
					direction -= 360;
				}
				this.gameObject.transform.rotation = Quaternion.identity;
				this.gameObject.transform.Rotate(0, 0, -direction);

				this.gameObject.transform.position = this.gameObject.transform.position + this.gameObject.transform.up * speed * Time.deltaTime;
					bounces--;
			}
			else {
				Destroy (this.gameObject);
			}
		}
	}

	// Runs on initial contact with enemies
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.SendMessage ("damaged", (Damage + properties.damage) * (1 + properties.damagebuff * 0.25f));
			if (pierce > 0) {
				pierce--;
			} else if (bounces > 0) {
				// Reverses the direction
				direction = (direction + 180);
				if (direction > 360) {
					direction -= 360;
				}
				this.gameObject.transform.rotation = Quaternion.identity;
				this.gameObject.transform.Rotate (0, 0, -direction);
				this.gameObject.transform.position = this.gameObject.transform.position + this.gameObject.transform.up * speed * Time.deltaTime;
				bounces--;
			} else {
				Destroy (this.gameObject);
			}
		}
	}
}