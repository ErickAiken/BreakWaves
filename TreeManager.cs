using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour{

    //Tree parameters
    public int maxNumberTrees;
    public GameObject[] treeType;
    public float minSpawnElevation;
    public float maxSpawnElevation;
    public float[] treeSize;

    //Noise parameters
    public int seed = 1;
    public int octaves = 1;
    public float redistribution = 1.0f;
    public float frequency = 1.0f;

    public void SpawnTree(Vector3 spawnLocation, int index, float theta){
        GameObject tree = treeType[index];
        Instantiate(tree, spawnLocation, Quaternion.Euler(0f, theta, 0f));
    }//

    public void SpawnForest(int mapSize){

        //Loop over trees to plant
        for(int i = 0; i != maxNumberTrees; i++){

            //Get a random spawn location (Check if we already got this value?)
            int randomPoint = Random.Range(0, GameData.mapLocations.Length - 1);
            Vector3 spawnLocation = GameData.mapLocations[randomPoint];

            //Get the normalized height so we can pair with color map
            float height = Mathf.InverseLerp(GameData.minTerrainHeight, GameData.maxTerrainHeight, spawnLocation.y);

            //Check if random point within elevation threshold
            if(minSpawnElevation <= height && height <= maxSpawnElevation){
                //Get a random tree treeType and set the size
                int randomTree = Random.Range(0, treeType.Length);
                GameObject tree = treeType[randomTree];
                float size = treeSize[randomTree];
                tree.transform.localScale = new Vector3(size, size, size);

                //Set a random rotation
                float theta = Random.Range(0, 360.0f);

                //Sink the tree slightly in the ground
                spawnLocation[1] -= 0.5f;

                //Add it to the scene
                Instantiate(tree, spawnLocation, Quaternion.Euler(0.0f, theta, 0.0f));
            }//check height
        }//loop over tree density
    }//end SpawnForest


    private float GetNoise(int x, int z, int mapSize){
        float sampleX = (float)(x)/(float)(GameData.chunkDimension*mapSize) - 0.01f;
        float sampleZ = (float)(z)/(float)(GameData.chunkDimension*mapSize) - 0.01f;
        float val = 0.0f;
        float A = 0.0f;
        for(int i = 0; i != octaves; i++){
            float div = Mathf.Pow(2.0f, i);
            A += (1/div);
            val += (1/div)*Mathf.PerlinNoise((frequency*div)*sampleX + seed,
                                             (frequency*div)*sampleZ + seed);
        }//
        float B = Mathf.Pow(val/A, redistribution);
        return B;
    }//end GetNoise

}//end TreeManager
