/*Created: Sprint - Last Edited Sprint 
This script’s purpose is to change the text for the difficulty setting in the menu. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MenuText : MonoBehaviour {
	TextMeshProUGUI text;
	StatsStorage stats;
	// Use this for initialization
	void Start () {
		text = this.gameObject.GetComponent<TextMeshProUGUI> ();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.name == "Difficulty") {
			switch (stats.Difficulty) {
			case(1):
				text.SetText("easy");
				break;
			case(2):
				text.SetText("relatively easy");
				break;
			case(3):
				text.SetText("kinda easy");
				break;
			case(4):
				text.SetText("slightly easy");
				break;
			case(5):
				text.SetText("possibly easy");
				break;
			case(6):
				text.SetText("lim(H=>0) H*easy");
				break;
			case(7):
				text.SetText("normal");
				break;
			case(8):
				text.SetText("very normal");
				break;
			case(9):
				text.SetText("not a tangent");
				break;
			case(10):
				text.SetText("hard");
				break;
			default:
				text.SetText("you broke it");
				break;
			}
		}
	}
}
