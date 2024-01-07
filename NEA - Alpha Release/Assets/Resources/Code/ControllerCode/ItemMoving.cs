using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoving : MonoBehaviour {
	InventoryBehaviour invBeh;
	GameObject cursor;
	public StatsStorage stats;
	bool held;
	public CameraMovement camMov;
	public string placeHolder;
	//Inventory Slot Locations
	public int ISLX;
	public int ISLY;
	public int positionInList;
	public int currentPosition;
	public string currentItem;
	Animator Animation;
	bool hidden;
	string pH2;

	// Use this for initialization
	void Start () {
		hidden = true;
		Animation = GetComponent<Animator>();
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		cursor = GameObject.Find ("Cursor");
		invBeh = this.GetComponentInParent<InventoryBehaviour> ();
		held = false;
		camMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement> ();
		ISLX = Mathf.RoundToInt (this.gameObject.transform.position.x- camMov.locX * 24) + 11;
		ISLY = Mathf.RoundToInt (this.gameObject.transform.position.y - camMov.locY * 16 - 1000 * stats.pause)* -1 + 4;
		positionInList = ISLX + ISLY * 11;
		currentPosition = positionInList;
		currentItem = invBeh.Locations [currentPosition];
		Animation.SetInteger ("ItemID", int.Parse(currentItem.Substring (0, 3)));
	}

	// Update is called once per frame
	void Update () {
		pH2 = "0";

		if (currentPosition < 0) {
			currentPosition = currentPosition + (28 + 77);
		}
		if (currentPosition < 77) {
			this.gameObject.transform.SetPositionAndRotation (new Vector2 (ISLX - 11 + camMov.locX * 24, (ISLY - 4) * -1 + camMov.locY * 16 + 1000 * stats.pause), Quaternion.identity);
		} else {
			this.gameObject.transform.SetPositionAndRotation (new Vector2 (ISLX - 11 + camMov.locX * 24, (ISLY - 4) * -1 + camMov.locY * 16), Quaternion.identity);
		}


		if (currentItem != invBeh.Locations [currentPosition] & Mathf.RoundToInt (this.gameObject.transform.position.y - camMov.locY * 16) * -1 + 4 > -100) {
			
			ISLX = Mathf.RoundToInt (this.gameObject.transform.position.x - camMov.locX * 24) + 11;
			ISLY = Mathf.RoundToInt (this.gameObject.transform.position.y - camMov.locY * 16)* -1 + 4;
			Debug.Log (ISLY);
			positionInList = ISLX + ISLY * 11;
			if (positionInList < 0) {
				positionInList = positionInList + (28 + 77);
			}

			//currentItem = invBeh.Locations [positionInList];
			//currentPosition = positionInList;
			if(currentItem.Substring(0,3) == invBeh.Locations [positionInList].Substring(0,3) & currentItem.Substring(3,1) == "N"){
				currentItem = invBeh.Locations [positionInList];
				currentPosition = positionInList;
			}
			else if(currentItem.Substring(0,3) == invBeh.Locations [positionInList].Substring(0,3) & currentItem.Substring(3,3) != invBeh.Locations [positionInList].Substring(3,3)){
				Debug.Log ("EEE");
				currentItem = invBeh.Locations [positionInList];
				currentPosition = positionInList;
			}
			else{
				this.gameObject.transform.position = new Vector2(Mathf.RoundToInt(invBeh.swapPosition % 11)-11 + camMov.locX * 24, (Mathf.RoundToInt(invBeh.swapPosition / 11) - 4)*-1 + camMov.locY * 16);
				currentPosition = invBeh.swapPosition;
				if (currentPosition >= 77) {
					this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x + 5, this.gameObject.transform.position.y + 10);
				}
			}
			ISLX = Mathf.RoundToInt (this.gameObject.transform.position.x - camMov.locX * 24) + 11;
			ISLY = Mathf.RoundToInt (this.gameObject.transform.position.y - camMov.locY * 16)* -1 + 4;
			Debug.Log (ISLY);
			positionInList = ISLX + ISLY * 11;
			if (positionInList < 0) {
				positionInList = positionInList + (28 + 77);
			}
		}

		if (Input.GetKeyDown (KeyCode.Mouse0) == true & stats.pause == 0 & stats.menu == 1 & Vector2.Distance(cursor.transform.position, this.gameObject.transform.position) < 0.5 & stats.holding == false) {
			stats.holding = true;
			held = true;
		}
		if ((Input.GetKey (KeyCode.Mouse0) == true & stats.pause == 0 & stats.menu == 1) & held == true) {
			if (Input.GetKey (KeyCode.LeftControl) == true) {
				invBeh.Locations [currentPosition] = invBeh.Locations [currentPosition].Substring (0, 3) + "000";
				stats.holding = false;
			}
			Debug.Log ("E");
			//if((cursor.transform.position.x - camMov.locX * 24 > -11.5f & cursor.transform.position.x - camMov.locX * 24 < -0.5f & cursor.transform.position.y - camMov.locY * 16 < 4.5f  )|( cursor.transform.position.x - camMov.locX * 24 > -6.5f & cursor.transform.position.x - camMov.locX * 24 < -0.5f)){
				//ISLX = Mathf.RoundToInt (this.gameObject.transform.position.x - camMov.locX * 24) + 11;
			//} if((cursor.transform.position.y - camMov.locY * 16 > -2.5f & cursor.transform.position.y - camMov.locY * 16 < 4.5f )|( cursor.transform.position.y - camMov.locY * 16 > 6.5f & cursor.transform.position.y - camMov.locY * 16 < 7.5f & cursor.transform.position.x - camMov.locX * 24 > -6.5f)){
				//ISLY = Mathf.RoundToInt (this.gameObject.transform.position.y - camMov.locY * 16)* -1 + 4;
			//}
			this.gameObject.transform.SetPositionAndRotation(new Vector2(cursor.transform.position.x, cursor.transform.position.y), Quaternion.identity);

			if (this.gameObject.transform.position.y * -1 + 4 - camMov.locY * 16 > -10) {
				if ((cursor.transform.position.x - camMov.locX * 24 > -11.5f & cursor.transform.position.x - camMov.locX * 24 < -0.5f & cursor.transform.position.y - camMov.locY * 16 < 4.5f) | (cursor.transform.position.x - camMov.locX * 24 > -6.5f & cursor.transform.position.x - camMov.locX * 24 < -0.5f)) {
					ISLX = Mathf.RoundToInt (this.gameObject.transform.position.x - camMov.locX * 24) + 11;
				}
				if((cursor.transform.position.y - camMov.locY * 16 > -2.5f & cursor.transform.position.y - camMov.locY * 16 < 4.5f )|( cursor.transform.position.y - camMov.locY * 16 > 6.5f & cursor.transform.position.y - camMov.locY * 16 < 7.5f & cursor.transform.position.x - camMov.locX * 24 > -6.5f)){
					ISLY = Mathf.RoundToInt (this.gameObject.transform.position.y - camMov.locY * 16) * -1 + 4;
				}
				Debug.Log (ISLY);
				positionInList = ISLX + ISLY * 11;
				if (positionInList < 0) {
					positionInList = positionInList + (28 + 77);
				}
				Debug.Log (positionInList);
				Debug.Log (currentPosition);
			}
		} else if ((Input.GetKeyUp (KeyCode.Mouse0) == true | stats.pause == 1 | stats.menu == 0) & held == true) {
			if (invBeh.Locations [positionInList].Length > 3) {
				if (invBeh.Locations [positionInList].Substring (3, 1) != "N") {
					if (invBeh.Locations [positionInList].Substring (0, 3) == invBeh.Locations [currentPosition].Substring (0, 3) & positionInList != currentPosition) {
						if (int.Parse (invBeh.Locations [positionInList].Substring (3, 3)) + int.Parse (invBeh.Locations [currentPosition].Substring (3, 3)) <= stats.stackLimit) {
							invBeh.Locations [currentPosition] = invBeh.Locations [positionInList].Substring (0, 3) + ((int.Parse (invBeh.Locations [positionInList].Substring (3, 3)) + int.Parse (invBeh.Locations [currentPosition].Substring (3, 3)) + 1000).ToString ()).Substring (1, 3);
							invBeh.swapPosition = currentPosition;
							invBeh.Locations [positionInList] = "0000000000";

							Destroy (this.gameObject);
						} else {
							pH2 = invBeh.Locations [positionInList];
							//invBeh.Locations [positionInList] = (invBeh.Locations [positionInList].Substring (0, 3) + ((int.Parse (invBeh.Locations [positionInList].Substring (3, 3)) + int.Parse (invBeh.Locations [currentPosition].Substring (3, 3)) + 1001).ToString ()).Substring (1, 3));
							//invBeh.Locations [currentPosition] = pH2.Substring (0, 3) + "999";
							Debug.Log ("AA");

							invBeh.Locations [currentPosition] = (invBeh.Locations [positionInList].Substring (0, 3) + ((int.Parse (invBeh.Locations [positionInList].Substring (3, 3)) + int.Parse (invBeh.Locations [currentPosition].Substring (3, 3)) + 2000 - stats.stackLimit).ToString ()).Substring (1, 3));
							invBeh.Locations [positionInList] = pH2.Substring (0, 3) + (1000 + stats.stackLimit).ToString().Substring(1,3);

							invBeh.swapPosition = currentPosition;
							currentItem = invBeh.Locations [currentPosition];

							this.gameObject.transform.position = new Vector2(Mathf.RoundToInt(currentPosition % 11)-11 + camMov.locX * 24, (Mathf.RoundToInt(currentPosition / 11) - 4)*-1 + camMov.locY * 16);
							currentPosition = invBeh.swapPosition;
							if (currentPosition >= 77) {
								this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x + 5, this.gameObject.transform.position.y + 10);
							}

							//placeHolder = invBeh.Locations [positionInList];
							//Destroy (this.gameObject);

						}
						//invBeh.swapPosition = currentPosition;
						//invBeh.Locations [positionInList] = "0000000000";

						//Destroy (this.gameObject);
					}
				}
				else if(invBeh.Locations [positionInList] == invBeh.Locations [currentPosition]){
					pH2 = invBeh.Locations [positionInList];
					Debug.Log ("AA");


					invBeh.swapPosition = currentPosition;
					currentItem = invBeh.Locations [currentPosition];
					Debug.Log (currentPosition);
					this.gameObject.transform.position = new Vector2(Mathf.RoundToInt(currentPosition % 11)-11 + camMov.locX * 24, (Mathf.RoundToInt(currentPosition / 11) - 4)*-1 + camMov.locY * 16);
					currentPosition = invBeh.swapPosition;
					if (currentPosition >= 77) {
						this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x + 5, this.gameObject.transform.position.y + 10);
					}
					ISLX = Mathf.RoundToInt (this.gameObject.transform.position.x) + 11 - camMov.locX * 24 ;
					ISLY = Mathf.RoundToInt (this.gameObject.transform.position.y)* -1 + 4 + camMov.locY * 16;
					Debug.Log (ISLY);
					positionInList = ISLX + ISLY * 11;
				}
			}
			Debug.Log ("AAA");
			invBeh.swapPosition = currentPosition;
			held = false;
			stats.holding = false;
			if (pH2 == "0") {
				placeHolder = invBeh.Locations [positionInList];
				invBeh.Locations [positionInList] = invBeh.Locations [currentPosition];
				invBeh.Locations [currentPosition] = placeHolder;
				invBeh.PlaceHolder [positionInList] = invBeh.Locations [positionInList];
				invBeh.PlaceHolder [currentPosition] = invBeh.Locations [currentPosition];
				Debug.Log (placeHolder);
			}
			currentPosition = positionInList;

		}
		if (ISLY == -3) {
			if (stats.pause == 0 & hidden == false) {
				this.gameObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + 1000);
				hidden = true;
			} else if (stats.pause == 1 & hidden == true) {
				this.gameObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y - 1000);
				hidden = false;
			}
		}
		if (currentItem.Substring (3, 3) == "000") {
			invBeh.Locations [currentPosition] = "0000000000";
			Destroy (this.gameObject);
		}
		if (currentPosition == invBeh.MainHandPosition + 77 ) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color(0.8f,0.8f,0.8f,1);
		} else if (currentPosition == invBeh.OffHandPosition + 77) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color(0.6f,0.6f,0.6f,1);
		} else {
			gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
		}

		//Debug.Log (Vector2.Distance(cursor.transform.position, this.gameObject.transform.position));
		//Debug.Log (ISLX + " " + ISLY);
	}
}
