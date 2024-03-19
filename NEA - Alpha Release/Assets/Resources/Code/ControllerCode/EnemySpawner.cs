/*Created: Sprint 3 - Last Edited Sprint 3
This script’s purpose is to manage the spawning of enemies into rooms. */
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
	int loops;
	private GameObject Enemies;
	public RoomLoader roomLoader;

	// initialization
	void Start () {
		Enemies = GameObject.Find("Enemies");
		Cam = GameObject.FindGameObjectWithTag("MainCamera");
		camMov = Cam.GetComponent<CameraMovement> ();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		roomLoader = GameObject.Find ("PassiveCodeController").GetComponent<RoomLoader> ();
		Random.InitState (stats.seed + stats.seedoffset);
		stats.seedoffset += 1;
		roomlocation = camMov.locX.ToString() + "." + camMov.locY.ToString();
		limiter = Mathf.RoundToInt((0.05f * stats.room + 3 * stats.Difficulty + 0.1f * stats.localDifficulty) * stats.points);
		loops = 0;
		stats.enemystatpoints = 0;
	}

	// Update per frame
	void Update () {
		// Determine amount of points to allocate to enemies & spawn
		limiter = Mathf.RoundToInt(((0.1f * stats.room + 2 * stats.Difficulty + 0.5f * stats.localDifficulty) * stats.points) / stats.Rooms[roomLoader.room, 1] - stats.enemystatpoints);
		stats.enemystatpoints = 0;
		rand = Random.value;
		if(stats.localDifficulty < 10000){
			if (dedicatedPoints >= limiter) {
				Destroy (this.gameObject);
			}
			summon ();
		}
	}

	// Randomly generate an enemy
	private void summon(){
		if (rand < stats.Enemies[loops, 1]*0.01) {
			if (loops < 6) {
				Object.Instantiate (stats.EnemyID[loops], this.gameObject.transform.position + new Vector3 (Mathf.Sin (rand), Mathf.Cos (rand)), Quaternion.identity, Enemies.transform);
				dedicatedPoints += 20;

			} 
			loops = 0;
		} else {
			loops += 1;
			summon ();

		}
	}
}
