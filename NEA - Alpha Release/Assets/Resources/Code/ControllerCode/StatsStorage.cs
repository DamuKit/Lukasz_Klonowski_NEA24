using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsStorage : MonoBehaviour {
	public AudioSource m_audio;
	int musicstate;
	public int Difficulty;
	public int localDifficulty;
	public int points;
	public int room;
	public int seed;
	public int seedoffset;
	public float gameSpeed;
	public List<string> Locations = new List<string>();
	public List<int> LocationID = new List<int>();
	public List<GameObject> RoomID = new List<GameObject>();
	public List<GameObject> EnemyID = new List<GameObject> ();
	public List<GameObject> ItemID = new List<GameObject> ();
	public List<float> RandomValues = new List<float> ();
	public int enemystatpoints;
	int refreshSeed;
	public bool killall;
	public float score;
	public int pause;
	public int menu;
	public bool holding;
	public int stackLimit;
	public int FishingState;
	public int kills;
	public float LifetimeDamage;
	/* array listing enemy id, raw probability, points used, raw hp, damage, speed*/
	public int[,] Enemies = new int[,] {{0,60/*50*/,20,5,10,3},{1,75/*75*/,45,7,15,5},{2,90/*100*/,90,30,5,2},{3,0,3,10,15,2},{4,95,5,35,10,1},{5,100,5,15,25,1},{99999,999,999,999,999,999}};
	/* array listing room id, number of spawners, path location x4(n, e, s, w), biome, edgetypes(n, e, s, w)  */
	public int[,] Rooms = new int[,] {
		{000,02,11102,10505,11102,10702,0,1,2,1,2},
		{001,02,11102,10016,11102,10016,0,1,2,1,2},
		{002,03,11102,00000,11102,00000,0,1,0,1,0},
		{003,03,11102,10702,11102,00000,0,1,2,1,0},
		{004,07,11102,10702,11102,10702,0,1,1,1,1},
		{005,03,00000,00000,11102,00000,0,0,0,1,0},
		{006,02,11102,10702,11102,10505,0,1,2,1,2},
		{007,02,11102,00000,00000,10702,0,1,0,0,1},
		{008,02,00000,10702,11102,00000,0,0,1,1,0},
		{009,01,00000,10702,00000,10702,0,0,1,0,1},
		{010,01,11102,00000,00000,00000,0,1,0,0,0},
		{011,01,11003,10702,11102,10702,0,2,1,1,2},
		{012,01,11003,10702,11003,10702,0,2,1,2,1},
		{013,00,00000,00000,00000,11002,0,0,0,0,1},
		{014,00,00000,11102,11102,00000,0,0,1,1,0}
	};
	public string[,] Achievements = new string[,] {
		{"Kill an enemy","","F"},
		{"Go Fish","","F"},
		{"Turn Based Combat","","F"},
		{"Seeded Run","","F"},
		{"OP used","","F"},
		{"Hidden Achievement?","spammed the textbox with commands","F"},
		{"Forever Alone (type a non command into the textbox)","","F"},
		{"Environmentalist","","F"},
		{"Kill a bunch more of enemies","","F"},
		{"obtain 1 Billion Score","","F"},
		{"win /gamble 5 consecutive times","","F"},
		{"","",""},
		{"","",""},
		{"","",""}
	};

	/* array listing item IDs, item chance*/
	public int[,] Items = new int[,] {{26,1500,0},{1,1700,0},{2,1800,0},{3,1900,0},{4,2100,0},{5,0000,0}};

	public float Master;
	public float Music;
	public float SFX;
	// Use this for initialization
	void Start () {
		LifetimeDamage = 0;
		kills = 0;
		FishingState = 0;
		Master = 0.5f;
		Music = 1;
		SFX = 1;
		musicstate = 1;
		m_audio = this.gameObject.GetComponent<AudioSource> ();
		m_audio.volume = 0;
		m_audio.loop = true;
		m_audio.clip = Resources.Load<AudioClip> ("Audio/music/Theme2");
		m_audio.Play ();
		menu = 1;
		stackLimit = 68;
		gameSpeed = 1;
		killall = false;
		seed = Random.Range(0,99999999);
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
		GameObject.Find ("CurrentSeed").GetComponent<TMP_Text>().SetText(seed.ToString("D8"));

	}

	// Update is called once per frame
	void Update () {



		if (score > 1000) {
			Achievements[9,2] = "T";
		}
		if (kills > 0) {
			Achievements[0,2] = "T";
		}
		if (kills >= 100) {
			Achievements[8,2] = "T";
		}

		m_audio.volume = Master * Music;

		if (refreshSeed != seed) {
			refreshSeed = seed;
			GameObject.Find ("CurrentSeed").GetComponent<TMP_Text>().SetText(seed.ToString("D8"));
			Random.InitState (seed);
			RandomValues.Clear();
		}
		if (RandomValues.Count < 10) {
			RandomValues.Add (Random.value);
			Debug.Log (RandomValues [RandomValues.Count - 1] + " " + (RandomValues.Count - 1));
		}

		if (musicstate == 0 & GameObject.Find ("Enemies").transform.childCount > 0) {
			m_audio.Stop ();
			musicstate = 1;
			if (RandomValues [0] < 0.5) {
				m_audio.clip = Resources.Load<AudioClip> ("Audio/music/BattleTheme1");
			} else {
				m_audio.clip = Resources.Load<AudioClip> ("Audio/music/BattleTheme2");
			}
			m_audio.clip = Resources.Load<AudioClip> ("Audio/music/BattleTheme2");
			m_audio.Play ();
		}
		else if(musicstate == 1 & GameObject.Find ("Enemies").transform.childCount == 0){
			m_audio.Stop ();
			musicstate = 0;
			if (RandomValues [0] < 0.5) {
				m_audio.clip = Resources.Load<AudioClip> ("Audio/music/Theme1");
			} else {
				m_audio.clip = Resources.Load<AudioClip> ("Audio/music/Theme2");
			}

			m_audio.Play ();
		}
	}

	public void updateSeed(){
		//try{
		if(GameObject.Find ("SeedInput").GetComponent<TMPro.TMP_InputField> ().text.Length > 0){
			seed = int.Parse(GameObject.Find ("SeedInput").GetComponent<TMPro.TMP_InputField> ().text);
			Achievements[3,2] = "T";
			GameObject.Find ("CurrentSeed").GetComponent<TMP_Text>().SetText(seed.ToString("D8"));
			GameObject.Find ("SeedInput").GetComponent<TMPro.TMP_InputField> ().text = "";
		}
	}
}