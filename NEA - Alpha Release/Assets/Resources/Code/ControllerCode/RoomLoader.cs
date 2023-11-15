﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour {
	public StatsStorage stats;
	private GameObject Tilemaps;
	public string roomlocation;
	public int lastRoom;
	//public GameObject Cam;
	public CameraMovement camMov;
	public int room;
	public int refreshSeed;
	public int[,] Testing = new int[,] {{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0}};
	// Use this for initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		Tilemaps = GameObject.Find ("Tilemaps");
		//Cam = GameObject.Find ("Main Camera");
		camMov = GameObject.Find ("Main Camera").GetComponent<CameraMovement> ();
		Random.InitState (stats.seed);
		refreshSeed = stats.seed;
		room = 0;
		lastRoom = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if (refreshSeed != stats.seed) {
			refreshSeed = stats.seed;
			Random.InitState (stats.seed);
		}
		//roomlocation = camMov.locX.ToString() + "." + camMov.locY.ToString();
		roomlocation = (camMov.locX + "." + camMov.locY);


		//Debug.Log (Locations.FindIndex (a => a == roomlocation));
		if (stats.Locations.FindIndex(a => a == roomlocation) != -1) {
			//Debug.Log ("e");
			stats.room = stats.Locations.FindIndex(a => a == roomlocation);
		} else {
			stats.Locations.Add (roomlocation);
			//---------------------------------------------------------------------------------
			/*
			//make code get values of adjacent locations and decide what room to place based on  it.
			Testing [0, 1] = 0;



			*/
			//---------------------------------------------------------------------------------

			room = Mathf.RoundToInt ((stats.RoomID.Count - 1) * Random.value - 0.5f);
			Debug.Log (room);
			if (room == lastRoom) {
				room -= 1;
				if (room == -1){
					room = 2;
				}
			}
			Debug.Log (room);
			stats.LocationID.Add (room);

			Object.Instantiate (stats.RoomID[room], new Vector3 (camMov.locX * 24, camMov.locY * 16), Quaternion.identity, Tilemaps.transform);
			Debug.Log (camMov.locX * 24 + " , " + camMov.locY * 16);
			//Debug.Log (Locations [Locations.Count - 1]);
			//Debug.Log (Locations.Count - 1);
			//Object.Instantiate(
			lastRoom = room;
		}

		
	}
}
