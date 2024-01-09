using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementScript : MonoBehaviour {
	TMP_Text Text;
	StatsStorage stats;
	Color Default;

	// Use this for initialization
	void Start () {
		if (this.gameObject.name == "AchievementsText") {
			Text = this.GetComponent<TMP_Text> ();
			Text.outlineColor = Color.green;
			Default = Text.faceColor;
		}
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.name == "AchievementsText") {
			if (stats.menu == 1) {
				Text.SetText ("Achievements");
				Text.faceColor = Default;
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
			if(stats.Achievements[ID,1] == ""){
				Text.SetText (stats.Achievements[ID,0]);
			}else{
				Text.SetText (stats.Achievements[ID,1]);
			}
			Text.faceColor = new Color32(42, 204, 0, 255);
			Text.outlineWidth = 100;
		}else{
			Text.SetText (stats.Achievements[ID,0]);
			Text.faceColor = Default;
			Text.outlineWidth = 0;

		}
	}
}
