/*Created: Sprint 3 - Last Edited Sprint 8
This script’s purpose is to generate the rooms in a logical way. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour {
	public float randomVal;
	public StatsStorage stats;
	private GameObject Tilemaps;
	public string roomlocation;
	public int placeholder;
	public CameraMovement camMov;
	public int room;
	public int refreshSeed;
	public int[,] Testing = new int[,] {{-1,-1,-1,-1,-1,-1,-1,-1,-1},{-1,-1,-1,-1,-1,-1,-1,-1,-1},{-1,-1,-1,-1,-1,-1,-1,-1,-1},{-1,-1,-1,-1,-1,-1,-1,-1,-1}};
	public List<GameObject> RoomOptions = new List<GameObject>();
	public int openings;
	int roomTest;

	// Initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		Tilemaps = GameObject.Find ("Tilemaps");
		camMov = GameObject.Find ("Main Camera").GetComponent<CameraMovement> ();
		Random.InitState (stats.seed);
		refreshSeed = stats.seed;
		room = 0;
		placeholder = 0;
		openings = 1;
		roomTest = 0;
	}
	
	// Update once per frame
	void Update () {
		// Refresh the seed
		if (refreshSeed != stats.seed) {
			refreshSeed = stats.seed;
			Random.InitState (stats.seed);
		}
		roomlocation = (camMov.locX + "." + camMov.locY);
		if (stats.Locations.FindIndex(a => a == roomlocation) != -1) {
			stats.room = stats.Locations.FindIndex(a => a == roomlocation);
		} else {
			randomVal = stats.RandomValues[0];
			stats.Locations.Add (roomlocation);
			Testing = new int[,] {{-1,-1,-1,-1,-1,-1,-1,-1,-1},{-1,-1,-1,-1,-1,-1,-1,-1,-1},{-1,-1,-1,-1,-1,-1,-1,-1,-1},{-1,-1,-1,-1,-1,-1,-1,-1,-1}};
		
			// make code get values of adjacent locations and decide what room to place based on  it.
			if (stats.Locations.FindIndex(a => a == camMov.locX + "." + (camMov.locY + 1)) != -1 ) {
				placeholder = stats.Locations.IndexOf(camMov.locX + "." + (camMov.locY + 1));
				for (int i = 0; i < 9; i += 1) {
					Testing [0, i] = stats.Rooms[stats.LocationID [placeholder], i +2];
				}
			}
			if (stats.Locations.FindIndex(a => a == camMov.locX + "." + (camMov.locY - 1)) != -1 ) {
				placeholder = stats.Locations.IndexOf(camMov.locX + "." + (camMov.locY - 1));
				for (int i = 0; i < 9; i += 1) {
					Testing [2, i] = stats.Rooms[stats.LocationID [placeholder], i +2];
				}
			}
			if (stats.Locations.FindIndex(a => a == (camMov.locX + 1) + "." + camMov.locY) != -1 ) {
				placeholder = stats.Locations.IndexOf((camMov.locX + 1) + "." + camMov.locY);

				for (int i = 0; i < 9; i += 1) {
					Testing [1, i] = stats.Rooms[stats.LocationID [placeholder], i +2];
				}
			}
			if (stats.Locations.FindIndex(a => a == (camMov.locX - 1) + "." + camMov.locY) != -1 ) {
				placeholder = stats.Locations.IndexOf((camMov.locX - 1) + "." + camMov.locY);

				for (int i = 0; i < 9; i += 1) {
					Testing [3, i] = stats.Rooms[stats.LocationID [placeholder], i +2];
				}
			}
			// Determine the amount openings in the current room to other rooms
			openings = 0;
			if (Testing [0, 7] == 1 | Testing [0, 7] == -1) {
				openings++;
			}
			if (Testing [1, 8] == 1 | Testing [1, 8] == -1) {
				openings++;
			}
			if (Testing [2, 5] == 1 | Testing [2, 5] == -1) {
				openings++;
			}
			if (Testing [3, 6] == 1 | Testing [3, 6] == -1) {
				openings++;
			}

			// Code for adding room prefabs to a new list to randomly select from the rooms
			RoomOptions.Clear ();
			RoomOptions.AddRange (stats.RoomID.GetRange(0,stats.RoomID.Count - 1));
			for(int i = RoomOptions.Count-1; i>=0;i-=1){
				if (Testing [0, 2] != -1 & (Testing[0,2] != stats.Rooms[i,2] | Testing[0,7] != stats.Rooms[i,7])){
					RoomOptions.RemoveAt (i);
				}
				else if (Testing [1, 3] != -1 & ( Testing[1,3] != stats.Rooms[i,3] | Testing[1,8] != stats.Rooms[i,8])) {
					RoomOptions.RemoveAt (i);
				}
				else if (Testing [2, 0] != -1 & (Testing[2,0] != stats.Rooms[i,4] | Testing[2,5] != stats.Rooms[i,9])) {
					RoomOptions.RemoveAt (i);
				}
				else if (Testing [3, 1] != -1 & (Testing[3,1] != stats.Rooms[i,5] | Testing[3,6] != stats.Rooms[i,10])) {
					RoomOptions.RemoveAt (i);
				}
				else if (openings >= 2) {
					roomTest = 0;
					if (stats.Rooms [i, 7] == -1 | stats.Rooms [i, 7] == 1) {
						roomTest += 1;
					}
					if (stats.Rooms [i, 8] == -1 | stats.Rooms [i, 8] == 1) {
						roomTest += 1;
					}
					if (stats.Rooms [i, 9] == -1 | stats.Rooms [i, 9] == 1) {
						roomTest += 1;
					}
					if (stats.Rooms [i, 10] == -1 | stats.Rooms [i, 10] == 1) {
						roomTest += 1;
					}
					if (roomTest <= 1) {
						RoomOptions.RemoveAt (i);
					}
				}
			}
			// Determine randomly which room to generate out of the available options
			room = Mathf.RoundToInt ((RoomOptions.Count) * randomVal - 0.5f);
			stats.RandomValues.RemoveAt (0);
			try{
				stats.LocationID.Add (int.Parse(RoomOptions [room].gameObject.name.Substring (1, 3)));
				Object.Instantiate (RoomOptions[room], new Vector3 (camMov.locX * 24, camMov.locY * 16), Quaternion.identity, Tilemaps.transform);
			}
			catch{
				stats.Locations.RemoveAt (stats.Locations.Count - 1);
			}
		}
	}
}