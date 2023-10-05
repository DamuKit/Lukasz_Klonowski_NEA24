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

	// Use this for initialization
	void Start () {
		seed = 51;
		Random.InitState (seed);
		Debug.Log (Random.value);
		Debug.Log (Random.value);
		Debug.Log (Random.value);
		l
	}

	// Update is called once per frame
	void Update () {
		
	}
}
