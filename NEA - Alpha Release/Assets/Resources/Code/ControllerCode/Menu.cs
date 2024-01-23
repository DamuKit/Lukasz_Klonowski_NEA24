﻿/*Created: Sprint - Last Edited Sprint 
This script’s purpose is to manage the audio settings and the difficulty from the menu. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public StatsStorage stats;
	public Slider Master;
	public Slider Music;
	public Slider SFX;

	// Use this for initialization
	void Start () {
		Master = GameObject.Find ("MasterVolume").GetComponent<Slider> ();
		Music = GameObject.Find ("MusicVolume").GetComponent<Slider> ();
		SFX = GameObject.Find ("SFXVolume").GetComponent<Slider> ();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (stats.menu);
		stats.Master = Master.value;
		stats.Music = Music.value;
		stats.SFX = SFX.value;
	}
	public void ClickedMenuButton(){
		if (stats.menu == 1) {
			stats.menu = 0;
		}
		else{
			stats.menu = 1;
		}
	}
	public void HigherDifficulty(){
		if (stats.Difficulty != 10) {
			stats.Difficulty ++;
		}
	}
	public void LowerDifficulty(){
		if (stats.Difficulty != 1) {
			stats.Difficulty --;
		}
	}
}
