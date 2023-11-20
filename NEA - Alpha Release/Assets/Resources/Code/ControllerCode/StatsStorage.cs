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
	public List<float> RandomValues = new List<float> ();
	public int enemystatpoints;
	int refreshSeed;
	public bool killall;

	/* array listing enemy id, raw probability, points used, raw hp, damage, speed*/
	public int[,] Enemies = new int[,] {{0,50,20,5,10,3},{1,100,45,7,15,5},{99999,999,999,999,999,999}};
	/* array listing room id, number of spawners, path location x4(n, e, s, w), biome, edgetypes(n, e, s, w)  */
	public int[,] Rooms = new int[,] {
		{0,2,11102,10505,11102,10702,0,1,2,1,2},
		{1,2,11102,10016,11102,10016,0,1,2,1,2},
		{2,3,11102,0,11102,0,0,1,0,1,0},
		{3,3,11102,10702,11102,0,0,1,2,1,0},
		{4,14,11102,10702,11102,10702,0,1,1,1,1},
		{5,3,0,0,11102,0,0,0,0,1,0},
		{6,2,11102,10702,11102,10505,0,1,2,1,2},
		{7,2,11102,0,0,10702,0,1,0,0,1},
		{8,2,0,10702,11102,0,0,0,1,1,0},
		{9,1,0,10702,0,10702,0,0,1,0,1},
		{10,1,11102,0,0,0,0,1,0,0,0},
		{11,1,11003,10702,11102,10702,0,2,1,1,2},
		{12,1,11003,10702,11003,10702,0,2,1,2,1},
		{13,0,0,0,0,11002,0,0,0,0,1},
		{14,0,0,11102,11102,0,0,0,1,1,0}
	};
	/* array listing item IDs, item chance*/
	public int[,] Items = new int[,] {{0,1500,0},{1,1700,0},{2,1800,0},{3,1900,0},{4,1900,0}};

	public int[,] Inventory = new int[,] {{0,0},{1,0},{2,0},{3,0},{4,0},{5,0},{6,0},{7,0},{8,0},{9,0}};

	// Use this for initialization
	void Start () {
		killall = false;
		seed = 0;
		//Rooms.Add(Resources.Load("Prefabs/Room/Room_" + "1") as GameObject);
		RoomID.AddRange(Resources.LoadAll<GameObject>("Prefabs/Room"));
		EnemyID.AddRange(Resources.LoadAll<GameObject>("Prefabs/Enemies"));
		ItemID.AddRange(Resources.LoadAll<GameObject>("Prefabs/Items"));
		//Rooms.Add(Resources.Load("Prefabs/Room/Room_" + "2") as GameObject);
		//Object[] subListObjects = Resources.LoadAll("Assets/Prefabs/Room", typeof(GameObject));
		room = 0;
		Difficulty = 1;
		points = 100;

		localDifficulty = 0;
		//Debug.Log(RoomID.Count);
		//Object.Instantiate (Rooms[0], this.gameObject.transform.position + new Vector3 (camMov.locX * 24, camMov.locX * 16), Quaternion.identity, Tilemaps.transform);
		//Debug.Log (EnemyID.Count);
		Locations.Add ("0.0");
		Locations.Add ("1.0");
		LocationID.Add (Rooms[Rooms.GetLength (0)-1, 0]);
		LocationID.Add (Rooms[Rooms.GetLength (0)-2, 0]);

		enemystatpoints = 0;
		refreshSeed = seed;
		Random.InitState (seed);

	}

	// Update is called once per frame
	void Update () {
		if (refreshSeed != seed) {
			refreshSeed = seed;
			Random.InitState (seed);
			RandomValues.Clear();
		}
		if (RandomValues.Count < 10) {
			RandomValues.Add (Random.value);
			Debug.Log (RandomValues [RandomValues.Count - 1] + " " + (RandomValues.Count - 1));
		}
	}
}