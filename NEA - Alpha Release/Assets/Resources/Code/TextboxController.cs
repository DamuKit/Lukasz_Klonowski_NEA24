using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextboxController : MonoBehaviour {
	public List<string> textbox = new List<string> ();
	public TMP_Text chat;
	string processSaver;
	// Use this for initialization

	//12 spaces shown at a time
	[TextAreaAttribute]
	public string text;


	void Start () {
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
			} else if ((textbox [textbox.Count - 1]).ToString ().ToLower ().Substring (0, 5) == "/heal") {
				if ((textbox [textbox.Count - 1]).ToString ().ToLower ().Substring (0, 6) == "/heal ") {
					textbox.Add ("Okay cheater");
				} else {
					textbox.Add ("error");
				}
			}
		}

	}
}
