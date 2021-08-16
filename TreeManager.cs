using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour{


    public int maxNumberTrees;
    public GameObject[] treeType;
    public float minSpawnElevation;
    public float maxSpawnElevation;
    public float[] treeSize;

    public void SpawnTree(Vector3 spawnLocation, int index, float theta){
        GameObject tree = treeType[index];
        Instantiate(tree, spawnLocation, Quaternion.Euler(0f, theta, 0f));
    }//

    public void SpawnForest(Vector3[] spawnLocations, float minTerrainHeight, float maxTerrainHeight){

        //Get the possible spaen locations
        int max = spawnLocations.Length;

        //Don't plant more trees than there are vertices
        maxNumberTrees = Mathf.Min(maxNumberTrees, max);

        //Loop over trees to plant
        for(int i = 0; i != maxNumberTrees; i++){

            //Get a random spawn location (Check if we already got this value?)
            int n = Random.Range(0, max);
            Vector3 spawnLocation = spawnLocations[n];

            //Get the normalized height so we can pair with color map
            float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, spawnLocation.y);

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

}//end TreeManager
