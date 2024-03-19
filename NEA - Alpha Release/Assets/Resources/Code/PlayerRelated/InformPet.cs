/*Created: Sprint 7 - Last Edited Sprint 8
This script’s purpose is to allow the pet to know the location of enemies. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformPet : MonoBehaviour {
	
	GameObject pet;
	PetInteraction petInfo;
	float distance;
	float angle;
	Vector3 position;
	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		try{
			pet = GameObject.FindGameObjectWithTag("Pet");

			position = pet.transform.position + new Vector3(0,-0.25f,0);

			RaycastHit2D DetectPet = Physics2D.Raycast (this.gameObject.transform.position, position - this.transform.position , 15);
			//Debug.DrawRay(this.gameObject.transform.position, position - this.transform.position , Color.blue, 15);
			//Debug.Log(DetectPet.collider.name);
			if(DetectPet.collider.tag=="Pet"){
				petInfo = DetectPet.collider.GetComponent<PetInteraction> ();


				distance = Mathf.Sqrt(Mathf.Pow(DetectPet.collider.transform.position.x - this.gameObject.transform.position.x, 2) + Mathf.Pow(DetectPet.collider.transform.position.y - this.gameObject.transform.position.y, 2));
				//angle = Vector2.Angle(DetectPet.collider.transform.position, this.gameObject.transform.position);


				//angle = enemy.angle;

				angle = Mathf.Rad2Deg * (Mathf.Atan (Mathf.Abs ((0.5f + 0.5f * Mathf.Sign (this.transform.position.x - pet.transform.position.x) * Mathf.Sign (this.transform.position.y - pet.transform.position.y)) * (this.transform.position.x - pet.transform.position.x) / (this.transform.position.y - pet.transform.position.y) + ((0.5f + 0.5f * -Mathf.Sign (this.transform.position.x - pet.transform.position.x) * Mathf.Sign (this.transform.position.y - pet.transform.position.y))) * (this.transform.position.y - pet.transform.position.y) / (this.transform.position.x - pet.transform.position.x)))) + 45 * (2 - 2 * Mathf.Sign (this.transform.position.x - pet.transform.position.x) + 1 - Mathf.Sign (this.transform.position.x - pet.transform.position.x) * Mathf.Sign (this.transform.position.y - pet.transform.position.y));

				DetectPet.collider.GetComponent<PetInteraction>().BroadcastMessage("Focus", new float[] {distance, angle});

			}
		}
		catch{
		}
	}

}
