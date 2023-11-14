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
		textbox.Add("Tab to hide chat");
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
			split ();

			chat.text = textbox[textbox.Count-12] + "\n" + textbox[textbox.Count-11] + "\n" + textbox[textbox.Count-10] + "\n" + textbox[textbox.Count-9] + "\n" + textbox[textbox.Count-8] + "\n" + textbox[textbox.Count-7] + "\n" + textbox[textbox.Count-6] + "\n" + textbox[textbox.Count-5] + "\n" + textbox[textbox.Count-4] + "\n" + textbox[textbox.Count-3] + "\n" + textbox[textbox.Count-2] + "\n" + textbox[textbox.Count-1];

			switch (inputElements [0].ToLower()) {
			//Help Command
			case("/help"):
				{
					textbox.Add ("/help <int>");
					textbox.Add ("/heal <int>");
					textbox.Add ("/difficulty <int>");
					textbox.Add ("/clearchat");
					textbox.Add ("/seed <int>");
					textbox.Add ("/gamble <int>");
					textbox.Add ("/Roll <int> <int>");
					textbox.Add ("[Page 1/1]");
				}
				break;

			//clearchat command
			case("/clearchat"):
				{
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
				}
				break;

			
			//heal command
			case("/heal"):
				{
					try {
						if (int.Parse (inputElements [1]) > 0) {
							textbox.Add ("NO HEALING");
						}
					} catch {
						try {
							inputElements [1].ToLower ();
							textbox.Add ("ITE001: Input must be integer");

						} catch {
							textbox.Add ("ITE006: Missing Input");
						}
					}
				}
					
				break;
			//difficulty command
			case("/difficulty"):
				{
					try {
						if (int.Parse (inputElements [1]) > 0) {
							stats.Difficulty = int.Parse (inputElements [1]);
							textbox.Add ("Difficulty Changed");
						}
					} catch {
						try {
							inputElements [1].ToLower ();
							textbox.Add ("ITE001: Input must be integer");

						} catch {
							textbox.Add ("ITE006: Missing Input");
						}
					}
				}
				break;
			//seed command
			case("/seed"):
				{
					try {
						if (int.Parse (inputElements [1]) > 0) {
							if (int.Parse (inputElements [1]) >= 0 & int.Parse (inputElements [1]) <= 1000000000) {
								stats.seed = int.Parse (inputElements [1]);
								textbox.Add ("Seed Changed");
							}
						}
					} catch {
						try {
							inputElements [1].ToLower ();
							textbox.Add ("ITE001: Input must be integer N");
						} catch {
							textbox.Add ("ITE006: Missing Input");
						}
					}
				}
				break;

			case("/gamble"):
				if (Random.value > 0.75) {
					textbox.Add ("You win");
				} else {
					textbox.Add ("You Lose");
				}
				break;

			case("roll"):
				try{
					if (int.Parse (inputElements [1]) > 0) & int.Parse (inputElements [2]) > 0){
						textbox.Add (Random.Range(int.Parse (inputElements [1]), int.Parse (inputElements [2])));
					}

				catch{
				}
				break;

				//case(""):
				//break;
			//no existing command

			default:
				{
					textbox.Add ("CTE001: unknown command");
				}
				break;
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

	CTE###: Command Type Error
	001: unknown command
*/
	}
	public void split(){
		if(textPlaceHolder.StartsWith("/")){
			textPlaceHolder += " .";
			inputElements.Clear();
			do{
				inputElements.Add(textPlaceHolder.Substring(0, textPlaceHolder.IndexOf(" ")));
				textPlaceHolder = textPlaceHolder.Substring (textPlaceHolder.IndexOf(" ") + 1);
			}while(textPlaceHolder.Contains(" "));
		}
	}
}
