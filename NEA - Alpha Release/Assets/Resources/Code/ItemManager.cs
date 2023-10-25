using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
	public StatsStorage stats;
	// Use this for initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		if(this.gameObject.name.Length > 2){
			this.gameObject.name = (this.gameObject.name.Substring (0, 2));
			Debug.Log (int.Parse(this.gameObject.name));
			//Debug.Log (this.gameObject.name);
		}
		//stats.Items [int.Parse (this.gameObject.name, 1)];
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			other.gameObject.SendMessage ("itemEffect", int.Parse(this.gameObject.name));
			Destroy (this.gameObject);
		}
	}
}
