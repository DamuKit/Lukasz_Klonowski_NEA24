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
	// Use this for initialization
	void Start () {
		MainHandPosition = 0;
		OffHandPosition = 1;
		SlotHolder = 0;
		location = 0;
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		camMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement> ();

		for (int x = 0; x < 83; x++) {
			Locations.Add ("0000000000");

		}
		for (int x = 0; x < 83; x++) {
			PlaceHolder.Add ("0000000000");

		}
		//PlaceHolder = Locations;

		//items.Enqueue("100N0008");
		//items.Enqueue("000001");
		//items.Enqueue("001068");
		//items.Enqueue("002068");
		//items.Enqueue("003999");
		//items.Enqueue("004068");

		//items.Enqueue ("100N0007");
		//items.Enqueue ("101N0000");

		items.Enqueue ("102N0000");
		items.Enqueue ("103N0000");
		items.Enqueue ("001034");
		items.Enqueue ("002034");
		items.Enqueue ("003034");
		items.Enqueue ("004034");
		swapPosition = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (items.Count > 0 & stats.pause == 0 & stats.menu == 1) {
			placeHolder2.Clear();
			placeHolder2 = new List<string>(Locations);
			location = -1;
			Debug.Log (items.Peek ());
			try{
				if(items.Peek ().Substring (3, 1) != "N"){
					if(int.Parse(items.Peek().Substring(3,3)) > stats.stackLimit){
						items.Enqueue(items.Peek().Substring(0,3) + (stats.stackLimit + 1000).ToString().Substring(1,3));
						items.Enqueue(items.Peek().Substring(0,3) + (int.Parse(items.Peek().Substring(3,3)) - stats.stackLimit + 1000).ToString().Substring(1,3));
						items.Dequeue();
					}
					Debug.Log("E");
					//location = Locations.FindIndex (a => (a.Substring (0, 3) == items.Peek ().Substring (0, 3) & int.Parse(a.Substring(3,3)) < stats.stackLimit));

					//stack.AddRange(Locations.FindAll(a => a.Substring (0, 3) == items.Peek ().Substring (0, 3)));
					//stack.AddRange(IEnumerable<string>(Locations.FindAll(a => a.Substring (0, 3) == items.Peek ().Substring (0, 3))));
					try{
						Debug.Log(Locations.Count);
						Debug.Log(placeHolder2.Count);
						for(int x = 0;x<=77;x++){
							try{
								if(placeHolder2[x].Substring(0,3) == items.Peek().Substring(0,3) & int.Parse(placeHolder2[x].Substring(3,3)) >= stats.stackLimit){
									placeHolder2[x] = "0000000000";
									Debug.Log("---------------------");
								}
								if(placeHolder2[x].Substring(0,3) == items.Peek().Substring(0,3) & int.Parse(placeHolder2[x].Substring(3,3)) < stats.stackLimit){
									location = x;
									Debug.Log("---------------------");
									break;
								}
							}
							catch{
							}

						}
					}
					catch{
						Debug.Log(items.Peek());
						Debug.Log("Error");
						Debug.Log(placeHolder2.FindIndex(a => a.Substring (0, 3) == items.Peek ().Substring (0, 3)));
					}

					Debug.Log(placeHolder2.Count);
					Debug.Log(location);
					if(location ==-1){
					Debug.Log(placeHolder2.FindIndex(a => a.Substring (0, 3) == items.Peek ().Substring (0, 3)));
					location = placeHolder2.FindIndex(a => a.Substring (0, 3) == items.Peek ().Substring (0, 3));
					}
					Debug.Log(location);
					Debug.Log("EE");
					if(int.Parse(items.Peek ().Substring (3, 3)) + int.Parse(Locations[location].Substring(3,3)) >= stats.stackLimit){
						if(int.Parse(items.Peek ().Substring (3, 3)) + int.Parse(Locations[location].Substring(3,3)) > stats.stackLimit){
							items.Enqueue(items.Peek().Substring(0,3) + (int.Parse(items.Peek ().Substring (3, 3)) + int.Parse(Locations[location].Substring(3,3)) + 2000 - stats.stackLimit).ToString().Substring(1,3));
						}
						Locations [location] = items.Peek().Substring(0,3) + (1000 + stats.stackLimit).ToString().Substring(1,3);

					}
					else{
						Locations [location] = items.Peek().Substring(0,3) + (int.Parse(items.Peek ().Substring (3, 3)) + int.Parse(Locations[location].Substring(3,3)) + 1000).ToString().Substring(1,3);
					}


					Debug.Log(Locations [location]);
					PlaceHolder[location] = Locations[location];
					swapPosition = location;
				}
				else{
					Locations [Locations.FindIndex (a => a == "0000000000")] = items.Peek ();
					Debug.Log("fail");
				}
			}
			catch{
				Locations [Locations.FindIndex (a => a == "0000000000")] = items.Peek ();
				Debug.Log("fail");
			}
			location = 0;
			items.Dequeue ();

		}
		if (Input.GetKeyDown (KeyCode.F) == true) {
			SlotHolder = MainHandPosition;
			MainHandPosition = OffHandPosition;
			OffHandPosition = SlotHolder;
		}
		{
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
			/*if(Input.mouseScrollDelta.y <= -1){
				if (MainHandPosition == 0) {
					MainHandPosition = 5;
				} else {
					MainHandPosition--;
				}
			}
			if(Input.mouseScrollDelta.y >= 1){
				if (MainHandPosition == 5) {
					MainHandPosition = 0;
				} else {
					MainHandPosition++;
				}
			}*/
		}
		//Debug.Log (Locations.FindAll (a => a != "0000000000").Count);
		//Debug.Log (this.gameObject.transform.childCount - 1);
		if (Locations.FindAll (a => a != "0000000000").Count > this.gameObject.transform.childCount - 1) {
			Debug.Log (Locations.FindAll (a => a != "0000000000").Count);
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
