using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGenerator : MonoBehaviour {
	public StatsStorage stats;

	// Use this for initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		Random.InitState (stats.seed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Item(GameObject thing){
		if(Random.value > 0.5){
			
		}
	}
}
