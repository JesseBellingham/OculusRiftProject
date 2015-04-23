using UnityEngine;
using System.Collections;
using Utility;

public class myTerrainManager : MonoBehaviour {

	public GameObject player;
	public Terrain refTerrain;
	public int spread = 2;
	public int TERRAIN_BUFFER_COUNT = 50;

	private int[] currentTerrainID;
    private Terrain[] terrainBuffer;
	private Vector3 referencePosition;
	private Vector2 referenceSize;
    private Quaternion referenceRotation;
	private BitArray usedTiles;

	private DoubleKeyDictionary<int, int, int> terrainList = new DoubleKeyDictionary<int, int, int>();
    //private DoubleKeyDictionary<int, int, Terrain> terrainUsage = new DoubleKeyDictionary<int, int, Terrain>();

	void Start(){
		currentTerrainID = new int[2];
        terrainBuffer = new Terrain[TERRAIN_BUFFER_COUNT];
		usedTiles = new BitArray(TERRAIN_BUFFER_COUNT, false);
		referencePosition = refTerrain.transform.position;
		referenceRotation = refTerrain.transform.rotation;
		referenceSize = new Vector2(refTerrain.terrainData.size.x, refTerrain.terrainData.size.z);

        for (int i = 0; i < TERRAIN_BUFFER_COUNT; i++)
        {
            TerrainData tData = new TerrainData();
            CopyTerrainDataFromTo(refTerrain.terrainData, tData);
            terrainBuffer[i] = Terrain.CreateTerrainGameObject(tData).GetComponent<Terrain>();
            terrainBuffer[i].gameObject.SetActive(false);
        }
	}    

	void Update(){
		Vector3 warpPosition = player.transform.position;
		TerrainIDFromPosition(ref currentTerrainID, ref warpPosition);
        //Debug.Log(currentTerrainID.ToString() + ", " + warpPosition.ToString());

		for(int i=-spread;i<=spread;i++)
		{
			for(int j=-spread;j<=spread;j++)
			{	
				DropTerrainAt(currentTerrainID[0] + i, currentTerrainID[1] + j);
                ReclaimTiles(i, j);
			}
		}
        
	}

    void ReclaimTiles(int i, int j)
    {
        Terrain[] floor = GameObject.FindObjectsOfType<Terrain>();
        Vector3 pos = new Vector3(referencePosition.x + i * referenceSize.x, referencePosition.y, referencePosition.z + j * referenceSize.y);
        foreach (Terrain tile in floor)
        {
            if (tile.GetPosition() != pos)
            {
                tile.gameObject.SetActive(false);
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
        ActivateUsedTile(i, j);
		usedTiles[terrainList[i,j]] = true;
	}

    private void ActivateUsedTile(int i, int j)
    {
        terrainBuffer[terrainList[i, j]].gameObject.transform.position = 
            new Vector3(referencePosition.x + i * referenceSize.x, referencePosition.y, referencePosition.z + j * referenceSize.y);

        terrainBuffer[terrainList[i, j]].transform.rotation = referenceRotation;
        terrainBuffer[terrainList[i, j]].gameObject.SetActive(true);
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

    private void CopyTerrainDataFromTo(TerrainData tDataFrom, TerrainData tDataTo)
    {
        tDataTo.SetDetailResolution(tDataFrom.detailResolution, 8);
        tDataTo.heightmapResolution = tDataFrom.heightmapResolution;
        tDataTo.alphamapResolution = tDataFrom.alphamapResolution;
        tDataTo.baseMapResolution = tDataFrom.baseMapResolution;
        tDataTo.size = tDataFrom.size;
        tDataTo.splatPrototypes = tDataFrom.splatPrototypes;
    }
}
