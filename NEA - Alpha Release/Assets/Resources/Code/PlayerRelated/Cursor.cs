/*Created: Sprint 2 - Last Edited Sprint 2
This script’s purpose is to make the in game cursor go to the position of the mouse cursor. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cursor : MonoBehaviour {
	public CameraMovement camMov;
	public PlayerMovement playerMovement;
	public TMP_Text Description;
	public float Displaying;
	bool CurrentDisplay;
	// Use this for initialization
	void Start () {
		Description = this.gameObject.transform.GetChild (0).GetChild (0).gameObject.GetComponent<TMP_Text> ();
		Displaying = 0;
		CurrentDisplay = false;
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.SetPositionAndRotation(new Vector3 ((Input.mousePosition.x - (Display.main.systemWidth / 2)) / Display.main.systemWidth * playerMovement.camerasizex * 2 + (camMov.locX * playerMovement.camerasizex * 2), (Input.mousePosition.y - (Display.main.systemHeight / 2)) / Display.main.systemHeight * playerMovement.camerasizey * 2 + (camMov.locY * playerMovement.camerasizey * 2), 0), Quaternion.identity);
		this.gameObject.transform.Rotate (0, 0, 135);
		if (CurrentDisplay == false & Displaying > 0) {
			Displaying -= 1 * Time.deltaTime;
		}
	}
	public void Describe(string item){
		
		if (item.Substring (0, 1) == "0") {
			if (int.Parse (item.Substring (3, 3)) > 1) {
				Description.SetText (Identify(item.Substring (0, 3)) + " x" + (int.Parse(item.Substring (3, 3))).ToString());
			} else {
				Description.SetText (Identify(item.Substring (0, 3)));
			}
		} else if (item.Substring (0, 1) == "1") {
			if (item.Substring (0, 3) == "103") {
				Description.SetText ("Fishing Rod");
			} else {
				if(Mathf.Pow (10, int.Parse (item.Substring (4, 1))) * int.Parse (item.Substring (5, 3)) > 0){
					Description.SetText (Identify(item.Substring (0, 3)) + " +" + (Mathf.Pow (10, int.Parse (item.Substring (4, 1))) * int.Parse (item.Substring (5, 3))).ToString () + " Bonus");
				}
				else{
					Description.SetText (Identify(item.Substring (0, 3)));
				}
			}
		}
		this.gameObject.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.5f);
		CurrentDisplay = true;
		Displaying = 1;
	}
	public void stop(){
		this.gameObject.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);
		Description.SetText ("");
		CurrentDisplay = false;
	}
	private string Identify(string item){
		switch (item) {
		case("001"):
			return("Hearts");
			break;
		case("002"):
			return("Strength Potion");
			break;
		case("003"):
			return("Speed Potion");
			break;
		case("004"):
			return("Enemy Repellant");
			break;
		case("005"):
			return("Arrow");
			break;
		case("006"):
			return("Atlantic Bass");
			break;
		case("007"):
			return("Clownfish");
			break;
		case("008"):
			return("Dab");
			break;
		case("009"):
			return("Sea Spider");
			break;
		case("010"):
			return("Blue Gill");
			break;
		case("011"):
			return("Guppy");
			break;
		case("012"):
			return("Freshwater Snail");
			break;
		case("013"):
			return("axolotl");
			break;
		case("014"):
			return("High Fin Banded Shark");
			break;
		case("015"):
			return("Golden Trench");
			break;
		case("016"):
			return("Moss Ball");
			break;
		case("017"):
			return("Plastic Bag");
			break;
		case("018"):
			return("Junonia");
			break;
		case("019"):
			return("Sand Dollar");
			break;
		case("020"):
			return("Stafish");
			break;
		case("021"):
			return("Ammunition");
			break;
		case("022"):
			return("Gun Crate");
			break;
		case("023"):
			return("Melee Crate");
			break;
		case("024"):
			return("Item Crate");
			break;
		case("025"):
			return("Random Crate");
			break;
		case("026"):
			return("Coin");
			break;

		case("100"):
			return("Sword");
			break;
		case("101"):
			return("Shield");
			break;
		case("102"):
			return("Gun 1");
			break;
		case("103"):
			return("Fishing Rod");
			break;
		case("104"):
			return("Gun 2");
			break;
		case("105"):
			return("Gun 3");
			break;
		case("106"):
			return("Gun 4");
			break;
		case("107"):
			return("Gun 5");
			break;
		case("108"):
			return("Gun 6");
			break;
		case("109"):
			return("Gun 7");
			break;
		case("110"):
			return("Frying Pan");
			break;
		case("111"):
			return("Wooden Bat");
			break;
		case("112"):
			return("Axe");
			break;
		case("113"):
			return("Dagger");
			break;
		default:
			return("???");
			break;
		}
	}
}
