using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextboxController : MonoBehaviour {
	public List<string> textbox = new List<string> ();
	public StatsStorage stats;
	public TMP_Text chat;
	string processSaver;
	string textPlaceHolder;
	public List<string> inputElements = new List<string> ();

	// Use this for initialization

	//12 spaces shown at a time
	[TextAreaAttribute]
	public string text;


	void Start () {
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		//text = textbox[textbox.Capacity];
		chat = this.GetComponent<TMPro.TMP_Text> ();
		textbox.Add("W, A, S, D to move");
		textbox.Add("Left mouse to attack");
		textbox.Add("Left Shift to dash");
		textbox.Add("");
		textbox.Add("");
		textbox.Add("");
		textbox.Add("");
		textbox.Add("");
		textbox.Add("");
		textbox.Add("");
		textbox.Add("");
		textbox.Add("");
		processSaver = "e";
	}
	
	// Update is called once per frame
	void Update () {
		if(processSaver != (textbox [textbox.Count - 1]).ToString()){
			processSaver = (textbox [textbox.Count - 1]).ToString();
			textPlaceHolder = processSaver;
			//split ();

			chat.text = textbox[textbox.Count-12] + "\n" + textbox[textbox.Count-11] + "\n" + textbox[textbox.Count-10] + "\n" + textbox[textbox.Count-9] + "\n" + textbox[textbox.Count-8] + "\n" + textbox[textbox.Count-7] + "\n" + textbox[textbox.Count-6] + "\n" + textbox[textbox.Count-5] + "\n" + textbox[textbox.Count-4] + "\n" + textbox[textbox.Count-3] + "\n" + textbox[textbox.Count-2] + "\n" + textbox[textbox.Count-1];
			if ((textbox [textbox.Count - 1]).ToString ().ToLower () == "/help") {
				textbox.Add ("No help for you.");
			} else if ((textbox [textbox.Count - 1]).ToString ().ToLower () == "/clearchat") {
				textbox.Add ("");
				textbox.Add ("");
				textbox.Add ("");
				textbox.Add ("");
				textbox.Add ("");
				textbox.Add ("");
				textbox.Add ("");
				textbox.Add ("");
				textbox.Add ("");
				textbox.Add ("");
				textbox.Add ("");
				textbox.Add ("");
			} else if (processSaver.Length >= 6 && processSaver.ToLower ().Substring (0, 6) == "/heal ") {
				try {
					if (int.Parse (processSaver.ToString ().ToLower ().Substring (6)) > 0) {
						//stats.Difficulty = int.Parse (processSaver.ToString ().ToLower ().Substring (6));
						textbox.Add ("Okay cheater");
					}
				} catch {
					if (processSaver.Length - 6 > 0) {
						textbox.Add ("ITE001: Input must be integer");
					} else {
						textbox.Add ("ITE006: Missing Input");
					}
				}

			} else if (processSaver.Length >= 12 && processSaver.ToString ().ToLower ().Substring (0, 12) == "/difficulty ") {
				try {
					if (int.Parse (processSaver.ToString ().ToLower ().Substring (12)) > 0 & int.Parse (processSaver.ToString ().ToLower ().Substring (12)) < 10) {
						stats.Difficulty = int.Parse (processSaver.ToString ().ToLower ().Substring (12));
						textbox.Add ("Difficulty Changed");
					}
				} catch {
					if (processSaver.Length - 12 > 0) {
						textbox.Add ("ITE001: Input must be integer");
					} else {
						textbox.Add ("ITE006: Missing Input");
					}
				}

			} else if (processSaver.Length >= 6 && processSaver.ToString ().ToLower ().Substring (0, 6) == "/seed ") {
				try {
					if (int.Parse (processSaver.ToString ().ToLower ().Substring (6)) >= 0 & int.Parse (processSaver.ToString ().ToLower ().Substring (6)) <= 1000000000) {
						stats.seed = int.Parse (processSaver.ToString ().ToLower ().Substring (6));
						textbox.Add ("Seed Changed");
					}
				} catch {
					if (processSaver.Length - 6 > 0) {
						textbox.Add ("ITE001: Input must be integer");
					} else {
						textbox.Add ("ITE006: Missing Input");

					}
				}
			}
			inputElements.Clear ();
		}
/*	ITE###: Input Type Error
	001: Input must be integer
	002: Input must be float
	003: Input must be string
	004: Input must be character
	005: Input must be Boolean
	006: Missing Input
	...
*/
	}
	/*public void split(){
		if(textPlaceHolder.StartsWith("/")){
			Debug.Log("E");
			inputElements.Add(processSaver.Substring (processSaver.IndexOf (" ") +1));
			inputElements.AddRange (new List<string> (textPlaceHolder.Split (" ")));

			Debug.Log (inputElements [0]);
			Debug.Log (inputElements [1]);
		}
	}*/
}
