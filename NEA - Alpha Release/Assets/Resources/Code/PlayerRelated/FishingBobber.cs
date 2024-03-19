/*Created: Sprint 8 - Last Edited Sprint 8
This script’s purpose is to manage the fishing, allowing the player to obtain items when successful. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FishingBobber : MonoBehaviour {
	public bool fishing;
	public int destroy;
	public TilemapBehaviour Water;
	CameraMovement CamMov;
	StatsStorage stats;
	Vector2 origin;
	float duration;
	float fishTime;
	float fish;
	public bool catching;
	PlayerMovement player;
	bool InitialFishCatch;
	InventoryBehaviour invBeh;
	// Use this for initialization
	void Start () {
		invBeh = GameObject.Find("Inventory").GetComponentInParent<InventoryBehaviour> ();
		InitialFishCatch = false;
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		catching = false;
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		CamMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement> ();
		fishing = false;
		destroy = 1;
		StartCoroutine ("Destroy");
		Water = GameObject.Find ("Tilemaps").transform.GetChild (stats.Locations.IndexOf (CamMov.locX + "." + CamMov.locY)).transform.Find ("Walls").GetComponent<TilemapBehaviour>();
		origin = this.gameObject.transform.position;
		fishTime = 0;
		//Debug.Log (GameObject.Find ("Tilemaps").transform.GetChild (stats.Locations.IndexOf (CamMov.locX + "." + CamMov.locY)).transform.Find ("Walls").name);
	}
	
	// Update is called once per frame
	void Update () {
		try{
			if (Water.TilesA.Contains (Water.liquids.GetTile (Water.liquids.WorldToCell(this.transform.position)))) {
				if (fishing == false) {
					destroy -=1;
					fishing = true;
					fishTime = Random.Range(50,150) * 0.1f;
					player.m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/Fishing1"));
				}
			}
			//else{
			//	GameObject.Find ("AttackHitBox").GetComponent<Attacking> ().fishing = false;
			//	Destroy (this.gameObject);
			//}
		}
		catch{
			//GameObject.Find ("AttackHitBox").GetComponent<Attacking> ().fishing = false;
			//Destroy (this.gameObject);
		}
		
		if (destroy == 2) {
			GameObject.Find ("AttackHitBox").GetComponent<Attacking> ().fishing = false;
			Destroy (this.gameObject);
		}
		if (fishing == true) {
			duration += 1 * Time.deltaTime;
			//this.transform.position = origin + new Vector2 (0, 0.05f * Mathf.Sin (duration * 3));
			this.gameObject.transform.SetPositionAndRotation (origin + new Vector2 (0, 0.05f * Mathf.Sin (duration * 3)), Quaternion.identity);

			if (duration > fishTime) {
				if (InitialFishCatch == false) {
					InitialFishCatch = true;
					player.m_audio.PlayOneShot(Resources.Load<AudioClip>("Audio/Fished1"));
					fish = Random.value;
					stats.FishingState = 0;
					Object.Instantiate (Resources.Load<GameObject>("Prefabs/UI/FishState"), this.gameObject.transform.position + new Vector3 (0,1,0), Quaternion.identity);

				}
				this.gameObject.transform.Rotate (0, 0, Mathf.Sin (duration * 5) * 10);
				if (catching == true) {
					GameObject.Find ("AttackHitBox").GetComponent<Attacking> ().fishing = false;
					Debug.Log ("Catch");
					caught ();
					stats.FishingState = 1;
					stats.Achievements[1,2] = "T";
					stats.Fished += 1;
					Object.Instantiate (Resources.Load<GameObject>("Prefabs/UI/FishState"), this.gameObject.transform.position + new Vector3 (0,1,0), Quaternion.identity);

				}
				if (duration > fishTime + 3) {
					fish = Random.Range (50, 150) * 0.1f;
					duration = 0;
					InitialFishCatch = false;
					stats.FishingState = 2;
					Object.Instantiate (Resources.Load<GameObject>("Prefabs/UI/FishState"), this.gameObject.transform.position + new Vector3 (0,1,0), Quaternion.identity);

				}
			} else {
				if (catching == true) {
					GameObject.Find ("AttackHitBox").GetComponent<Attacking> ().fishing = false;
					Debug.Log ("Didnt Catch");
					stats.FishingState = 2;
					Object.Instantiate (Resources.Load<GameObject>("Prefabs/UI/FishState"), this.gameObject.transform.position + new Vector3 (0,1,0), Quaternion.identity);
					Destroy (this.gameObject);
				}
			}
		}
		if (Vector2.Distance (this.gameObject.transform.position, player.transform.position) > 5) {
			GameObject.Find ("AttackHitBox").GetComponent<Attacking> ().fishing = false;
			Debug.Log ("Too Far");
			Destroy (this.gameObject);
		}
	}
	public void caught(){
		if (fish < 0.05) {

		} else if (fish < 0.1) {
			invBeh.items.Enqueue ("006001");
		} else if (fish < 0.15) {
			invBeh.items.Enqueue ("007001");
		} else if (fish < 0.2) {
			invBeh.items.Enqueue ("008001");
		} else if (fish < 0.25) {
			invBeh.items.Enqueue ("009001");
		} else if (fish < 0.3) {
			invBeh.items.Enqueue ("010001");
		} else if (fish < 0.35) {
			invBeh.items.Enqueue ("011001");
		} else if (fish < 0.4) {
			invBeh.items.Enqueue ("012001");
		} else if (fish < 0.45) {
			invBeh.items.Enqueue ("013001");
		} else if (fish < 0.5) {
			invBeh.items.Enqueue ("014001");
		} else if (fish < 0.55) {
			invBeh.items.Enqueue ("015001");
		} else if (fish < 0.6) {
			invBeh.items.Enqueue ("016001");
		} else if (fish < 0.65) {
			invBeh.items.Enqueue ("017001");
		} else if (fish < 0.7) {
			invBeh.items.Enqueue ("018001");
		} else if (fish < 0.75) {
			invBeh.items.Enqueue ("019001");
		} else if (fish < 0.9f) {
			invBeh.items.Enqueue ("020001");
		} else if (fish < 0.925f) {
			invBeh.items.Enqueue ("022001");
		} else if (fish < 0.95f) {
			invBeh.items.Enqueue ("023001");
		} else if (fish < 0.975f) {
			invBeh.items.Enqueue ("024001");
		} else if (fish < 1) {
			invBeh.items.Enqueue ("025001");
		}
		Destroy (this.gameObject);
	}

	public IEnumerator Destroy(){
		yield return new WaitForSeconds (1f);
		destroy += 1;
	}
}
