/*Created: Sprint 7 - Last Edited Sprint 7
This script’s purpose is to display the amount of an item in the inventory. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemAmount : MonoBehaviour {
	public TMP_Text amount;
	public InventoryBehaviour invBeh;
	public ItemMoving location;

	// Use this for initialization
	void Start () {
		amount = this.GetComponent<TMPro.TMP_Text> ();
		invBeh = this.transform.parent.parent.GetComponent<InventoryBehaviour>();
		location = GetComponentInParent<ItemMoving>();
	}
	
	// Update is called once per frame
	void Update () {
		try{
		amount.SetText(int.Parse(invBeh.Locations[location.currentPosition].Substring(3,3)).ToString());
		}
		catch{
			amount.SetText ("");
		}
	}
}
