using UnityEngine;
using System.Collections;
using Utility;

public class myTerrainManager : MonoBehaviour {

	public GameObject player;
	public Terrain refTerrain;
	public int spread = 2;

	private int TERRAIN_BUFFER_COUNT = 50;
	private int[] currentTerrainID;
	private Vector3 referencePosition;
	private Vector2 referenceSize;
	private BitArray usedTiles;

	private DoubleKeyDictionary<int, int, int> terrainList = new DoubleKeyDictionary<int, int, int>();

	void Start(){
		currentTerrainID = new int[2];
		usedTiles = new BitArray(TERRAIN_BUFFER_COUNT, false);
		referencePosition = refTerrain.transform.position;
		//referenceRotation = refTerrain.transform.rotation;
		referenceSize = new Vector2(refTerrain.terrainData.size.x, refTerrain.terrainData.size.z);
	}

	void Update(){
		Vector3 warpPosition = player.transform.position;
		TerrainIDFromPosition(ref currentTerrainID, ref warpPosition);

		for(int i=-spread;i<=spread;i++)
		{
			for(int j=-spread;j<=spread;j++)
			{	
				DropTerrainAt(currentTerrainID[0] + i, currentTerrainID[1] + j);
			}
		}
	}

	void DropTerrainAt(int i, int j){
		// Check if terrain exists, if it does, activate it.
		if(terrainList.ContainsKey(i, j) && terrainList[i,j] != -1)
		{
			// Tile mapped, use it.
		}
		// If terrain doesn't exist, drop it.
		else
		{
			terrainList[i,j] = FindNextAvailableTerrainID();
			if(terrainList[i,j] == -1) Debug.LogError("No more tiles, failing...");
		}		
		usedTiles[terrainList[i,j]] = true;
	}

	void TerrainIDFromPosition(ref int[] currentTerrainID, ref Vector3 position)
	{
		currentTerrainID[0] = Mathf.RoundToInt((position.x - referencePosition.x )/ referenceSize.x);
		currentTerrainID[1] = Mathf.RoundToInt((position.z - referencePosition.z )/ referenceSize.y);
	}

	int FindNextAvailableTerrainID()
	{
		for(int i=0;i<usedTiles.Length;i++)
			if(!usedTiles[i]) return i;
		return -1;	
	}
}
