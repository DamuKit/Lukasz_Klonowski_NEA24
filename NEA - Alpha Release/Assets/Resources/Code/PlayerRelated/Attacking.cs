using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour {
	public PlayerMovement playerMovement;
	public InventoryBehaviour invBeh;
	public StatsStorage stats;
	public bool Attack;
	public float cooldownLim;
	public float counter;
	public float attackduration;
	public float damage;
	public float damagebuff;
	public float weaponDamage;
	public bool shieldWield;
	bool dashtest;
	public bool cooldown;
	Vector3 Position;
	public float[] ProjectileStats = new float[] {0,0,0,0,0,0};
	// Use this for initialization
	void Start () {
		shieldWield = false;
		stats = GameObject.Find ("PassiveCodeController").GetComponent<StatsStorage> ();
		playerMovement = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		invBeh =  GameObject.Find ("Inventory").GetComponent<InventoryBehaviour> ();
		Attack = false;
		cooldownLim = 0;
		attackduration = 0.5f;
		damage = 5;
		damagebuff = 0;
		weaponDamage = 0;
		dashtest = false;
		cooldown = false;



	}
	
	// Update is called once per frame
	void Update () {
		dashtest = false;
		try{

			if(invBeh.Locations [invBeh.OffHandPosition + 77].Substring (0, 3) == "101"){
				dashtest = true;
			}
		}
		catch{
		}
		try{
			if(invBeh.Locations [invBeh.MainHandPosition + 77].Substring (0, 3) == "101"){
				dashtest = true;
			}
		}
		catch{
		}
		if (dashtest == true) {
			shieldWield = true;
		} else {
			shieldWield = false;
			Debug.Log (invBeh.Locations [invBeh.OffHandPosition + 77] + invBeh.Locations [invBeh.MainHandPosition + 77]);
		}
		//Debug.Log (playerMovement.angle + "e");
		this.gameObject.transform.rotation = Quaternion.identity;
		this.gameObject.transform.Rotate (0, 0, -playerMovement.angle + 180);

		//Debug.Log (counter);
		if (counter <=0 && Attack == false & playerMovement.hp >0 & stats.pause == 1) {
			if (Input.GetKeyDown (KeyCode.Mouse0) == true) {
				Interact (invBeh.MainHandPosition + 77);
			}
			else if(Input.GetKeyDown (KeyCode.Mouse1) == true){
				Interact(invBeh.OffHandPosition + 77);
			}
		}
		if (Attack == true) {
			
			if (counter <= 0) {
				Attack = false;
				counter = cooldownLim; 
			}
		}
			if(counter >0){
				counter -= 1 * Time.deltaTime;
			}

	}
	public void Interact(int slot){
		try{
			switch(invBeh.Locations[slot].Substring(0,1)){
			case("0"):
				if(invBeh.Locations[slot].Substring(0,3) == "000"){
				}
				else{
					playerMovement.SendMessage ("itemEffect", int.Parse(invBeh.Locations[slot].Substring(0,3)));
					invBeh.Locations[slot] = invBeh.Locations[slot].Substring(0,3) + (int.Parse(invBeh.Locations[slot].Substring(3,3)) - 1 + 2000).ToString().Substring(1,3);
				}
				break;
			case("1"):
				if(cooldown == false){
					if(invBeh.Locations[slot].Substring(0,3) == "100"){
						Attack = true;
						counter = attackduration;
						playerMovement.SendMessage ("attack");
						weaponDamage = Mathf.Pow(10,int.Parse(invBeh.Locations[slot].Substring(4,1))) * int.Parse(invBeh.Locations[slot].Substring(5,3));
					}
					if(invBeh.Locations[slot].Substring(0,3) == "102"){
						cooldown = false;
						StartCoroutine ("Cooldown", 1f);
						Debug.Log("A");
						ProjectileStats = new float[] {Mathf.Pow(10,int.Parse(invBeh.Locations[slot].Substring(4,1))) * int.Parse(invBeh.Locations[slot].Substring(5,3)),10,10,0,0,1};
						Position = this.gameObject.transform.position + new Vector3(Mathf.Sin(playerMovement.angle * Mathf.Deg2Rad) * 0.55f, Mathf.Cos(playerMovement.angle * Mathf.Deg2Rad) * 0.25f, 0);
						Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles/P000Bullet"), Position, Quaternion.identity);
						//Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles/P000Bullet"), Position, Quaternion.identity);
						//Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles/P000Bullet"), Position, Quaternion.identity);
						//Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles/P000Bullet"), Position, Quaternion.identity);
						//Instantiate(Resources.Load<GameObject>("Prefabs/Projectiles/P000Bullet"), Position, Quaternion.identity);
					}
				}
				break;
			case("2"):
				break;
			default:
				break;
			}
		}
		catch{}
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			if (Attack == true & playerMovement.dashing == false) {
				other.gameObject.SendMessage ("damaged", (weaponDamage + damage) * (1 + damagebuff * 0.25f));
			} else if (playerMovement.dashing == true) {
				other.gameObject.SendMessage ("damaged", damage * (0.5f + damagebuff * 0.125f));
			}
			//Destroy (other.gameObject);
		}
	}

	public IEnumerator Cooldown(float time){
		cooldown = true;
		yield return new WaitForSeconds (time);
		cooldown = false;
	}
}
