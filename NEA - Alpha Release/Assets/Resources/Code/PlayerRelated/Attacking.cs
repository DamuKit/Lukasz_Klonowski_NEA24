using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour {
	public PlayerMovement playerMovement;
	public bool Attack;
	public float cooldownLim;
	public float counter;
	public float attackduration;
	// Use this for initialization
	void Start () {
		playerMovement = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		Attack = false;
		cooldownLim = 0;
		attackduration = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (playerMovement.angle + "e");
		this.gameObject.transform.rotation = Quaternion.identity;
		this.gameObject.transform.Rotate (0, 0, -playerMovement.angle + 180);

		//Debug.Log (counter);
		if (Input.GetKeyDown (KeyCode.Mouse0) == true && counter <=0 && Attack == false) {
			Attack = true;
			counter = attackduration;
			playerMovement.SendMessage ("attack");
		}
		if (Attack == true) {
			
			if (counter <= 0) {
				Attack = false;
				counter = cooldownLim; 
			}
		}
			if(counter >0){
				counter -= 1 * Time.deltaTime;
			}
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy" & Attack == true) {
			other.gameObject.SendMessage ("damaged", 5);
			//Destroy (other.gameObject);
		}
	}
}
