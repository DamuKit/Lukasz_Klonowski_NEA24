/*Created: Sprint 2 - Last Edited Sprint 2
Purpose: This script manages the movement of the basic green slime enemy. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour {
	bool onscreen = false;
	public CameraMovement camMov;
	string location;
	public GameObject player;

	// Use this for initialization
	void Start () {
		location = (camMov.locX + "." + camMov.locY);
		Debug.Log (location);
	}
	/* This code adds motion to the slime enemy if they originally spawned on the same screen as the player is currently on. */
	// Update is called once per frame
	void Update () {
		if(location == (camMov.locX + "." + camMov.locY))
		{
			//Debug.Log ("nearby");
			//Debug.Log (camMov.locX + "." + camMov.locY);
			//Debug.Log (location);
			if(player.transform.position.x > this.transform.position.x)
			{
				this.transform.Translate(new Vector2(0.025f, 0));
			}
			if(player.transform.position.x < this.transform.position.x)
			{
				this.transform.Translate(new Vector2(-0.025f, 0));
			}
			if(player.transform.position.y > this.transform.position.y)
			{
				this.transform.Translate(new Vector2(0, 0.025f));
			}
			if(player.transform.position.y < this.transform.position.y)
			{
				this.transform.Translate(new Vector2(0, -0.025f));
			}
		}
	}
}
