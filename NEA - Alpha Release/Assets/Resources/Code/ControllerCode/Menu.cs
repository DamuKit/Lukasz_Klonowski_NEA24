using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
	public StatsStorage stats;
	// Use this for initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ClickedMenuButton(){
		if (stats.menu == 1) {
			stats.menu = 0;
		}
		else{
			stats.menu = 1;
		}
	}
}
