using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGenerator : MonoBehaviour {
	public StatsStorage stats;
	GameObject Items;
	float rand;
	// Use this for initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		Random.InitState (stats.seed);
		Items = GameObject.Find("Items");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Item(GameObject thing){
		rand = Random.value;
		if(rand >= 0.5f & rand < 0.8f){
			Object.Instantiate (stats.ItemID [0], thing.transform.position, Quaternion.identity, Items.transform);		
		}
		else if(rand >= 0.8f & rand < 1){
			Object.Instantiate (stats.ItemID [1], thing.transform.position, Quaternion.identity, Items.transform);		
		}
	}
}
