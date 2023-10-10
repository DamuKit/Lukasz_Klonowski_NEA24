﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour {
	public StatsStorage stats;
	private GameObject Tilemaps;
	public string roomlocation;
	public int room;
	//public GameObject Cam;
	public CameraMovement camMov;
	// Use this for initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		Tilemaps = GameObject.Find("Tilemaps");
		//Cam = GameObject.Find ("Main Camera");
		camMov = GameObject.Find ("Main Camera").GetComponent<CameraMovement> ();
		Random.InitState (stats.seed);
		room = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//roomlocation = camMov.locX.ToString() + "." + camMov.locY.ToString();
		roomlocation = (camMov.locX + "." + camMov.locY);


		//Debug.Log (Locations.FindIndex (a => a == roomlocation));
		if (stats.Locations.FindIndex(a => a == roomlocation) != -1) {
			//Debug.Log ("e");
			stats.room = stats.Locations.FindIndex(a => a == roomlocation);
		} else {
			stats.Locations.Add (roomlocation);
			room = Mathf.RoundToInt ((stats.RoomID.Count - 1) * Random.value - 0.5f);
			Debug.Log (room);
			Object.Instantiate (stats.RoomID[room], new Vector3 (camMov.locX * 24, camMov.locY * 16), Quaternion.identity, Tilemaps.transform);
			Debug.Log (camMov.locX * 24 + " , " + camMov.locY * 16);
			//Debug.Log (Locations [Locations.Count - 1]);
			//Debug.Log (Locations.Count - 1);
			//Object.Instantiate(
		}

		
	}
}
