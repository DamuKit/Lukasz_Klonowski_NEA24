using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {
	
	public float Damage;
	public float direction;
	public float speed;
	public float accelleration;
	public float bounces;
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
		this.gameObject.transform.Rotate (0, 0, -direction);

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

	private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.SendMessage ("damaged", (Damage + properties.damage) * (1 + properties.damagebuff * 0.25f));
			Destroy (this.gameObject);
		} else if(other.gameObject.name.Substring(0,1)!= "P"){
			Destroy (this.gameObject);
			if (bounces >= 0) {
				
			}

		}

	}
}