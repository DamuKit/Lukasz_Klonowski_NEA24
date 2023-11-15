using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsStorage : MonoBehaviour {
	public int Difficulty;
	public int localDifficulty;
	public int points;
	public int room;
	public int seed;
	public int seedoffset;
	public List<string> Locations = new List<string>();
	public List<int> LocationID = new List<int>();
	public List<GameObject> RoomID = new List<GameObject>();
	public List<GameObject> EnemyID = new List<GameObject> ();
	public List<GameObject> ItemID = new List<GameObject> ();
	public int enemystatpoints;

	/* array listing enemy id, raw probability, points used, raw hp, damage, speed*/
	public int[,] Enemies = new int[,] {{0,50,20,5,10,3},{1,100,45,7,15,5},{99999,999,999,999,999,999}};
	/* array listing room id, number of spawners, path location x4(n, e, s, w), biome, edgetypes(n, e, s, w)  */
	public int[,] Rooms = new int[,] {
		{0,2,11102,10505,11102,10702,0,1,2,1,2},
		{1,2,11102,10012,11102,10016,0,1,2,1,2},
		{2,3,11102,0,11102,0,0,1,0,1,0},
		{3,3,11102,10702,11102,0,0,1,2,1,0},
		{4,14,11102,10702,11102,10702,0,1,1,1,1},
		{5,0,0,0,0,11002,0,0,0,0,1},
		{6,0,0,11002,11102,0,0,0,1,1,0}
	};
	/* array listing item IDs, item chance*/
	public int[,] Items = new int[,] {{0,1500,0},{1,1700,0},{2,1800,0},{3,1900,0},{4,1900,0}};

	public int[,] Inventory = new int[,] {{0,0},{1,0},{2,0},{3,0},{4,0},{5,0},{6,0},{7,0},{8,0},{9,0}};

	// Use this for initialization
	void Start () {
		//Rooms.Add(Resources.Load("Prefabs/Room/Room_" + "1") as GameObject);
		RoomID.AddRange(Resources.LoadAll<GameObject>("Prefabs/Room"));
		EnemyID.AddRange(Resources.LoadAll<GameObject>("Prefabs/Enemies"));
		ItemID.AddRange(Resources.LoadAll<GameObject>("Prefabs/Items"));
		//Rooms.Add(Resources.Load("Prefabs/Room/Room_" + "2") as GameObject);
		//Object[] subListObjects = Resources.LoadAll("Assets/Prefabs/Room", typeof(GameObject));
		room = 0;
		Difficulty = 1;
		points = 100;
		seed = 0;
		localDifficulty = 0;
		Debug.Log(RoomID.Count);
		//Object.Instantiate (Rooms[0], this.gameObject.transform.position + new Vector3 (camMov.locX * 24, camMov.locX * 16), Quaternion.identity, Tilemaps.transform);
		Debug.Log (EnemyID.Count);
		Locations.Add ("0.0");
		Locations.Add ("1.0");
		LocationID.Add (5);
		LocationID.Add (6);
		enemystatpoints = 0;

	}

	// Update is called once per frame
	void Update () {

	}
}