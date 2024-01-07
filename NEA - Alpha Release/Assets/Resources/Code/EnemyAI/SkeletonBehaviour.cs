using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour {
	
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
	public float decay;

	public float speed;
	public float health;
	public int damage;
	public EnemyHealth HPBar;
	Animator Animation;
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
		baseSpeed = Mathf.RoundToInt(baseSpeed * (Random.Range (0.75f, 1.5f) + stats.room * 0.001f));
		decay = health;
		//Debug.Log (health + " " + damage + " " + baseSpeed);
		//stats.enemystatpoints += Mathf.RoundToInt((statVariance + damage + health + baseSpeed) * (stats.Difficulty * 0.1f + 0.3f));
	}
	
	// Update is called once per frame
	void Update () {
		speed = baseSpeed * 0.5f * Time.deltaTime * stats.pause;
		if (wonder == 1) {
			speed *= 0.5f;
		}

		if (stats.pause == 1) {
			health -= decay * 0.1f * Time.deltaTime;
		}

		if (location == (camMov.locX + "." + camMov.locY)) {
			if (delay <= 0) {
				wonder = 0;
				Animation.SetBool ("walk", true);

				RaycastHit2D DetectPlayer = Physics2D.Raycast (this.gameObject.transform.position - new Vector3(0, 0.1f), (player.transform.position - transform.position - new Vector3(0, 0.1f))*2);

				if (DetectPlayer.collider.name == "Player" & Player.hp > 0 & Player.repellant == false & DetectPlayer.distance <= 8) {
					if (DetectPlayer.distance <= 3) {
						angle = Mathf.Rad2Deg * (Mathf.Atan (Mathf.Abs ((0.5f + 0.5f * Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y)) * (player.transform.position.x - this.transform.position.x) / (player.transform.position.y - this.transform.position.y) + ((0.5f + 0.5f * -Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y))) * (player.transform.position.y - this.transform.position.y) / (player.transform.position.x - this.transform.position.x)))) + 45 * (2 - 2 * Mathf.Sign (player.transform.position.x - this.transform.position.x) + 1 - Mathf.Sign (player.transform.position.x - this.transform.position.x) * Mathf.Sign (player.transform.position.y - this.transform.position.y));
					}
				}
				else {
					
					delay = Random.Range(40, 100);
					angle = Random.Range(-100, 360);
					wonder = 1;
					//Debug.Log ("B");
				}
			} else {
				Animation.SetBool ("walk", false);
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
		if (IV == true & IVTime == false & attack.Attack == false) {
			IV = false;
			gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		if (stats.killall == true) {
			health = -10;
		}
		if (health <= 0) {
			Player.m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/explosion"));
			//GameObject.Find ("PassiveCodeController").GetComponent<DropGenerator> ().BroadcastMessage ("Item", this.gameObject);
			//Player.xp += stats.Enemies [int.Parse (this.gameObject.name.Substring (1)), 2] * 0.25f;
			//stats.score += stats.Enemies [int.Parse (this.gameObject.name.Substring (1)), 2] * 0.25f;
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
			StartCoroutine ("Invincibility");
		}
	}
	private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Player" & Player.invincible == false) {
			other.gameObject.SendMessage ("Debuff", Random.Range(1,4) );
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
