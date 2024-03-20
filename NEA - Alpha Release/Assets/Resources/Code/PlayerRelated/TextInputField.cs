/*Created: Sprint 4 - Last Edited Sprint 5
This script’s purpose is to interact with the textbox controller code, adding the text from the textbox. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextInputField : MonoBehaviour {
	public TMP_InputField input;
	public TextboxController textbox;
	string text;

	// Initialization
	void Start () {
		input = this.GetComponent<TMPro.TMP_InputField> ();
		textbox = GameObject.Find ("Textbox").GetComponent<TextboxController> ();
	}
	
	// Update once per frame
	void Update () {
		// Inputs the chatbox text from the player
		if (Input.GetKeyDown (KeyCode.Return) == true & input.text != "") {
			text = input.text;
			textbox.textbox.Add (text);
			input.text = "";
		}
	}
}
