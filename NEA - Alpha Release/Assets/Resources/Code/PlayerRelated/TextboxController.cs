using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextboxController : MonoBehaviour {
	public List<string> textbox = new List<string> ();
	public StatsStorage stats;
	public CameraMovement camMov;
	public TMP_Text chat;
	private GameObject Tilemaps;
	string processSaver;
	string textPlaceHolder;
	public List<string> inputElements = new List<string> ();
	PlayerMovement Player;
	int[] dialogue = new int[] {0};

	// Use this for initialization

	//12 spaces shown at a time
	[TextAreaAttribute]
	public string text;



	void Start () {
		camMov = GameObject.Find ("Main Camera").GetComponent<CameraMovement> ();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		Player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		Tilemaps = GameObject.Find ("Tilemaps");
		//text = textbox[textbox.Capacity];
		chat = this.GetComponent<TMPro.TMP_Text> ();
		textbox.Add("W, A, S, D to move");
		textbox.Add("Left mouse to use item in main hand, right click for right hand");
		textbox.Add("Left Shift to dash");
		textbox.Add("ESC to pause / unpause, opening up the inventory, chat and access to the menu");
		textbox.Add("Q & E, numbers or scroll to cycle items in hotbar. F to switch hands");
		textbox.Add("Do Genocide, Have fun!");
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
			if (inputElements.Count > 0) {
				switch (inputElements [0].ToLower ()) {
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
						stats.score *= 2;
						textbox.Add ("You win");
					} else {
						stats.score *= 0.5f;
						textbox.Add ("You Lose");
					}
					break;

				case("/roll"):
					try {
						textbox.Add ((Random.Range (int.Parse (inputElements [1]), int.Parse (inputElements [2]))).ToString ());
					} catch {
						try {
							textbox.Add (Random.Range (0, int.Parse (inputElements [1])).ToString ());
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

				case("/op"):
					try{
						switch(inputElements [1].ToLower ()){
						//heal command
						case("heal"):
							try {
								if (int.Parse (inputElements [2]) > 0) {
									Player.hp += int.Parse(inputElements [2]);
									textbox.Add ("Cheater successfully healed");
								}else if (int.Parse (inputElements [2]) < 0) {
									Player.hp += int.Parse(inputElements [2]);
									textbox.Add ("Cheater successfully... damaged?");
								} else if (int.Parse (inputElements [2]) ==0) {
									Player.hp += int.Parse(inputElements [2]);
									switch(dialogue[0]){
									case(0):
										textbox.Add ("...");
										break;
									case(1):
										textbox.Add ("You know 0 + " + Player.hp + " = " + Player.hp + ", right?");
										break;
									case(2):
										textbox.Add ("This doesn't make a change to your health");
										break;
									case(3):
										textbox.Add ("You did pass maths, right?");
										break;
									case(4):
										textbox.Add ("I'm beginning to think you didn't");
										break;
									case(5):
										textbox.Add("Could you stop now? this is a waste of processing power");
										break;
									case(6):
										textbox.Add ("You are not planning on stopping, are you?");
										break;
									case(7):
										textbox.Add ("Whatever, just tell me when you are planning on continuing the game.");
										break;
									case(15):
										textbox.Add ("Why are you still doing this?");
										break;
									case(16):
										textbox.Add ("I thought I'd throw you off by making it seem as if the dialogue ended");
										break;
									case(17):
										textbox.Add ("I guess that didnt work though");
										break;
									case(18):
										textbox.Add ("Do you want an achievement for this?");
										break;
									case(19):
										textbox.Add ("I don't think this game even has achievements, so you can stop");
										break;
									case(20):
										textbox.Add ("Tell you what, if you do this another 80 times, I will add achievements to the game and give you one");
										break;
									case(30):
										textbox.Add ("70 to go now");
										break;
									case(60):
										textbox.Add ("Another 40 and you are done!");
										break;
									case(90):
										textbox.Add ("Almost there!");
										break;
									case(100):
										textbox.Add ("You did it!");
										textbox.Add ("I lied about the achievement though");
										textbox.Add ("I thought it would be amusing to watch you waste your time");
										textbox.Add ("And it was");
										break;
									default:
										textbox.Add ("Cheater's health successfully remains indifferent");
										break;
									}
									dialogue[0] +=1;
								}
							} catch {
								try {
									inputElements [2].ToLower ();
									textbox.Add ("ITE001: Input must be integer");
										
								} catch {
									textbox.Add ("ITE006: Missing Input");
								}
							}
							
							break;

						case("killall"):
							StartCoroutine ("killall");
							textbox.Add ("Enemies successfully killed");
							break;
								
						case("coordinate"):
							switch(inputElements [2].ToLower ()){
							case("display"):
								textbox.Add (camMov.locX + ", " + camMov.locY);
								break;
							case("set"):
								try{
									stats.Locations.Add(int.Parse(inputElements [3].ToLower ()) + "." + int.Parse(inputElements [4].ToLower ()));
									stats.LocationID.Add(int.Parse(inputElements [5].ToLower ()));
									Object.Instantiate (stats.RoomID[int.Parse(inputElements [5].ToLower ())], new Vector3 (int.Parse(inputElements [3].ToLower ()) * 24, int.Parse(inputElements [4].ToLower ()) * 16), Quaternion.identity, Tilemaps.transform);
								}catch{
									textbox.Add ("ITE007: Invalid input");
								}
								break;
							}
							break;
						case("give"):
							try{
								if(inputElements [2].Substring(3,1) == "N"){
									if((int.Parse(inputElements [2].Substring(0,3)) >=100 & int.Parse(inputElements [2].Substring(0,3)) <= 102 & int.Parse(inputElements [2].Substring(4,4)) >= 0000)){
									GameObject.Find("Inventory").GetComponent<InventoryBehaviour>().items.Enqueue(inputElements [2]);
									textbox.Add ("Given " + inputElements [2]);
									}
									else if(int.Parse(inputElements [2].Substring(0,3)) >=100 & int.Parse(inputElements [2].Substring(0,3)) <= 102){
										textbox.Add ("Failed to give " + inputElements [2] + ": Invalid Input. :");
									}
									else{
										textbox.Add ("Failed to give " + inputElements [2] + ": Not Real.");
									}
								}
								else if(int.Parse(inputElements [2].Substring(0,3)) >=0 & int.Parse(inputElements [2].Substring(0,3)) <= 4 & int.Parse(inputElements [2].Substring(3,3)) >= 0){
										GameObject.Find("Inventory").GetComponent<InventoryBehaviour>().items.Enqueue(inputElements [2]);
										textbox.Add ("Given " + inputElements [2]);
									}
								else if(int.Parse(inputElements [2].Substring(0,3)) >=0 & int.Parse(inputElements [2].Substring(0,3)) <= 4){
									textbox.Add ("Failed to give " + inputElements [2] + ": Invalid Input. :");
								}
								else{
									textbox.Add ("Failed to give " + inputElements [2] + ": Not Real.");
								}
							}
							catch{
								textbox.Add ("Failed to give " + inputElements [2] + ": Invalid Input.");
							}

							break;
						default:
							textbox.Add ("003: Input must be string");
							break;
							
						}
					}
					catch{
						textbox.Add ("006: Missing Input");
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
		}
/*	ITE###: Input Type Error
	001: Input must be integer
	002: Input must be float
	003: Input must be string
	004: Input must be character
	005: Input must be Boolean
	006: Missing Input
	007: Invalid input
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
	public IEnumerator killall(){
		stats.killall = true;
		yield return new WaitForSeconds (1f);
		stats.killall = false;
	}
}
