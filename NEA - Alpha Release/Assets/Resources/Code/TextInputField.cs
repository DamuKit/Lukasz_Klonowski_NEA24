using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextInputField : MonoBehaviour {
	public TMP_InputField input;
	string text;
	// Use this for initialization
	void Start () {
		input = this.GetComponent<TMPro.TMP_InputField> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return) == true & input.text != "") {
			text = input.text;
			Debug.Log (text);
			input.text = "";
		}
	}
}
