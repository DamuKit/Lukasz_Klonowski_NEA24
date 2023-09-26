using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {
	public CameraMovement camMov;
	public PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.SetPositionAndRotation(new Vector3 ((Input.mousePosition.x - (Display.main.systemWidth / 2)) / Display.main.systemWidth * playerMovement.camerasizex * 2, (Input.mousePosition.y - (Display.main.systemHeight / 2)) / Display.main.systemHeight * playerMovement.camerasizey * 2, 0), Quaternion.identity);
		this.gameObject.transform.Rotate (0, 0, 135);
	}
}
