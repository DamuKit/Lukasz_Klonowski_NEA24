/*Created: Sprint 2 - Last Edited Sprint 4
Purpose: This script manages the movement of the basic green slime enemy. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour {
	bool onscreen = false;
	public StatsStorage stats;
	public Attacking attack;
	bool IV;
	public CameraMovement camMov;
	public PlayerMovement Player;
	public RoomLoader roomLoader;
	string location;
	public GameObject player;
	public GameObject Cam;
	float angle;
	int delay;
	private int sibling;

	float speed;
	int health;
	int damage;
	// Use this for initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		attack = GameObject.Find ("AttackHitBox").GetComponent<Attacking> ();
		IV = false;
		Cam = GameObject.FindGameObjectWithTag("MainCamera");
		roomLoader = GameObject.Find ("PassiveCodeController").GetComponent<RoomLoader> ();
		camMov = Cam.GetComponent<CameraMovement> ();
		location = (camMov.locX + "." + camMov.locY);
		//Debug.Log (location);
		if(this.gameObject.name.Contains("(Clone)")){
			this.gameObject.name = (this.gameObject.name.Substring (0, this.gameObject.name.Length - 7));
			//Debug.Log (this.gameObject.name);
		}
		speed = stats.Enemies[int.Parse(this.gameObject.name.Substring(6)),5] * 0.5f;
		player = GameObject.FindGameObjectWithTag ("Player");
		Player = player.GetComponent<PlayerMovement> ();
		delay = 0;
		health = stats.Enemies [1, 3];
		damage = stats.Enemies [1, 4];

		sibling = transform.GetSiblingIndex () - stats.Rooms [roomLoader.room, 1];
		if (sibling < stats.Rooms [roomLoader.room, 1]) {
			sibling += stats.Rooms [roomLoader.room, 1];
		}

	}
	/* This code adds motion to the slime enemy if they originally spawned on the same screen as the player is currently on. */
	// Update is called once per frame
	void Update () {
		speed = stats.Enemies [int.Parse (this.gameObject.name.Substring (6)), 5] * 0.5f * Time.deltaTime;
		sibling = transform.GetSiblingIndex () - stats.Rooms [roomLoader.room, 1];
		if (sibling < stats.Rooms [roomLoader.room, 1]) {
			sibling += stats.Rooms [roomLoader.room, 1];
			if (sibling * stats.Rooms [roomLoader.room, 1] > transform.GetSiblingIndex ()) {
				sibling = 0;
			}
		}
		//Debug.Log(roomLoader.room);
		//Debug.Log (speed);
		if (location == (camMov.locX + "." + camMov.locY)) {
			
			if (delay == 0) {
				RaycastHit2D DetectPlayer = Physics2D.Raycast (this.gameObject.transform.position - new Vector3(0, 0.1f), (player.transform.position - transform.position - new Vector3(0, 0.1f))*2);
				//Debug.DrawRay (transform.position, (player.transform.position - transform.position), Color.white, 10); 
				//Debug.Log (DetectPlayer.collider.name);
				if (DetectPlayer.collider.name == "Player") {
				
					//Debug.Log ("nearby");
					//Debug.Log (camMov.locX + "." + camMov.locY);
					//Debug.Log (location);;
					/* Calculates angle from enemy to player */
					angle = Mathf.Rad2Deg * (Mathf.Atan (Mathf.Abs ((0.5f + 0.5f * Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y)) * (player.transform.position.x - this.transform.position.x) / (player.transform.position.y - this.transform.position.y) + ((0.5f + 0.5f * -Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y))) * (player.transform.position.y - this.transform.position.y) / (player.transform.position.x - this.transform.position.x)))) + 45 * (2 - 2 * Mathf.Sign (player.transform.position.x - this.transform.position.x) + 1 - Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y));

				} else {
					
					RaycastHit2D Gather = Physics2D.Raycast (this.gameObject.transform.position - new Vector3(0, 0.1f), transform.parent.GetChild (sibling).position - this.gameObject.transform.position - new Vector3(0, 0.1f), Vector2.Distance(this.gameObject.transform.position,transform.parent.GetChild (sibling).position));
					//Debug.DrawRay (this.gameObject.transform.position - new Vector3 (0, 0.1f), transform.parent.GetChild (sibling).position - this.gameObject.transform.position - new Vector3 (0, 0.1f), Color.white, 10);
					//Debug.Log (Gather.collider.name);

					//Debug.Log (transform.GetSiblingIndex ());
					//(transform.parent.GetChild (transform.GetSiblingIndex () + 1))

					if (Gather.collider == null && Vector2.Distance(this.gameObject.transform.position,transform.parent.GetChild (sibling).position) > 0.6f) {
						//Debug.Log (Gather.collider.name + "B");
						//Debug.Log ("A");
		
						angle = Mathf.Rad2Deg * (Mathf.Atan (Mathf.Abs ((0.5f + 0.5f * Mathf.Sign (transform.parent.GetChild (sibling).transform.position.x - this.transform.position.x) * Mathf.Sign (transform.parent.GetChild (sibling).transform.position.y - this.transform.position.y)) * (transform.parent.GetChild (sibling).transform.position.x - this.transform.position.x) / (transform.parent.GetChild (sibling).transform.position.y - this.transform.position.y) + ((0.5f + 0.5f * -Mathf.Sign (transform.parent.GetChild (sibling).transform.position.x - this.transform.position.x) * Mathf.Sign (transform.parent.GetChild (sibling).transform.position.y - this.transform.position.y))) * (transform.parent.GetChild (sibling).transform.position.y - this.transform.position.y) / (transform.parent.GetChild (sibling).transform.position.x - this.transform.position.x)))) + 45 * (2 - 2 * Mathf.Sign (transform.parent.GetChild (sibling).transform.position.x - this.transform.position.x) + 1 - Mathf.Sign (transform.parent.GetChild (sibling).transform.position.x - this.transform.position.x) * Mathf.Sign (transform.parent.GetChild (sibling).transform.position.y - this.transform.position.y));


					} else {
						delay = 100;
						angle = -1;
						//Debug.Log ("B");
					}
				}
			} else {
				delay -= 1;
			}
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

		if (IV == true & attack.Attack == false) {
			IV = false;
			gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		if (health <= 0) {
			GameObject.Find ("PassiveCodeController").GetComponent<DropGenerator> ().BroadcastMessage ("Item", this.gameObject);
			Destroy (this.gameObject);
		}
	}
	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player" & Player.invincible == false) {
			Player.hp -= damage;
			Player.ivFrames = true;
			//Destroy (this.gameObject);
		}
	}
	void damaged(int dmg) {
		if (IV == false) {
			Debug.Log ("damaged");
			gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
			health -= dmg;
			Debug.Log (health);
			IV = true;


		}
	}

}
