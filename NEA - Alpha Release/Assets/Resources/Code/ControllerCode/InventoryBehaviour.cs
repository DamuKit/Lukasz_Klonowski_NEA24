/*Created: Sprint 7 - Last Edited Sprint 8
This script’s purpose is to manage items being obtained, causing them to enter the player’s inventory. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehaviour : MonoBehaviour {
	public CameraMovement camMov;
	public List<string> Locations = new List<string>();
	public List<string> PlaceHolder = new List<string> ();
	public StatsStorage stats;
	public int swapPosition;
	public Queue<string> items = new Queue<string>();
	int location;
	public List<string> placeHolder2 = new List<string> ();
	public int MainHandPosition;
	public int OffHandPosition;
	public int SlotHolder;

	// initialization
	void Start () {
		MainHandPosition = 0;
		OffHandPosition = 1;
		SlotHolder = 0;
		location = 0;
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		camMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement> ();
		// Fill inventory & placeholder with empty data
		for (int x = 0; x < 83; x++) {
			Locations.Add ("0000000000");
		}
		for (int x = 0; x < 83; x++) {
			PlaceHolder.Add ("0000000000");
		}
		swapPosition = 0;
	}
	
	// Update per frame
	void Update () {
		if (items.Count > 0 & stats.pause == 0 & stats.menu == 1) {
			// Reset
			placeHolder2.Clear();
			placeHolder2 = new List<string>(Locations);
			location = -1;
			try{
				// Check Item Type
				if(items.Peek ().Substring (3, 1) != "N"){
					// Check if an item needs to be split due to being more than a stack
					if(int.Parse(items.Peek().Substring(3,3)) > stats.stackLimit){
						items.Enqueue(items.Peek().Substring(0,3) + (stats.stackLimit + 1000).ToString().Substring(1,3));
						items.Enqueue(items.Peek().Substring(0,3) + (int.Parse(items.Peek().Substring(3,3)) - stats.stackLimit + 1000).ToString().Substring(1,3));
						items.Dequeue();
					}
					try{
						// Identify incomplete item stack
						for(int x = 0;x<=77;x++){
							try{
								if(placeHolder2[x].Substring(0,3) == items.Peek().Substring(0,3) & int.Parse(placeHolder2[x].Substring(3,3)) >= stats.stackLimit){
									placeHolder2[x] = "0000000000";
								}
								if(placeHolder2[x].Substring(0,3) == items.Peek().Substring(0,3) & int.Parse(placeHolder2[x].Substring(3,3)) < stats.stackLimit){
									location = x;
									break;
								}
							}
							catch{
							}

						}
					}
					catch{
					}
					// Default to first empty slot
					if(location ==-1){
					location = placeHolder2.FindIndex(a => a.Substring (0, 3) == items.Peek ().Substring (0, 3));
					}
					// Identify if combining the stack with the new amount overflows the stack & split appropriately
					if(int.Parse(items.Peek ().Substring (3, 3)) + int.Parse(Locations[location].Substring(3,3)) >= stats.stackLimit){
						if(int.Parse(items.Peek ().Substring (3, 3)) + int.Parse(Locations[location].Substring(3,3)) > stats.stackLimit){
							items.Enqueue(items.Peek().Substring(0,3) + (int.Parse(items.Peek ().Substring (3, 3)) + int.Parse(Locations[location].Substring(3,3)) + 2000 - stats.stackLimit).ToString().Substring(1,3));
						}
						Locations [location] = items.Peek().Substring(0,3) + (1000 + stats.stackLimit).ToString().Substring(1,3);

					}
					else{
						// No overflow
						Locations [location] = items.Peek().Substring(0,3) + (int.Parse(items.Peek ().Substring (3, 3)) + int.Parse(Locations[location].Substring(3,3)) + 1000).ToString().Substring(1,3);
					}
					PlaceHolder[location] = Locations[location];
					swapPosition = location;
				}
				else{
					// create item if non-stackable
					Locations [Locations.FindIndex (a => a == "0000000000")] = items.Peek ();
				}
			}
			catch{
				Locations [Locations.FindIndex (a => a == "0000000000")] = items.Peek ();
			}
			location = 0;
			items.Dequeue ();

		}
		// Swap hand slots
		if (Input.GetKeyDown (KeyCode.F) == true) {
			SlotHolder = MainHandPosition;
			MainHandPosition = OffHandPosition;
			OffHandPosition = SlotHolder;
		}
		// scroll/cycle through inventory
		if (Input.GetKeyDown (KeyCode.Q) == true | Input.mouseScrollDelta.y >= 1) {
			if (MainHandPosition == 0) {
				MainHandPosition = 5;
			} else {
				MainHandPosition--;
			}
			if (MainHandPosition == OffHandPosition) {
				if (MainHandPosition == 0) {
					MainHandPosition = 5;
				} else {
					MainHandPosition--;
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.E) == true | Input.mouseScrollDelta.y <= -1) {
			if (MainHandPosition == 5) {
				MainHandPosition = 0;
			} else {
				MainHandPosition++;
			}
			if (MainHandPosition == OffHandPosition) {
				if (MainHandPosition == 5) {
					MainHandPosition = 0;
				} else {
					MainHandPosition++;
				}
			}
		}
		// switch hand slot to specific slot based on number input
		if (Input.GetKeyDown (KeyCode.Alpha1) == true) {
			if (OffHandPosition != 0) {
				MainHandPosition = 0;
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha2) == true) {
			if (OffHandPosition != 1) {
				MainHandPosition = 1;
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha3) == true) {
			if (OffHandPosition != 2) {
				MainHandPosition = 2;
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha4) == true) {
			if (OffHandPosition != 3) {
				MainHandPosition = 3;
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha5) == true) {
			if (OffHandPosition != 4) {
				MainHandPosition = 4;
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha6) == true) {
			if (OffHandPosition != 5) {
				MainHandPosition = 5;
			}
		}
		// Check that the appropriate number of items exists, creating new ones if necessary for slots where an item should be
		if (Locations.FindAll (a => a != "0000000000").Count > this.gameObject.transform.childCount - 1) {
			for (int x = 0; x < 77; x++) {
				if(Locations[x] != PlaceHolder[x]){
					Instantiate(Resources.Load<GameObject>("Prefabs/UI/item"),new Vector2(Mathf.RoundToInt(x % 11)-11 + camMov.locX * 24 , (Mathf.RoundToInt(x / 11) - 4)*-1 + camMov.locY * 16 + 1000 * stats.pause),Quaternion.identity, this.gameObject.transform);
					PlaceHolder[x] = Locations[x];
					break;
				}
				if (Locations [x].Substring (0, 3) == "000" & Locations [x].Length < 10) {
					Locations[x] = "0000000000";
				}
			}
		}
	}
}
