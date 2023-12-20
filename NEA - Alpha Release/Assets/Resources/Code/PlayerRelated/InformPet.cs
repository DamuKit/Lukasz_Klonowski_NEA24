using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformPet : MonoBehaviour {
	
	GameObject pet;
	PetInteraction petInfo;
	float distance;
	float angle;
	// Use this for initialization

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		try{
			pet = GameObject.FindGameObjectWithTag("Pet");
			RaycastHit2D DetectPlayer = Physics2D.Raycast (this.gameObject.transform.position, pet.transform.position - this.transform.position, 5);
			if(DetectPlayer.collider.tag=="Pet"){
				petInfo = DetectPlayer.collider.GetComponent<PetInteraction> ();


				distance = Mathf.Sqrt(Mathf.Pow(DetectPlayer.collider.transform.position.x - this.gameObject.transform.position.x, 2) + Mathf.Pow(DetectPlayer.collider.transform.position.y - this.gameObject.transform.position.y, 2));

				//angle = enemy.angle;

				angle = Mathf.Rad2Deg * (Mathf.Atan (Mathf.Abs ((0.5f + 0.5f * Mathf.Sign (this.transform.position.x - pet.transform.position.x) * Mathf.Sign (this.transform.position.y - pet.transform.position.y)) * (this.transform.position.x - pet.transform.position.x) / (this.transform.position.y - pet.transform.position.y) + ((0.5f + 0.5f * -Mathf.Sign (this.transform.position.x - pet.transform.position.x) * Mathf.Sign (this.transform.position.y - pet.transform.position.y))) * (this.transform.position.y - pet.transform.position.y) / (this.transform.position.x - pet.transform.position.x)))) + 45 * (2 - 2 * Mathf.Sign (this.transform.position.x - pet.transform.position.x) + 1 - Mathf.Sign (this.transform.position.x - pet.transform.position.x) * Mathf.Sign (this.transform.position.y - pet.transform.position.y));

				DetectPlayer.collider.GetComponent<PetInteraction>().BroadcastMessage("Focus", new float[] {distance, angle});

			}
		}
		catch{
		}
	}

}
