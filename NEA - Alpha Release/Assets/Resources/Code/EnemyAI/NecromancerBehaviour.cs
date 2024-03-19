/*Created: Sprint 7 - Last Edited Sprint 8
This script’s purpose is to manage the actions and behaviour of the necromancer enemy. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerBehaviour : MonoBehaviour {
	
	bool onscreen = false;
	public StatsStorage stats;
	public Attacking attack;
	bool IV;
	public CameraMovement camMov;
	public PlayerMovement Player;
	public RoomLoader roomLoader;
	public string location;
	public GameObject player;
	public GameObject Cam;
	public float angle;
	public int delay;
	public int statVariance;
	public int baseSpeed;
	int wonder;
	public float speed;
	public int health;
	public int damage;
	public EnemyHealth HPBar;
	Animator Animation;
	public bool summoning;
	public bool summonCooldown;
	public bool IVTime;

	// Use this for initialization
	void Start () {
		IVTime = false;
		Animation = this.gameObject.GetComponent<Animator> ();
		HPBar = gameObject.transform.Find("EnemyHP").GetComponent<EnemyHealth>();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		attack = GameObject.Find ("AttackHitBox").GetComponent<Attacking> ();
		IV = false;
		Cam = GameObject.FindGameObjectWithTag("MainCamera");
		roomLoader = GameObject.Find ("PassiveCodeController").GetComponent<RoomLoader> ();
		camMov = Cam.GetComponent<CameraMovement> ();
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
		summoning = false;
		summonCooldown = false;
	}
	
	// Update is called once per frame
	void Update () {
		speed = baseSpeed * 0.5f * Time.deltaTime * stats.pause;
		if (wonder == 1) {
			speed *= 0.5f;
		}
		if (location == (camMov.locX + "." + camMov.locY)) {
			if (delay <= 0 & summoning == false) {
				wonder = 0;
				Animation.SetBool ("walk", true);
				// Detect player
				RaycastHit2D DetectPlayer = Physics2D.Raycast (this.gameObject.transform.position - new Vector3(0, 0.1f), (player.transform.position - transform.position - new Vector3(0, 0.1f))*2);
				if (DetectPlayer.collider.name == "Player" & Player.hp > 0 & Player.repellant == false & DetectPlayer.distance <= 8) {
					if (DetectPlayer.distance <= 3) {
						// Identify the direction the player is in
						angle = Mathf.Rad2Deg * (Mathf.Atan (Mathf.Abs ((0.5f + 0.5f * Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y)) * (player.transform.position.x - this.transform.position.x) / (player.transform.position.y - this.transform.position.y) + ((0.5f + 0.5f * -Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y))) * (player.transform.position.y - this.transform.position.y) / (player.transform.position.x - this.transform.position.x)))) + 45 * (2 - 2 * Mathf.Sign (player.transform.position.x - this.transform.position.x) + 1 - Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y));
						angle = (angle + 180);
						if (angle > 360) {
							angle -= 360;
						}
						// Spawn enemy
					} else if (DetectPlayer.distance <= 8) {
						if (summonCooldown == false) {
							angle = Mathf.Rad2Deg * (Mathf.Atan (Mathf.Abs ((0.5f + 0.5f * Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y)) * (player.transform.position.x - this.transform.position.x) / (player.transform.position.y - this.transform.position.y) + ((0.5f + 0.5f * -Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y))) * (player.transform.position.y - this.transform.position.y) / (player.transform.position.x - this.transform.position.x)))) + 45 * (2 - 2 * Mathf.Sign (player.transform.position.x - this.transform.position.x) + 1 - Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y));
							Animation.Play ("Summon");
							StartCoroutine ("Summon");
						} 
						// Move away if summon is on cooldown
						else if((DetectPlayer.distance <= 5)){
							angle = Mathf.Rad2Deg * (Mathf.Atan (Mathf.Abs ((0.5f + 0.5f * Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y)) * (player.transform.position.x - this.transform.position.x) / (player.transform.position.y - this.transform.position.y) + ((0.5f + 0.5f * -Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y))) * (player.transform.position.y - this.transform.position.y) / (player.transform.position.x - this.transform.position.x)))) + 45 * (2 - 2 * Mathf.Sign (player.transform.position.x - this.transform.position.x) + 1 - Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y));
							angle = (angle + 180);
							if (angle > 360) {
								angle -= 360;
							}
						}
						// Otherwise randomise direction
						else if((DetectPlayer.distance <= 8)){
							delay = Random.Range(40, 100);
							angle = Random.Range(-100, 360);
							wonder = 1;
						}
						else {
							delay = 1;
							angle = -1;
							wonder = 1;
						}
					}
				} 
				else {

					delay = Random.Range(40, 100);
					angle = Random.Range(-100, 360);
					wonder = 1;
				}
			} else {
				Animation.SetBool ("walk", false);
				delay -= 1;
			}
			if (summoning == false) {
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
			// Identify direction for sprite
			if (angle >= 0) {
				if (angle <= 360 & angle >= 315 | angle <= 45 & angle >= 0) {
					Animation.SetInteger ("Direction", 0);
				} else if (angle >= 45 & angle <= 135) {
					Animation.SetInteger ("Direction", 1);
				} else if (angle >= 135 & angle <= 225) {
					Animation.SetInteger ("Direction", 2);
				} else if (angle >= 225 & angle <= 315) {
					Animation.SetInteger ("Direction", 3);
				}
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

	// Taking damage
	void damaged(int dmg) {
		if (IV == false) {
			Player.m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/MeleeAttack"));
			if (summoning == true) {
				summoning = false;
				Animation.Play ("transitionState");
			}
			gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
			health -= dmg;
			stats.LifetimeDamage += dmg;
			StartCoroutine ("Invincibility");
		}
	}

	// Summoning enemy
	public IEnumerator Summon(){
		Animation.Play ("Summon");
		summoning = true;
		summonCooldown = true;
		yield return new WaitForSeconds (2f);
		if (summoning == true) {
			summoning = false;
			Object.Instantiate (stats.EnemyID[3], this.gameObject.transform.position + new Vector3 (Mathf.Sin(Animation.GetInteger("Direction") * 90),Mathf.Cos(Animation.GetInteger("Direction") * 90)), Quaternion.identity, GameObject.Find("Enemies").transform);
		}
		yield return new WaitForSeconds (10f);
		summonCooldown = false;
	}

	// Provide invincibility to the player
	public IEnumerator Invincibility(){
		IV = true;
		IVTime = true;
		yield return new WaitForSeconds (0.35f);
		IVTime = false;
	}
}
