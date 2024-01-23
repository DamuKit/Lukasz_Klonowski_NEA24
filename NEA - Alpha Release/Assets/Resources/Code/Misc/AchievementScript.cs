/*Created: Sprint - Last Edited Sprint 
This script’s purpose is to manage the text and appearance of achievement and statistic related components in the menu. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementScript : MonoBehaviour {
	TMP_Text Text;
	StatsStorage stats;
	Color Default;
	int type;
	bool update;
	int statistic;
	int pH;


	// Use this for initialization
	void Start () {
		update = false;
		statistic = 0;

		if (this.gameObject.name == "AchievementsText") {
			type = 1;
		}
		else if(this.gameObject.name == "StatsText") {
			type = 3;
		}
		else if(this.gameObject.name.Substring(3,1) == "A") {
			type = 2;
		}
		else if(this.gameObject.name.Substring(3,1) == "S") {
			type = 4;
			statistic = int.Parse (this.gameObject.name.Substring (0, 3));
			pH = -1;
		}
		/*else if(this.gameObject.name.Substring(3,1) == "S") {
			type = 4;
		}
		*/

		if(type == 1 | type == 3){
			Text = this.GetComponent<TMP_Text> ();
			Default = Text.faceColor;
		}
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (stats.menu == 1 & (type == 1 | type == 3)) {
			if (type == 1) {
				Text.SetText ("Achievements");
				Text.faceColor = Default;
			}
			if (type == 3) {
				Text.SetText ("Statistics");
				update = false;
			}
		} else {
			if (type == 2) {
				if (stats.Achievements [int.Parse (this.gameObject.name.Substring (0, 3)), 2] == "T") {
					//gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
					this.gameObject.GetComponent<Image> ().color = Color.white;
					Destroy (this);
				}
			}

			if (type == 4) {
				if(pH != int.Parse(stats.Statistic[statistic,2])){
					this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/CustomSprites/Stat Sprites/" + this.gameObject.name.Substring(0,3) + stats.Statistic[statistic,2] + "Stat");
					pH = int.Parse (stats.Statistic [statistic, 2]);

				}
			}
		}

		if(type == 3 & update == true){
			Text.SetText  (stats.Statistic[statistic,0].ToString() + " " + stats.Statistic[statistic,1].ToString());
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
		}else{
			Text.SetText (stats.Achievements[ID,0]);
			Text.faceColor = Default;

		}
	}
	public void updateStatText(int ID){
		update = true;
		statistic = ID;
	}
}
