using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieces : MonoBehaviour {

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
	bool turn;
	public float speed;
	public float health;
	public int damage;
	public EnemyHealth HPBar;
	public bool IVTime;
	public bool move;
	public bool PlayerMove;
	bool EndTurn;
	bool FollowedRules;
	// Use this for initialization
	void Start () {
		FollowedRules = true;
		turn = false;
		move = false;
		IVTime = false;
		PlayerMove = false;
		EndTurn = false;;
		HPBar = gameObject.transform.Find("EnemyHP").GetComponent<EnemyHealth>();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		attack = GameObject.Find ("AttackHitBox").GetComponent<Attacking> ();
		IV = false;
		Cam = GameObject.FindGameObjectWithTag("MainCamera");
		roomLoader = GameObject.Find ("PassiveCodeController").GetComponent<RoomLoader> ();
		camMov = Cam.GetComponent<CameraMovement> ();
		location = stats.Locations[stats.Locations.Count - 1];
		//Debug.Log (location);
		this.gameObject.name = (this.gameObject.name.Substring (0, 4));


		player = GameObject.FindGameObjectWithTag ("Player");
		Player = player.GetComponent<PlayerMovement> ();
		delay = 0;

		health = stats.Enemies [int.Parse(this.gameObject.name.Substring(1)), 3];
		damage = stats.Enemies [int.Parse(this.gameObject.name.Substring(1)), 4];
		//speed = stats.Enemies[int.Parse(this.gameObject.name.Substring(1)),5] * 0.5f;
		baseSpeed = stats.Enemies [int.Parse (this.gameObject.name.Substring (1)), 5];

		Random.InitState (stats.seed + stats.seedoffset);
		stats.seedoffset += 1;

		statVariance = -Mathf.RoundToInt(health) - damage - baseSpeed;
		//setting random stats
		health = Mathf.RoundToInt(health * (Random.Range (0.75f, 1.5f) + stats.room * 0.1f * (stats.Difficulty - 2/3)* 3));
		damage = Mathf.RoundToInt(damage * (Random.Range (0.75f, 1.5f) + stats.room * 0.1f * (stats.Difficulty - 2/3)* 3));

		//Debug.Log (health + " " + damage + " " + baseSpeed);
		//stats.enemystatpoints += Mathf.RoundToInt((statVariance + damage + health + baseSpeed) * (stats.Difficulty * 0.1f + 0.3f));
	}

	// Update is called once per frame
	void Update () {
		if (Mathf.RoundToInt(Time.time) % 2 == 0 & this.gameObject.name.Substring (1, 3) == "004") {
			if (turn == false) {
				move = true;
			}

			turn = true;
			if (Player.moving == true) {
				PlayerMove = true;
				FollowedRules = false;
			}
			EndTurn = false;
		} else if (Mathf.RoundToInt(Time.time) % 2 == 1 & this.gameObject.name.Substring (1, 3) == "005") {
			if (turn == false) {
				move = true;
			}
			turn = true;
			if (Player.moving == true) {
				PlayerMove = true;
				FollowedRules = false;
			}
			EndTurn = false;
		} else {
			if (PlayerMove == true & EndTurn == false) {
				turn = true;
				move = true;
				EndTurn = true;
				PlayerMove = false;
			} else if (EndTurn == false) {
				turn = false;
				move = false;
				EndTurn = true;
			}
		}
		speed = baseSpeed * Time.deltaTime * stats.pause;
		if (wonder == 1) {
			speed *= 1f;
		}

		if (stats.pause == 1) {
		}

		if (location == (camMov.locX + "." + camMov.locY) & turn == true) {
			if (delay <= 0) {
				wonder = 0;

				RaycastHit2D DetectPlayer = Physics2D.Raycast (this.gameObject.transform.position - new Vector3(0, 0.1f), (player.transform.position - transform.position - new Vector3(0, 0.1f))*2);

				if (move == true) {
					if (DetectPlayer.collider.name == "Player" & Player.hp > 0 & Player.repellant == false & DetectPlayer.distance <= 8) {
						if (DetectPlayer.distance <= 8) {
							angle = Mathf.Rad2Deg * (Mathf.Atan (Mathf.Abs ((0.5f + 0.5f * Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y)) * (player.transform.position.x - this.transform.position.x) / (player.transform.position.y - this.transform.position.y) + ((0.5f + 0.5f * -Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y))) * (player.transform.position.y - this.transform.position.y) / (player.transform.position.x - this.transform.position.x)))) + 45 * (2 - 2 * Mathf.Sign (player.transform.position.x - this.transform.position.x) + 1 - Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y));
						}

					} else {

						delay = Random.Range (40, 100);
						angle = Random.Range (-100, 360);
						wonder = 1;
						//Debug.Log ("B");
					}
					move = false;
				}
			} else {
				delay -= 1;
			}
			if (turn == true) {
				if (angle <= 360 & angle >= 315 | angle <= 45 & angle >= 0) {
					this.transform.Translate (0, speed, 0);
				} else if (angle >= 45 & angle <= 135) {
					this.transform.Translate (speed, 0, 0);
				} else if (angle >= 135 & angle <= 225) {
					this.transform.Translate (0, -speed, 0);
				} else if (angle >= 225 & angle <= 315) {
					this.transform.Translate (-speed, 0, 0);
				} 
			}
		}

		if (IV == true & IVTime == false & attack.Attack == false) {
			IV = false;
			gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		if (stats.killall == true) {
			health = -10;
		}
		if (health <= 0) {
			Player.m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/explosion"));
			GameObject.Find ("PassiveCodeController").GetComponent<DropGenerator> ().BroadcastMessage ("Item", this.gameObject);
			Player.xp += stats.Enemies [int.Parse (this.gameObject.name.Substring (1)), 2] * 0.25f;
			stats.score += stats.Enemies [int.Parse (this.gameObject.name.Substring (1)), 2] * 0.25f;
			stats.kills +=1;
			if (FollowedRules == true) {
				stats.Achievements[2,2] = "T";
			}
			Destroy (this.gameObject);
		}
		HPBar.SendMessage ("HealthReport", health);
	}
	void damaged(int dmg) {
		if (IV == false) {
			Player.m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/MeleeAttack"));
			Debug.Log ("damaged");
			gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
			if (turn == true) {
				health -= dmg * 0.25f;
				stats.LifetimeDamage += dmg * 0.25f;
			} else {
				health -= dmg;
				stats.LifetimeDamage += dmg;
			}
			Debug.Log (health);
			StartCoroutine ("Invincibility");
		}
	}
	private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Player" & Player.invincible == false) {
			other.gameObject.SendMessage ("Damaged", damage);

		}
	}

	public IEnumerator Invincibility(){
		IV = true;
		IVTime = true;
		yield return new WaitForSeconds (0.35f);
		IVTime = false;
	}
}
