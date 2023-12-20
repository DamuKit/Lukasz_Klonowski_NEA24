using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickMovement : MonoBehaviour {
	public float angle;
	public float speed;
	public float baseSpeed;

	StatsStorage stats;
	PetInteraction interaction;
	// Use this for initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		interaction = GameObject.Find ("PetInteraction").GetComponent<PetInteraction> ();
		baseSpeed = 3f;
		angle = -10;
	}
	
	// Update is called once per frame
	void Update () {
		angle = interaction.angle;
		speed = baseSpeed * 0.5f * Time.deltaTime * stats.pause;

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


}
