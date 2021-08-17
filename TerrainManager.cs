using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour{

    public GameObject chunkTerrain;
    public GameObject chunkWater;
    public GameObject treeManager;
    public int mapSize;

    void Awake(){
        GameData.mapLocations = new Vector3[(GameData.chunkDimension + 1)*(GameData.chunkDimension + 1)*mapSize*mapSize];
    }//end Awake

    void Start(){

        //Generate the grid of terrain
        for(int i = 0; i != mapSize; i++){
            for(int j = 0; j != mapSize; j++){
                Vector3 chunkPosition = new Vector3(i*GameData.chunkDimension, 0f, j*GameData.chunkDimension);
                SpawnChunk(chunkPosition, i*GameData.chunkDimension, j*GameData.chunkDimension);
            }//end j
        }//end i

        //Add trees
        treeManager.GetComponent<TreeManager>().SpawnForest(mapSize);

        //Add rocks

    }//end Start

    void SpawnChunk(Vector3 chunkPosition, int xOrigin, int zOrigin){
        //update the origin so we can smoothly sample the noise between chunks
        chunkTerrain.GetComponent<MeshGenerator>().xOrigin = xOrigin;
        chunkTerrain.GetComponent<MeshGenerator>().zOrigin = zOrigin;
        chunkTerrain.GetComponent<MeshGenerator>().mapSize = mapSize;

        //Instantiate a new terrain object with the given origins
        Instantiate(chunkTerrain, chunkPosition, Quaternion.identity);

        //Add water
        chunkWater.GetComponent<WaterGenerator>().xOrigin = xOrigin;
        chunkWater.GetComponent<WaterGenerator>().zOrigin = zOrigin;
        chunkWater.GetComponent<WaterGenerator>().mapSize = mapSize;
        Instantiate(chunkWater, chunkPosition, Quaternion.identity);
    }//end SpawnChunk

}//end TerrainManager
