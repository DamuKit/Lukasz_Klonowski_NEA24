using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour {
	public PlayerMovement playerMovement;
	public bool Attack;
	public float cooldown;
	// Use this for initialization
	void Start () {
		playerMovement = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		Attack = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (playerMovement.angle + "e");
		this.gameObject.transform.rotation = Quaternion.identity;
		this.gameObject.transform.Rotate (0, 0, -playerMovement.angle + 180);

		if (Input.GetKeyDown (KeyCode.Mouse0) == true) {
			Attack = true;
		}
	}
}
