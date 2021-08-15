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
        for(int i = 0; i != mapSize; i++){
            for(int j = 0; j != mapSize; j++){
                if(i == 0 && j == 0){
                    //continue;
                }//
                Vector3 chunkPosition = new Vector3(i*100f, 0f, j*100f);
                SpawnChunk(chunkPosition);
            }//end j
        }//end i
    }//end Start

    void SpawnChunk(Vector3 chunkPosition){
        GameObject terrain = Instantiate(chunkTerrain, chunkPosition, Quaternion.identity);
        Vector3[] vertices = terrain.GetComponent<MeshGenerator>().vertices;
        float minTerrainHeight = terrain.GetComponent<MeshGenerator>().minTerrainHeight;
        float maxTerrainHeight = terrain.GetComponent<MeshGenerator>().maxTerrainHeight;
        for(int i = 0; i != vertices.Length; i++){
            vertices[i] += chunkPosition;
        }//
        treeManager.GetComponent<TreeManager>().SpawnForest(vertices, minTerrainHeight, maxTerrainHeight);
        Instantiate(chunkWater, chunkPosition, Quaternion.identity);
    }//end SpawnChunk


    void Update(){

    }//end Update
}//end TerrainManager
