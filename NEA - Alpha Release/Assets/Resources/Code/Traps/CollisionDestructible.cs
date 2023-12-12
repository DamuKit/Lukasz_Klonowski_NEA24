using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestructible : MonoBehaviour {
	public Attacking attack;
	int health;
	bool IV;
	public EnemyHealth HPBar;
	// Use this for initialization
	void Start () {
		HPBar = gameObject.transform.Find("EnemyHP").GetComponent<EnemyHealth>();
		attack = GameObject.Find ("AttackHitBox").GetComponent<Attacking> ();
		health = 20;
		IV = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy (this.gameObject);
		}
	}

	private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			if (IV == false) {
				StartCoroutine ("Damaged");
				health -= Mathf.RoundToInt (attack.damage);
			}
			//Destroy (this.gameObject);
		}
		HPBar.SendMessage ("HealthReport", health);
	}

	public IEnumerator Damaged(){
		IV = true;
		gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds (0.25f);
		gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
		IV = false;
	}
}
