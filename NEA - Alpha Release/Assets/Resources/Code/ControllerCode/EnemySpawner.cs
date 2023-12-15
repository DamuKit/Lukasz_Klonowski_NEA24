/*Created: Sprint 3 - Last Edited Sprint 3
Purpose: This script manages the spawning of enemies. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public int limiter;
	public int dedicatedPoints;
	float rand;
	string roomlocation;
	public CameraMovement camMov;
	public GameObject Cam;
	public StatsStorage stats;
	public GameObject Slime;
	int loops;
	private GameObject Enemies;
	public RoomLoader roomLoader;

	// Use this for initialization
	void Start () {
		Enemies = GameObject.Find("Enemies");
		Cam = GameObject.FindGameObjectWithTag("MainCamera");
		camMov = Cam.GetComponent<CameraMovement> ();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		roomLoader = GameObject.Find ("PassiveCodeController").GetComponent<RoomLoader> ();
		Random.InitState (stats.seed + stats.seedoffset);
		stats.seedoffset += 1;
		//Debug.Log (Random.value);
		//Debug.Log (Random.value);
		//Debug.Log (Random.value);
		roomlocation = camMov.locX.ToString() + "." + camMov.locY.ToString();
		limiter = Mathf.RoundToInt((0.05f * stats.room + 3 * stats.Difficulty + 0.1f * stats.localDifficulty) * stats.points);
		loops = 0;
		stats.enemystatpoints = 0;
		//points = 0.5 * room;
		//Locations.Add ("0.0");
	}

	// Update is called once per frame
	void Update () {
		limiter = Mathf.RoundToInt(((0.1f * stats.room + 2 * stats.Difficulty + 0.5f * stats.localDifficulty) * stats.points) / stats.Rooms[roomLoader.room, 1] - stats.enemystatpoints);
		//Debug.Log (stats.enemystatpoints);
		stats.enemystatpoints = 0;
		//Debug.Log (limiter);
		//Debug.Log (stats.Rooms [roomLoader.room, 1]);
		rand = Random.value;
		//roomlocation = camMov.locX.ToString() + "." + camMov.locY.ToString();
		/*Debug.Log (Locations.FindIndex (a => a == roomlocation));
		if (Locations.FindIndex(a => a == roomlocation) != -1) {
			Debug.Log ("e");
		} else {if
			Locations.Add (roomlocation);
			Debug.Log (Locations [Locations.Count - 1]);
			Debug.Log (Locations.Count - 1);

		}			*/
		if(stats.localDifficulty < 10000){
			if (dedicatedPoints >= limiter) {
				Destroy (this.gameObject);
			}
			summon ();
		}
	}
	private void summon(){
		if (rand < stats.Enemies[loops, 1]*0.01) {
			if (loops < 3) {
				//Debug.Log (stats.EnemyID.Count);
				//Debug.Log (loops);
				Object.Instantiate (stats.EnemyID[loops], this.gameObject.transform.position + new Vector3 (Mathf.Sin (rand), Mathf.Cos (rand)), Quaternion.identity, Enemies.transform);
				dedicatedPoints += 20;

			} 
			loops = 0;
			//Debug.Log ("A");
		} else {
			loops += 1;
			summon ();
			//Debug.Log ("E");

		}
	}
}
