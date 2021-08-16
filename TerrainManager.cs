using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour{

    public GameObject chunkTerrain;
    public GameObject chunkWater;
    public GameObject treeManager;
    public Vector3[,] vertices;
    public int mapSize;

    void Start(){
        if(mapSize < 0){
            mapSize = 0;
        }//

        //Initialize the gameData maplocations
        GameData.mapLocations = new Vector3[(GameData.chunkDimension + 1)*(GameData.chunkDimension + 1)*mapSize*mapSize];

        for(int i = 0; i != mapSize; i++){
            for(int j = 0; j != mapSize; j++){
                Vector3 chunkPosition = new Vector3(i*GameData.chunkDimension, 0f, j*GameData.chunkDimension);
                SpawnChunk(chunkPosition, i*GameData.chunkDimension, j*GameData.chunkDimension);
            }//end j
        }//end i

    }//end Start

    void SpawnChunk(Vector3 chunkPosition, int xOrigin, int zOrigin){
        //update the origin so we can smoothly sample the noise between chunks
        chunkTerrain.GetComponent<MeshGenerator>().xOrigin = xOrigin;
        chunkTerrain.GetComponent<MeshGenerator>().zOrigin = zOrigin;
        chunkTerrain.GetComponent<MeshGenerator>().mapSize = mapSize;

        //Instantiate a new terrain object with the given origins
        GameObject terrain = Instantiate(chunkTerrain, chunkPosition, Quaternion.identity);
        Vector3[] vertices = terrain.GetComponent<MeshGenerator>().vertices;
        float minTerrainHeight = terrain.GetComponent<MeshGenerator>().minTerrainHeight;
        float maxTerrainHeight = terrain.GetComponent<MeshGenerator>().maxTerrainHeight;
        for(int i = 0; i != vertices.Length; i++){
            vertices[i] += chunkPosition;
        }//

        //Get the vertices from the instantiated chunk and spawn trees
        treeManager.GetComponent<TreeManager>().SpawnForest(vertices, minTerrainHeight, maxTerrainHeight);

        //Add water
        chunkWater.GetComponent<WaterGenerator>().xOrigin = xOrigin;
        chunkWater.GetComponent<WaterGenerator>().zOrigin = zOrigin;
        chunkWater.GetComponent<WaterGenerator>().mapSize = mapSize;
        Instantiate(chunkWater, chunkPosition, Quaternion.identity);
    }//end SpawnChunk


    void Update(){

    }//end Update
}//end TerrainManager
