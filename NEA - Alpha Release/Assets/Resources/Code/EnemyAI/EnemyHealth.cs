/*Created: Sprint - Last Edited Sprint 
This script’s purpose is to display the health of the enemy the object is attached to. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	Animator Animation;
	public int HPMax;
	public int HP;
	public int HPpercentage;
	// Use this for initialization
	void Start () {
		Animation = GetComponent<Animator>();
		HP = -1;
		HPMax = -1;
		HPpercentage = 100;
	}
	
	// Update is called once per frame
	void Update () {
		HPpercentage = Mathf.RoundToInt((HP * 1f / (HPMax) * 1f ) * 100);
		Animation.SetInteger ("HP%", HPpercentage);
		if (HPMax == HP) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color(1,1,1,0);
		} else {
			gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}

	void HealthReport(int health){
		if (HPMax == -1) {
			HPMax = health;

		} 
		HP = health;
	}
}
