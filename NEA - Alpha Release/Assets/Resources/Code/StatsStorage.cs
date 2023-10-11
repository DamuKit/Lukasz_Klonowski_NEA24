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
	public List<GameObject> RoomID = new List<GameObject>();
	public List<GameObject> EnemyID = new List<GameObject> ();

	/* array listing enemy id, raw probability, points used, raw hp, damage*/
	//public string[,] Enemies = new string[,] {{"Slime","0.5","20","5"},{"N/A","1","1","1"}};
	public int[,] Enemies = new int[,] {{0,50,20,5,10},{9999,100,1,1,0}};
	/* array listing room id, number of spawners */
	public int[,] Rooms = new int[,] {{0, 2},{1, 2},{2,3}};

	// Use this for initialization
	void Start () {
		//Rooms.Add(Resources.Load("Prefabs/Room/Room_" + "1") as GameObject);
		RoomID.AddRange(Resources.LoadAll<GameObject>("Prefabs/Room"));
		EnemyID.AddRange(Resources.LoadAll<GameObject>("Prefabs/Enemies"));
		//Rooms.Add(Resources.Load("Prefabs/Room/Room_" + "2") as GameObject);
		//Object[] subListObjects = Resources.LoadAll("Assets/Prefabs/Room", typeof(GameObject));
		room = 0;
		Difficulty = 1;
		points = 100;
		seed = 1;
		localDifficulty = 0;
		Debug.Log(RoomID.Count);
		//Object.Instantiate (Rooms[0], this.gameObject.transform.position + new Vector3 (camMov.locX * 24, camMov.locX * 16), Quaternion.identity, Tilemaps.transform);
		Debug.Log (EnemyID.Count);
		Locations.Add ("0.0");
		Locations.Add ("1.0");

	}

	// Update is called once per frame
	void Update () {

	}
}