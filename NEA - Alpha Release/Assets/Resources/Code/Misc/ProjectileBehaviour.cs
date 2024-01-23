/*Created: Sprint - Last Edited Sprint 
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
	public bool piercing;
	Attacking properties;
	PlayerMovement Player;

	// Use this for initialization
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
		piercing = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.gameObject.transform.position = this.gameObject.transform.position + this.gameObject.transform.up * speed * Time.deltaTime;
		speed = speed + (accelleration * Time.deltaTime);
		if (speed <= 0) {
			Destroy (this.gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D other) {
		Debug.Log (other.gameObject.name);
		if(other.gameObject.name.Substring(0,1)!= "P" && other.gameObject.tag != "PlayerPart" && other.gameObject.name.Substring(0,1) != "I"){
			
			if (bounces >  0) {
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
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.SendMessage ("damaged", (Damage + properties.damage) * (1 + properties.damagebuff * 0.25f));
			if (pierce > 0) {
				//if (piercing == false) {
				pierce--;
				//piercing = true;
				//}
			} else if (bounces > 0) {
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
	private void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			piercing = false;
		}
	}
}