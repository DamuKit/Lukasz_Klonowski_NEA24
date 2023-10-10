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
	public List<GameObject> Rooms = new List<GameObject>();
	public List<GameObject> EnemyID = new List<GameObject> ();
	string roomlocation;
	public CameraMovement camMov;
	public GameObject Cam;
	private GameObject Tilemaps;
	/* array listing enemy name, raw probability, points used, raw hp, location for use in other code*/
	//*public string[,] Enemies = new string[,] {{"Slime","0.5","20","5"},{"N/A","1","1","1"}};
	public int[,] Enemies = new int[,] {{1,50,20,5,0},{9999,100,1,1,0}};

	// Use this for initialization
	void Start () {
		//Rooms.Add(Resources.Load("Prefabs/Room/Room_" + "1") as GameObject);
		Rooms.AddRange(Resources.LoadAll<GameObject>("Prefabs/Room"));
		EnemyID.AddRange(Resources.LoadAll<GameObject>("Prefabs/Enemies"));
		//Rooms.Add(Resources.Load("Prefabs/Room/Room_" + "2") as GameObject);
		//Object[] subListObjects = Resources.LoadAll("Assets/Prefabs/Room", typeof(GameObject));
		Tilemaps = GameObject.Find("Tilemaps");
		room = 0;
		Difficulty = 1;
		points = 100;
		seed = 7;
		localDifficulty = 0;
		Cam = GameObject.FindGameObjectWithTag("MainCamera");
		camMov = Cam.GetComponent<CameraMovement> ();
		Debug.Log(Rooms.Count);
		//Object.Instantiate (Rooms[0], this.gameObject.transform.position + new Vector3 (camMov.locX * 24, camMov.locX * 16), Quaternion.identity, Tilemaps.transform);
		Debug.Log (EnemyID.Count);
		Locations.Add ("0.0");
		Locations.Add ("1.0");
		Random.InitState (seed);
	}

	// Update is called once per frame
	void Update () {
		
		roomlocation = camMov.locX.ToString() + "." + camMov.locY.ToString();
		//Debug.Log (Locations.FindIndex (a => a == roomlocation));
		if (Locations.FindIndex(a => a == roomlocation) != -1) {
			//Debug.Log ("e");
			room = Locations.FindIndex(a => a == roomlocation);
		} else {
			Locations.Add (roomlocation);
			Object.Instantiate (Rooms[Mathf.RoundToInt((Rooms.Count - 1) * Random.value - 0.5f)], new Vector3 (camMov.locX * 24, camMov.locY * 16), Quaternion.identity, Tilemaps.transform);
			Debug.Log (camMov.locX * 24 + " , " + camMov.locY * 16);
			//Debug.Log (Locations [Locations.Count - 1]);
			//Debug.Log (Locations.Count - 1);
			//Object.Instantiate(
		}
	}
}