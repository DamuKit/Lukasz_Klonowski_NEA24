/*Created: Sprint 3 - Last Edited Sprint 3
Purpose: This script manages the spawning of enemies. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public int Difficulty;
	public int localDifficulty;
	public int points;
	public int room;
	public int seed;
	string roomlocation;
	public CameraMovement camMov;
	public List<string> Locations = new List<string>();
	public GameObject Cam;


	// Use this for initialization
	void Start () {
		Cam = GameObject.FindGameObjectWithTag("MainCamera");
		camMov = Cam.GetComponent<CameraMovement> ();
		seed = 51;
		Random.InitState (seed);
		Debug.Log (Random.value);
		Debug.Log (Random.value);
		Debug.Log (Random.value);
		//Locations.Add ("0.0");
	}

	// Update is called once per frame
	void Update () {
		roomlocation = camMov.locX.ToString() + "." + camMov.locY.ToString();
		Debug.Log (Locations.FindIndex (a => a == roomlocation));
		if (Locations.FindIndex(a => a == roomlocation) != -1) {
			Debug.Log ("e");
		} else {
			Locations.Add (roomlocation);
			Debug.Log (Locations [Locations.Count - 1]);
			Debug.Log (Locations.Count - 1);
		}			
	}
}
