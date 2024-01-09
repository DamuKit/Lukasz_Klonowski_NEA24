using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementScript : MonoBehaviour {
	TMP_Text Text;
	StatsStorage stats;

	// Use this for initialization
	void Start () {
		if (this.gameObject.name == "AchievementsText") {
			Text = this.GetComponent<TMP_Text> ();
		}
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.name == "AchievementsText") {
			if (stats.menu == 1) {
				Text.SetText ("Achievements");
			}
		} else {
			if (stats.Achievements [int.Parse (this.gameObject.name.Substring (0, 3)), 2] == "T") {
				//gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
				this.gameObject.GetComponent<Image> ().color = Color.white;
				Destroy (this);
			}
		}
	}
	public void updateText(int ID){
		if(stats.Achievements[ID,2] == "T"){
			Text.SetText (stats.Achievements[ID,1]);
		}else{
			Text.SetText (stats.Achievements[ID,0]);
		}
	}
}
