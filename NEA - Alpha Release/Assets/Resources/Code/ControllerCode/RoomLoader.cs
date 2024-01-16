using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour {
	public float randomVal;
	public StatsStorage stats;
	private GameObject Tilemaps;
	public string roomlocation;
	public int lastRoom;
	public int placeholder;
	//public GameObject Cam;
	public CameraMovement camMov;
	public int room;
	public int refreshSeed;
	public int[,] Testing = new int[,] {{-1,-1,-1,-1,-1,-1,-1,-1,-1},{-1,-1,-1,-1,-1,-1,-1,-1,-1},{-1,-1,-1,-1,-1,-1,-1,-1,-1},{-1,-1,-1,-1,-1,-1,-1,-1,-1}};
	public List<GameObject> RoomOptions = new List<GameObject>();
	int n;
	public int openings;
	int roomTest;
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
		placeholder = 0;
		openings = 1;
		roomTest = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (refreshSeed != stats.seed) {
			refreshSeed = stats.seed;
			Random.InitState (stats.seed);
			Debug.Log ("E");
		}
		//roomlocation = camMov.locX.ToString() + "." + camMov.locY.ToString();
		roomlocation = (camMov.locX + "." + camMov.locY);


		//Debug.Log (Locations.FindIndex (a => a == roomlocation));
		if (stats.Locations.FindIndex(a => a == roomlocation) != -1) {
			//Debug.Log ("e");
			stats.room = stats.Locations.FindIndex(a => a == roomlocation);
		} else {
			randomVal = stats.RandomValues[0];

			stats.Locations.Add (roomlocation);

			Testing = new int[,] {{-1,-1,-1,-1,-1,-1,-1,-1,-1},{-1,-1,-1,-1,-1,-1,-1,-1,-1},{-1,-1,-1,-1,-1,-1,-1,-1,-1},{-1,-1,-1,-1,-1,-1,-1,-1,-1}};

			//---------------------------------------------------------------------------------
			/*
			//make code get values of adjacent locations and decide what room to place based on  it.
			Testing [0, 1] = 0;
			*/
			if (stats.Locations.FindIndex(a => a == camMov.locX + "." + (camMov.locY + 1)) != -1 ) {
				placeholder = stats.Locations.IndexOf(camMov.locX + "." + (camMov.locY + 1));
				Debug.Log (placeholder);
				Debug.Log (stats.LocationID[placeholder]);

				for (int i = 0; i < 9; i += 1) {
					Testing [0, i] = stats.Rooms[stats.LocationID [placeholder], i +2];
					Debug.Log (Testing [0, i] + " " + i);
				}
			}
			if (stats.Locations.FindIndex(a => a == camMov.locX + "." + (camMov.locY - 1)) != -1 ) {
				placeholder = stats.Locations.IndexOf(camMov.locX + "." + (camMov.locY - 1));
				Debug.Log (placeholder);

				for (int i = 0; i < 9; i += 1) {
					Testing [2, i] = stats.Rooms[stats.LocationID [placeholder], i +2];
					Debug.Log (Testing [2, i] + " " + i);
				}
			}
			if (stats.Locations.FindIndex(a => a == (camMov.locX + 1) + "." + camMov.locY) != -1 ) {
				placeholder = stats.Locations.IndexOf((camMov.locX + 1) + "." + camMov.locY);
				Debug.Log((camMov.locX + 1) + "." + camMov.locY);
				Debug.Log (placeholder);
				Debug.Log (stats.LocationID[placeholder]);

				for (int i = 0; i < 9; i += 1) {
					Testing [1, i] = stats.Rooms[stats.LocationID [placeholder], i +2];
					Debug.Log (Testing [1, i] + " " + i);
				}
			}
			if (stats.Locations.FindIndex(a => a == (camMov.locX - 1) + "." + camMov.locY) != -1 ) {
				placeholder = stats.Locations.IndexOf((camMov.locX - 1) + "." + camMov.locY);
				Debug.Log (placeholder);
				Debug.Log (stats.LocationID[placeholder]);

				for (int i = 0; i < 9; i += 1) {
					Testing [3, i] = stats.Rooms[stats.LocationID [placeholder], i +2];
					Debug.Log (Testing [3, i] + " " + i);
				}
			}
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



			//---------------------------------------------------------------------------------
			/*Code for adding room prefabs to a new list to randomly select from the rooms
			 */
			Debug.Log (stats.RoomID.Count);
			RoomOptions.Clear ();
			RoomOptions.AddRange (stats.RoomID.GetRange(0,stats.RoomID.Count - 1));
			for(int i = RoomOptions.Count-1; i>=0;i-=1){
				if (Testing [0, 2] != -1 & (Testing[0,2] != stats.Rooms[i,2] | Testing[0,7] != stats.Rooms[i,7])){
					RoomOptions.RemoveAt (i);
					Debug.Log ("Removed a : " + i);
					Debug.Log (Testing [0, 2] + " " + stats.Rooms [i, 2] + " " + Testing[0,7] + " " + stats.Rooms[i,7]);
				}
				else if (Testing [1, 3] != -1 & ( Testing[1,3] != stats.Rooms[i,3] | Testing[1,8] != stats.Rooms[i,8])) {
					RoomOptions.RemoveAt (i);
					Debug.Log ("Removed b : " + i);
					Debug.Log (Testing [1, 3] + " " + stats.Rooms [i, 3] + " " + Testing[1,8] + " " + stats.Rooms[i,8]);
				}
				else if (Testing [2, 0] != -1 & (Testing[2,0] != stats.Rooms[i,4] | Testing[2,5] != stats.Rooms[i,9])) {
					RoomOptions.RemoveAt (i);
					Debug.Log ("Removed c : " + i);
					Debug.Log (Testing [2, 0] + " " + stats.Rooms [i, 4] + " " + Testing[2,5] + " " + stats.Rooms[i,9]);
				}
				else if (Testing [3, 1] != -1 & (Testing[3,1] != stats.Rooms[i,5] | Testing[3,6] != stats.Rooms[i,10])) {
					RoomOptions.RemoveAt (i);
					Debug.Log ("Removed d : " + i);
					Debug.Log (Testing [3, 1] + " " + stats.Rooms [i, 5] + " " + Testing[3,6] + " " + stats.Rooms[i,10]);
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

			//---------------------------------------------------------------------------------
			Debug.Log(RoomOptions.Count);


			Debug.Log ("E " + randomVal + " " + stats.seed);
			room = Mathf.RoundToInt ((RoomOptions.Count) * randomVal - 0.5f);
			Debug.Log (room);
			/*if (room == lastRoom & RoomOptions.Count > 1) {
				room -= 1;
				if (room == -1) {
					room = 2;
				}
			}
			*/
			Debug.Log (room);
			stats.RandomValues.RemoveAt (0);
			try{
				Debug.Log (int.Parse(RoomOptions [room].gameObject.name.Substring (1, 3)));
				stats.LocationID.Add (int.Parse(RoomOptions [room].gameObject.name.Substring (1, 3)));
				Object.Instantiate (RoomOptions[room], new Vector3 (camMov.locX * 24, camMov.locY * 16), Quaternion.identity, Tilemaps.transform);
			}
			catch{
				stats.Locations.RemoveAt (stats.Locations.Count - 1);
			}
			//stats.LocationID.Add (int.Parse(RoomOptions [room].gameObject.name.Substring (1, 3)));

			//Object.Instantiate (RoomOptions[room], new Vector3 (camMov.locX * 24, camMov.locY * 16), Quaternion.identity, Tilemaps.transform);
			Debug.Log (camMov.locX * 24 + " , " + camMov.locY * 16);
			//Debug.Log (Locations [Locations.Count - 1]);
			//Debug.Log (Locations.Count - 1);
			//Object.Instantiate(
			lastRoom = room;
		}

		
	}

}