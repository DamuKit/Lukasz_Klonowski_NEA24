/*Created: Sprint 1 - Last Edited Sprint 4
Purpose: This script manages the control of the playable character by adding controls for various actions with suitable animations. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public AudioSource m_audio;
	public float angle;
	float dashDirection;
	float speed;
	public float duration;
	float durationLim;
	Animator Animation;
	Attacking interact;
	public CameraMovement camMov;
	public GameObject Slime;
	string direction;
	bool moving;
	public bool dashing;
	public float camerasizex;
	public float camerasizey;
	public float hp;
	public float maxhp;
	public float maxStamina;
	public float stamina;
	public bool invincible;
	public bool ivFrames;
	public int ivDuration;
	public StatsStorage stats;
	public float xp;
	public int level;
	public int[] lockmovement = new int[] {1,1,1,1};
	public int[] Items = new int[] {0,0,0,0,0,0,0,0,0,0,0};
	public bool repellant;
	int repellantStack;
	public bool wither;
	public bool confused;
	InventoryBehaviour InvBeh;

	float speedbuff;

	// Use this for initialization
	void Start () {
		InvBeh = GameObject.Find ("Inventory").GetComponent<InventoryBehaviour> ();
		m_audio = this.gameObject.GetComponent<AudioSource> ();

		//m_audio.clip = Resources.Load<AudioClip>("Prefabs/Audio/hitHurt.wav");
		//m_audio.PlayOneShot(Resources.Load<AudioClip>("Prefabs/Audio/hitHurt.wav"));
		interact = GameObject.Find ("AttackHitBox").GetComponent<Attacking> ();
		speedbuff = 0;
		speed = 0.05f;
		dashDirection = 0f;
		durationLim = 15;
		duration = durationLim;
		direction = "down";
		moving = false;
		dashing = false;
		camerasizex = 12f;
		camerasizey = 8f;
		Animation = GetComponent<Animator>();
		maxhp = 300;
		hp = maxhp;
		maxStamina = 100;
		stamina = maxStamina;
		invincible = false;
		ivFrames = false;
		ivDuration = 0;
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		xp = 0;
		level = 1;
		repellantStack = 0;
		wither = false;
		confused = false;

	}

	// Update is called once per frame
	void Update () {
		m_audio.volume = stats.Master * stats.SFX;

		
		if (stats.pause == 1 & wither == true) {
			hp -= maxhp * 0.01f * Time.deltaTime;
		}
		if (hp < 0) {
			hp = 0;
		} else if (hp > maxhp) {
			hp = maxhp;
		}
		if (stamina < maxStamina) {
			stamina += (1.5f + 0.15f * stamina) * Time.deltaTime;
		}
		if (stamina > maxStamina) {
			stamina = maxStamina;
		}

		speed = (2.5f + speedbuff) * Time.deltaTime * stats.gameSpeed * stats.pause;

		//Debug.Log (speed);
		if (ivFrames == true) {
			invincible = true;
			ivDuration = 100;
			ivFrames = false;
		}
		if (ivDuration > 0) {
			ivDuration -= 1;
		} else{
			invincible = false;
		}

		moving = false;

		Angle();
		//Debug.Log (angle);

		/* This section checks input to allow the player to move */
		if (hp > 0 & stats.pause != 0) {
			Animation.SetBool ("walk", false);
			if (Input.GetKey (KeyCode.A) == true && dashing == false) {
				if (confused == true) {
					this.transform.Translate (speed * lockmovement [1], 0, 0);
				} else {
					this.transform.Translate (-speed * lockmovement [3], 0, 0);
				}

				moving = true;
				Animation.SetBool ("walk", true);
			} else if (Input.GetKey (KeyCode.D) == true && dashing == false) {
				if (confused == true) {
					this.transform.Translate (-speed * lockmovement [3], 0, 0);
				} else {
					this.transform.Translate (speed * lockmovement [1], 0, 0);
				}
				moving = true;
				Animation.SetBool ("walk", true);
			}

			if (Input.GetKey (KeyCode.W) == true && dashing == false) {
				if (confused == true) {
					this.transform.Translate (0, -speed * lockmovement [2], 0);
				} else {
					this.transform.Translate (0, speed * lockmovement [0], 0);
				}
				moving = true;
				Animation.SetBool ("walk", true);
			} else if (Input.GetKey (KeyCode.S) == true && dashing == false) {
				if (confused == true) {
					this.transform.Translate (0, speed * lockmovement [0], 0);
				} else {
					this.transform.Translate (0, -speed * lockmovement [2], 0);
				}
				moving = true;
				Animation.SetBool ("walk", true);
			}
			/* This provides a dash which prevents other actions from ocurring. This occurs for a number of frames and checks the angle of the player initially to move them in those frames.*/
			if (Input.GetKeyDown (KeyCode.LeftShift) == true && dashing == false & interact.shieldWield == true & stamina > 40) {
				stamina -= 30;
				interact.Attack = true;
				interact.counter = 1;
				dashing = true;
				Animation.SetBool ("dash", true);
				Animation.Play ("Dash");
			}

			if (dashing == true && duration > 0) {

				interact.counter = 0.25f;
				ivFrames = true;
				ivDuration = 1;
				moving = true;
				if (duration == durationLim) {
					dashDirection = angle;
				}
				duration -= 1;
				if (Input.GetKey (KeyCode.LeftShift) == true) {
					duration += 0.3f;
				}
				if (dashDirection >= 337.5 | dashDirection <= 22.5) {
					this.transform.Translate (0, 3 * speed * lockmovement [0], 0);
				} else if (dashDirection >= 22.5 && dashDirection <= 67.5) {
					this.transform.Translate (1.5f * speed * lockmovement [1], 1.5f * speed * lockmovement [0], 0);
				} else if (dashDirection >= 67.5 && dashDirection <= 112.5) {
					this.transform.Translate (3 * speed * lockmovement [1], 0, 0);
				} else if (dashDirection >= 112.5 && dashDirection <= 157.5) {
					this.transform.Translate (1.5f * speed * lockmovement [1], -1.5f * speed * lockmovement [2], 0);
				} else if (dashDirection >= 157.5 && dashDirection <= 202.5) {
					this.transform.Translate (0, -3 * speed * lockmovement [2], 0);
				} else if (dashDirection >= 202.5 && dashDirection <= 247.5) {
					this.transform.Translate (-1.5f * speed * lockmovement [3], -1.5f * speed * lockmovement [2], 0);
				} else if (dashDirection >= 247.5 && dashDirection <= 292.5) {
					this.transform.Translate (-3 * speed * lockmovement [3], 0, 0);
				} else if (dashDirection >= 292.5 && dashDirection <= 337.5) {
					this.transform.Translate (-1.5f * speed * lockmovement [3], 1.5f * speed * lockmovement [0], 0);
				}

			} else if(dashing == true) {
				duration = durationLim;
				Animation.SetBool ("dash", false);
				dashing = false;
			}

			/* This section checks the angle of the pmouse cursor from the player to change the player animation to face the cursor. */
			if (Input.mousePosition.x - (Display.main.systemWidth / 2) >= 0 & Input.mousePosition.y - (Display.main.systemHeight / 2) >= 0) {
				//Debug.Log ("e");
			}
			if (angle >= 45 && angle <= 135) {
				//Debug.Log((this.transform.position.x / camerasizex)* (Display.main.systemWidth/2));
				//Debug.Log (Input.mousePosition.x - (Display.main.systemWidth / 2));
				direction = "right";
				//Debug.Log ("Right");
				Animation.SetBool ("right", true);
				Animation.SetBool ("left", false);
				Animation.SetBool ("up", false);
				Animation.SetBool ("down", false);

			} else if (angle >= 225 && angle <= 315) {
				direction = "left";
				//Debug.Log ("Left");
				Animation.SetBool ("left", true);
				Animation.SetBool ("right", false);
				Animation.SetBool ("up", false);
				Animation.SetBool ("down", false);

			} else if (angle >= 315 | angle <= 45) {
				direction = "up";
				//Debug.Log ("up");
				Animation.SetBool ("up", true);
				Animation.SetBool ("right", false);
				Animation.SetBool ("left", false);
				Animation.SetBool ("down", false);

			} else if (angle >= 135 && angle <= 225) {
				direction = "down";
				//Debug.Log ("down");
				Animation.SetBool ("down", true);
				Animation.SetBool ("right", false);
				Animation.SetBool ("left", false);
				Animation.SetBool ("up", false);
			}
			gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
		} else if (hp <=0){
			gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
		}

		/*if(Input.GetKey (KeyCode.Mouse0) == true){
			Object.Instantiate (Slime, new Vector3(0, 0, 0), Quaternion.identity);
		}*/
		//-(camMov.locX * camerasizex)
		//-(camMov.locY * camerasizey) (((this.transform.position.y -(camMov.locY * camerasizey))/ camerasizey)



		if (xp >= level * level * 0.03f+ 100) {
			xp -= level * level * 0.03f+ 100;
			level += 1;
			Debug.Log ("Level Up!");
			interact.damage += 0.25f;
			hp*= 1.1f;
			maxhp *= 1.005f;
			gameObject.GetComponent<ParticleSystem> ().Play ();
			m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/LevelUp"));
		}
	}
	public void Damaged(int damage) {
		if (invincible == false) {
			hp -= damage;
			ivFrames = true;
			m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/hitHurt"));
		}
	}
	/* This function provides the angle of the mouse cursor from the character.*/
	public void Angle () {
		if (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2)) >= 0 & 0 <= Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2))) {
			angle = (Mathf.Atan (Mathf.Abs (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2))) / (Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2))))) * Mathf.Rad2Deg);
		}
		else if (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2)) >= 0 & 0 >= Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2))) {
			angle = (90 + Mathf.Atan ((Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2)))) / Mathf.Abs (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2)))) * Mathf.Rad2Deg);
		}
		else if (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2)) <= 0 & 0 >= Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2))) {
			angle = (180 + Mathf.Atan (Mathf.Abs (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2))) / (Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2))))) * Mathf.Rad2Deg);
		}
		else if (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2)) <= 0 & 0 <= Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2))) {
			angle = (270 + Mathf.Atan ((Mathf.Abs (Input.mousePosition.y - (Display.main.systemHeight / 2) - (((this.transform.position.y - (camMov.locY * camerasizey * 2)) / camerasizey) * (Display.main.systemHeight / 2)))) / Mathf.Abs (Input.mousePosition.x - (Display.main.systemWidth / 2) - (((this.transform.position.x - (camMov.locX * camerasizex * 2)) / camerasizex) * (Display.main.systemWidth / 2)))) * Mathf.Rad2Deg);
		}

		if (confused == true) {
			angle = (angle + 180);
			if (angle > 360) {
				angle -= 360;
			}
		}
	}
	void attack(){
		Animation.Play ("Attack");
	}
	void itemEffect(int item){
		
		switch (item) {

		case(1):
			hp += 25;
			if (hp > maxhp) {
				hp = maxhp;
			}
			break;
		case(2):
			StartCoroutine ("damageBuff");
			break;
		case(3):
			StartCoroutine ("speedBuff");
			break;
		case(4):
			StartCoroutine ("repellantBuff");
			break;

		case(6):
			//StartCoroutine ("-");
			break;
		case(7):
			//StartCoroutine ("-");
			break;
		case(8):
			//StartCoroutine ("-");
			break;
		case(9):
			//StartCoroutine ("-");
			break;
		case(10):
			//StartCoroutine ("-");
			break;
		case(11):
			//StartCoroutine ("-");
			break;
		case(12):
			//StartCoroutine ("-");
			break;
		case(13):
			//StartCoroutine ("-");
			break;
		case(14):
			//StartCoroutine ("-");
			break;
		case(15):
			//StartCoroutine ("-");
			break;
		case(16):
			//StartCoroutine ("-");
			break;
		case(17):
			//StartCoroutine ("-");
			break;
		case(18):
			//StartCoroutine ("-");
			break;
		case(19):
			//StartCoroutine ("-");
			break;
		case(20):
			//StartCoroutine ("-");
			break;

		case(22):
			GunCrate (Random.value);
			break;
		case(23):
			MeleeCrate (Random.value);
			break;
		case(24):
			PotionCrate (Random.value);
			break;
		case(25):
			RandomCrate (Random.value);
			break;
		}


	}
	void Debuff(int debuff){
		switch (debuff) {
		case(1):
			StartCoroutine ("Wither");
			break;
		case(2):
			StartCoroutine ("Weak");
			break;
		case(3):
			StartCoroutine ("Confused");
			break;
		default:
			break;
		}
	}
	public void GunCrate(float p){
		if (p < 0.15) {
			InvBeh.items.Enqueue ("102N" + DmgCalc());
		} else if (p < 0.3) {
			InvBeh.items.Enqueue ("104N" + DmgCalc());
		} else if (p < 0.4) {
			InvBeh.items.Enqueue ("105N" + DmgCalc());
		} else if (p < 0.55) {
			InvBeh.items.Enqueue ("106N" + DmgCalc());
		} else if (p < 0.7) {
			InvBeh.items.Enqueue ("107N" + DmgCalc());
		} else if (p < 0.85) {
			InvBeh.items.Enqueue ("108N" + DmgCalc());
		} else if (p < 1) {
			InvBeh.items.Enqueue ("109N" + DmgCalc());
		} 
		InvBeh.items.Enqueue ("021" + (Random.Range (30, 90) + 1000).ToString ().Substring (1, 3));
	}
	public void MeleeCrate(float p){

	}
	public void PotionCrate(float p){

	}
	public void RandomCrate(float p){
		
	}

	public string DmgCalc(){
		if((Mathf.RoundToInt(level * 0.2f * interact.damage)).ToString ().Length <3){
			return(((Mathf.RoundToInt(level * 0.2f * interact.damage + 100)).ToString ().Length - 3).ToString () + (Mathf.RoundToInt(level * 0.2f * interact.damage)).ToString ("D3"));
		}
		else{
			return(((Mathf.RoundToInt(level * 0.2f * interact.damage + 100)).ToString ().Length - 3).ToString () + (Mathf.RoundToInt(level * 0.2f * interact.damage)).ToString ("G3"));
		}
	}

	public IEnumerator damageBuff(){
		GameObject.Find ("AttackHitBox").GetComponent<Attacking> ().damagebuff += 1;
		yield return new WaitForSeconds (10f);
		GameObject.Find ("AttackHitBox").GetComponent<Attacking> ().damagebuff -= 1;
	}
	public IEnumerator speedBuff(){
		speedbuff += 0.3f;
		yield return new WaitForSeconds (10f);
		speedbuff -= 0.3f;
	}
	public IEnumerator repellantBuff(){
		repellant = true;
		repellantStack++;
		yield return new WaitForSeconds (10f);
		repellantStack--;
		if (repellantStack == 0) {
			repellant = false;
		}
	}
	public IEnumerator Wither(){
		wither = true;
		yield return new WaitForSeconds (5f);
		wither = false;
	}
	public IEnumerator Weak(){
		GameObject.Find ("AttackHitBox").GetComponent<Attacking> ().damagebuff -= 1;
		yield return new WaitForSeconds (10f);
		GameObject.Find ("AttackHitBox").GetComponent<Attacking> ().damagebuff += 1;
	}
	public IEnumerator Confused(){
		confused = true;
		yield return new WaitForSeconds (5f);
		confused = false;
	}
}