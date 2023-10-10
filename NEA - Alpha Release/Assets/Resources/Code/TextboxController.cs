using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextboxController : MonoBehaviour {
	public List<string> textbox = new List<string> ();
	// Use this for initialization

	//12 spaces shown at a time
	[TextAreaAttribute]
	public string text;


	void Start () {
		text = "a/na/na/na/na/na/na/na/na/na/na/na";
	}
	
	// Update is called once per frame
	void Update () {
		

	}
}
