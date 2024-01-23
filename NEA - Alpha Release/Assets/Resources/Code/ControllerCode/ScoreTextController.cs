/*Created: Sprint - Last Edited Sprint 
This script’s purpose is to display the score of the player in text form. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreTextController : MonoBehaviour {
	public TMP_Text score;
	public StatsStorage stats;
	// Use this for initialization
	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		score = this.GetComponent<TMPro.TMP_Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		score.text = "Score : " + Mathf.RoundToInt(stats.score) + "000";

	}
}
