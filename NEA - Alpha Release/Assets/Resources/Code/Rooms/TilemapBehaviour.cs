/*Created: Sprint - Last Edited Sprint 
This script’s purpose is to make the water animated for aesthetic purposes. */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapBehaviour : MonoBehaviour {
	public List<TileBase> TilesA = new List<TileBase> ();
	public List<TileBase> TilesB = new List<TileBase> ();
	public Tilemap liquids;
	int cycleA;
	int cycleB;
	int j;
	int k;

	// Use this for initialization
	void Start () {
		TilesA.AddRange(Resources.LoadAll<TileBase>("tilesets/Liquid/water"));
		TilesB.AddRange(Resources.LoadAll<TileBase>("tilesets/Liquid/waterfall"));
		Debug.Log (TilesA.Count);
		liquids = GetComponent<Tilemap> ();
		cycleA = 1;
		cycleB = 1;
		j = 0;
		k = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (j == 20) {
			if (cycleA <= 3) {
				for (int i = TilesA.Count - 2; i >= 0; i -= 1) { 
					liquids.SwapTile (TilesA [i], TilesA [i + 1]);
				}
				cycleA += 1;
			} else {
				cycleA = 1;
				for (int i = 3; i <= TilesA.Count - 1; i += 1) { 
					liquids.SwapTile (TilesA [i], TilesA [i - 3]);
				}
			}
			j = 0;
		}
		if (k == 5) {
			if (cycleB <= 2) {
				for (int i = TilesB.Count - 2; i >= 0; i -= 1) { 
					liquids.SwapTile (TilesB [i], TilesB [i + 1]);
				}
				cycleB += 1;
			} else {
				cycleB = 1;
				for (int i = 2; i <= TilesB.Count - 1; i += 1) { 
					liquids.SwapTile (TilesB [i], TilesB [i - 2]);
				}
			}
			k = 0;
		}
		j += 1;
		k += 1;
	}
}
