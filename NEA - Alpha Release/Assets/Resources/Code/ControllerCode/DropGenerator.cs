/*Created: Sprint 4 - Last Edited Sprint 8
This script’s purpose is to decide what items are generated upon killing an enemy. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGenerator : MonoBehaviour {
	public StatsStorage stats;
	GameObject Items;
	float rand;
	public int refreshSeed;
	int i;

	// initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		Random.InitState (stats.seed);
		refreshSeed = stats.seed;
		Items = GameObject.Find("Items");
		i = 0;
	}
	
	// Update once per frame
	void Update () {
		// Seed check
		if (refreshSeed != stats.seed) {
			refreshSeed = stats.seed;
			Random.InitState (stats.seed);
		}
	}

	// attempt to generate a random item
	void Item(GameObject thing){
		i = 0;
		rand = Random.value;
		do {
			try{
				if(rand >= 0 & rand < stats.Items [i,1] * 0.0001f & stats.Items [i,2] == stats.localDifficulty){
					Object.Instantiate (stats.ItemID[stats.ItemID.FindIndex(a => int.Parse(a.name.ToString().Substring(1,3)) == stats.Items [i,0])], thing.transform.position, Quaternion.identity, Items.transform);
					break;
				}
			}
			catch{
				break;
			}
			i+=1;
		} while(i <= stats.ItemID.Count);
	}
}
