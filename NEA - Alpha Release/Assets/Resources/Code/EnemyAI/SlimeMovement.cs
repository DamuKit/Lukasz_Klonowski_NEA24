/*Created: Sprint 2 - Last Edited Sprint 8
This script’s purpose is to manage the actions and behaviour of the slime enemy. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour {
	public StatsStorage stats;
	public Attacking attack;
	bool IV;
	public CameraMovement camMov;
	public PlayerMovement Player;
	public RoomLoader roomLoader;
	string location;
	public GameObject player;
	public float angle;
	int delay;
	private int sibling;
	public int statVariance;
	public int baseSpeed;
	int wonder;
	public float speed;
	public int health;
	public int damage;
	public EnemyHealth HPBar;
	public bool IVTime;

	// Initialization
	void Start () {
		IVTime = false;
		HPBar = gameObject.transform.Find("EnemyHP").GetComponent<EnemyHealth>();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		attack = GameObject.Find ("AttackHitBox").GetComponent<Attacking> ();
		IV = false;
		roomLoader = GameObject.Find ("PassiveCodeController").GetComponent<RoomLoader> ();
		camMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement> ();
		location = stats.Locations[stats.Locations.Count - 1];
		this.gameObject.name = (this.gameObject.name.Substring (0, 4));
		player = GameObject.FindGameObjectWithTag ("Player");
		Player = player.GetComponent<PlayerMovement> ();
		delay = 0;
		health = stats.Enemies [int.Parse(this.gameObject.name.Substring(1)), 3];
		damage = stats.Enemies [int.Parse(this.gameObject.name.Substring(1)), 4];
		baseSpeed = stats.Enemies [int.Parse (this.gameObject.name.Substring (1)), 5];
		Random.InitState (stats.seed + stats.seedoffset);
		stats.seedoffset += 1;
		statVariance = -health - damage - baseSpeed;
		//setting random stats
		health = Mathf.RoundToInt(health * (Random.Range (0.75f, 1.5f) + stats.room * 0.1f * (stats.Difficulty - 2/3)* 3));
		damage = Mathf.RoundToInt(damage * (Random.Range (0.75f, 1.5f) + stats.room * 0.1f * (stats.Difficulty - 2/3)* 3));
		baseSpeed = Mathf.RoundToInt(baseSpeed * (Random.Range (0.75f, 1.5f) + stats.room * 0.001f));
		stats.enemystatpoints += Mathf.RoundToInt((statVariance + damage + health + baseSpeed) * (stats.Difficulty * 0.1f + 0.3f));
		sibling = transform.GetSiblingIndex () - stats.Rooms [roomLoader.room, 1];
		if (sibling < stats.Rooms [roomLoader.room, 1]) {
			sibling += stats.Rooms [roomLoader.room, 1];
		}
		wonder = 0;
	}

	// Update once per frame
	void Update () {
		speed = baseSpeed * 0.5f * Time.deltaTime * stats.pause;
		if (wonder == 1) {
			speed *= 0.5f;
		}
		sibling = transform.GetSiblingIndex () - stats.Rooms [roomLoader.room, 1];
		if (sibling < stats.Rooms [roomLoader.room, 1]) {
			sibling += stats.Rooms [roomLoader.room, 1];
			if (sibling * stats.Rooms [roomLoader.room, 1] > transform.GetSiblingIndex ()) {
				sibling = 0;
			}
		}
		if (location == (camMov.locX + "." + camMov.locY)) {
			if (delay == 0) {
				wonder = 0;
				// Detect player
				RaycastHit2D DetectPlayer = Physics2D.Raycast (this.gameObject.transform.position - new Vector3(0, 0.1f), (player.transform.position - transform.position - new Vector3(0, 0.1f))*2);
				if (DetectPlayer.collider.name == "Player" & Player.hp>0 & Player.repellant == false & DetectPlayer.distance <= 6) {
					// Calculates angle from enemy to player
					angle = Mathf.Rad2Deg * (Mathf.Atan (Mathf.Abs ((0.5f + 0.5f * Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y)) * (player.transform.position.x - this.transform.position.x) / (player.transform.position.y - this.transform.position.y) + ((0.5f + 0.5f * -Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y))) * (player.transform.position.y - this.transform.position.y) / (player.transform.position.x - this.transform.position.x)))) + 45 * (2 - 2 * Mathf.Sign (player.transform.position.x - this.transform.position.x) + 1 - Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y));
				} else {
					RaycastHit2D Gather = Physics2D.Raycast (this.gameObject.transform.position - new Vector3(0, 0.1f), transform.parent.GetChild (sibling).position - this.gameObject.transform.position - new Vector3(0, 0.1f), Vector2.Distance(this.gameObject.transform.position,transform.parent.GetChild (sibling).position));
					if (Gather.collider == null && Vector2.Distance(this.gameObject.transform.position,transform.parent.GetChild (sibling).position) > 0.7f) {
						angle = Mathf.Rad2Deg * (Mathf.Atan (Mathf.Abs ((0.5f + 0.5f * Mathf.Sign (transform.parent.GetChild (sibling).transform.position.x - this.transform.position.x) * Mathf.Sign (transform.parent.GetChild (sibling).transform.position.y - this.transform.position.y)) * (transform.parent.GetChild (sibling).transform.position.x - this.transform.position.x) / (transform.parent.GetChild (sibling).transform.position.y - this.transform.position.y) + ((0.5f + 0.5f * -Mathf.Sign (transform.parent.GetChild (sibling).transform.position.x - this.transform.position.x) * Mathf.Sign (transform.parent.GetChild (sibling).transform.position.y - this.transform.position.y))) * (transform.parent.GetChild (sibling).transform.position.y - this.transform.position.y) / (transform.parent.GetChild (sibling).transform.position.x - this.transform.position.x)))) + 45 * (2 - 2 * Mathf.Sign (transform.parent.GetChild (sibling).transform.position.x - this.transform.position.x) + 1 - Mathf.Sign (transform.parent.GetChild (sibling).transform.position.x - this.transform.position.x) * Mathf.Sign (transform.parent.GetChild (sibling).transform.position.y - this.transform.position.y));
					} else {
						// Otherwise randomise direction
						delay = Random.Range(50, 125);
						angle = Random.Range(-100, 360);
						wonder = 1;
					}
				}
			} else {
				delay -= 1;
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
		// Invincibility
		if (IV == true & IVTime == false & attack.Attack == false) {
			IV = false;
			gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		if (stats.killall == true) {
			health = -10;
		}
		// On kill
		if (health <= 0) {
			Player.m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/explosion"));
			GameObject.Find ("PassiveCodeController").GetComponent<DropGenerator> ().BroadcastMessage ("Item", this.gameObject);
			Player.xp += stats.Enemies [int.Parse (this.gameObject.name.Substring (1)), 2] * 0.25f;
			stats.score += stats.Enemies [int.Parse (this.gameObject.name.Substring (1)), 2] * 0.25f;
			stats.kills +=1;
			Destroy (this.gameObject);
		}
		HPBar.SendMessage ("HealthReport", health);
	}

	// Detect when colliding with the player
	private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Player" & Player.invincible == false) {
			other.gameObject.SendMessage ("Damaged", damage);
		}
	}

	// Taking damage
	void damaged(int dmg) {
		if (IV == false) {
			Player.m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/MeleeAttack"));
			gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
			health -= dmg;
			stats.LifetimeDamage += dmg;
			StartCoroutine ("Invincibility");
		}
	}
		
	// Provide invincibility to the player
	public IEnumerator Invincibility(){
		IV = true;
		IVTime = true;
		yield return new WaitForSeconds (0.35f);
		IVTime = false;
	}
}
