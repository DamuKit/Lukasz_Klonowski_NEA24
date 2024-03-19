/*Created: Sprint 3 - Last Edited Sprint 8
This script’s purpose is to hold important commonly needed in multiple scripts while also managing most statistics, some achievements, the background music and the seed. */
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
	public float DistanceTravelled;
	public int Fished;
	public int consumed;
	// array listing enemy id, raw probability, points used, raw hp, damage, speed
	public int[,] Enemies = new int[,] {{0,60,20,5,10,3},{1,75,45,7,15,5},{2,90,90,30,5,2},{3,0,3,10,15,2},{4,95,5,35,10,1},{5,100,5,15,25,1},{99999,999,999,999,999,999}};
	// array listing room id, number of spawners, path location x4(n, e, s, w), biome, edgetypes(n, e, s, w)
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
		{"Hidden Achievement","spammed the textbox with commands","F"},
		{"Forever Alone (type a non command into the textbox)","","F"},
		{"Environmentalist","","F"},
		{"Kill a bunch more of enemies","","F"},
		{"obtain 1 Billion Score","","F"},
		{"win /gamble 5 consecutive times","","F"},
		{"Hidden Achievement","Caught the Legendary BlaHaj","F"},
		{"","",""}
	};

	public string[,] Statistic = new string[,] {
		{"Level:","1","0","3"},
		{"Max Health:","0","0","1000"},
		{"Raw Damage:","0","0","20"},
		{"Lifetime Damage:","0","0","200"},
		{"Kills:","0","0","5"},
		{"Distance Travelled:","0","0","200"},
		{"Items consumed:","0","0","5"},
		{"Objects Fished:","0","0","3"},
		{"rooms explored:","0","0","1"},
		{"","","","0"}
	};
	/* array listing item IDs, item chance*/
	public int[,] Items = new int[,] {{26,1500,0},{1,1700,0},{2,1800,0},{3,1900,0},{4,2100,0},{5,0000,0}};
	public float Master;
	public float Music;
	public float SFX;

	// Use this for initialization
	void Start () {
		DistanceTravelled = 0;
		LifetimeDamage = 0;
		kills = 0;
		FishingState = 0;
		Fished = 0;
		consumed = 0;
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
		RoomID.AddRange(Resources.LoadAll<GameObject>("Prefabs/Room"));
		EnemyID.AddRange(Resources.LoadAll<GameObject>("Prefabs/Enemies"));
		ItemID.AddRange(Resources.LoadAll<GameObject>("Prefabs/Items"));
		room = 0;
		Difficulty = 1;
		points = 100;
		localDifficulty = 0;
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
		
		// Constantly update stats
		Statistic [3, 1] = LifetimeDamage.ToString ();
		Statistic [4, 1] = kills.ToString ();
		Statistic [5, 1] = DistanceTravelled.ToString ("F2"); 
		Statistic [6, 1] = consumed.ToString ();
		Statistic [7, 1] = Fished.ToString ();
		Statistic [8, 1] = (Locations.Count - 2).ToString ();
		{
			if (float.Parse (Statistic [0, 1]) >= float.Parse (Statistic [0, 3]) & int.Parse (Statistic [0, 2]) <5) {
				Statistic [0, 2] = (int.Parse (Statistic [0, 2]) + 1).ToString ();
				Statistic [0, 3] = (float.Parse (Statistic [0, 3]) * 3).ToString ();
			}
			if (float.Parse (Statistic [1, 1]) >= float.Parse (Statistic [1, 3]) & int.Parse (Statistic [1, 2]) <5) {
				Statistic [1, 2] = (int.Parse (Statistic [1, 2]) + 1).ToString ();
				Statistic [1, 3] = (float.Parse (Statistic [1, 3]) * 3).ToString ();
			}
			if (float.Parse (Statistic [2, 1]) >= float.Parse (Statistic [2, 3]) & int.Parse (Statistic [2, 2]) <5) {
				Statistic [2, 2] = (int.Parse (Statistic [2, 2]) + 1).ToString ();
				Statistic [2, 3] = (float.Parse (Statistic [2, 3]) * 3).ToString ();
			}
			if (float.Parse (Statistic [3, 1]) >= float.Parse (Statistic [3, 3]) & int.Parse (Statistic [3, 2]) <5) {
				Statistic [3, 2] = (int.Parse (Statistic [3, 2]) + 1).ToString ();
				Statistic [3, 3] = (float.Parse (Statistic [3, 3]) * 5).ToString ();
			}
			if (float.Parse (Statistic [4, 1]) >= float.Parse (Statistic [4, 3]) & int.Parse (Statistic [4, 2]) <5) {
				Statistic [4, 2] = (int.Parse (Statistic [4, 2]) + 1).ToString ();
				Statistic [4, 3] = (float.Parse (Statistic [4, 3]) * 10).ToString ();
			}
			if (float.Parse (Statistic [5, 1]) >= float.Parse (Statistic [5, 3]) & int.Parse (Statistic [5, 2]) <5) {
				Statistic [5, 2] = (int.Parse (Statistic [5, 2]) + 1).ToString ();
				Statistic [5, 3] = (float.Parse (Statistic [5, 3]) * 5).ToString ();
			}
			if (float.Parse (Statistic [6, 1]) >= float.Parse (Statistic [6, 3]) & int.Parse (Statistic [6, 2]) <5) {
				Statistic [6, 2] = (int.Parse (Statistic [6, 2]) + 1).ToString ();
				Statistic [6, 3] = (float.Parse (Statistic [6, 3]) * 3).ToString ();
			}
			if (float.Parse (Statistic [7, 1]) >= float.Parse (Statistic [7, 3]) & int.Parse (Statistic [7, 2]) <5) {
				Statistic [7, 2] = (int.Parse (Statistic [7, 2]) + 1).ToString ();
				Statistic [7, 3] = (float.Parse (Statistic [7, 3]) * 5).ToString ();
			}
			if (float.Parse (Statistic [8, 1]) >= float.Parse (Statistic [8, 3]) & int.Parse (Statistic [8, 2]) <5) {
				Statistic [8, 2] = (int.Parse (Statistic [8, 2]) + 1).ToString ();
				Statistic [8, 3] = (float.Parse (Statistic [8, 3]) * 3).ToString ();
			}
		}
		// Update achievements
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
		// Update seed
		if (refreshSeed != seed) {
			refreshSeed = seed;
			GameObject.Find ("CurrentSeed").GetComponent<TMP_Text>().SetText(seed.ToString("D8"));
			Random.InitState (seed);
			RandomValues.Clear();
		}
		// Create a list of random values
		if (RandomValues.Count < 10) {
			RandomValues.Add (Random.value);
		}
		// Manage background music
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

	// Ensures that the text for the seed in the menu displays the current seed
	public void updateSeed(){
		if(GameObject.Find ("SeedInput").GetComponent<TMPro.TMP_InputField> ().text.Length > 0){
			seed = int.Parse(GameObject.Find ("SeedInput").GetComponent<TMPro.TMP_InputField> ().text);
			Achievements[3,2] = "T";
			GameObject.Find ("CurrentSeed").GetComponent<TMP_Text>().SetText(seed.ToString("D8"));
			GameObject.Find ("SeedInput").GetComponent<TMPro.TMP_InputField> ().text = "";
		}
	}
}