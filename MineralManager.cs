using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralManager : MonoBehaviour{
    float minTerrainHeight;
    float maxTerrainHeight;
    public GameObject terrain;
    public Vector3[] mapLocations;
    public int maxNumberRocks;
    public GameObject[] rockType;
    public GameObject[] coalType;
    public GameObject[] goldType;
    public GameObject[] diamondType;
    public float minSpawnElevation;
    public float maxSpawnElevation;
    public float[] rockSize;
    public float[] coalSize;
    public float[] goldSize;
    public float[] diamondSize;


    void Start(){
        minTerrainHeight = terrain.GetComponent<MeshGenerator>().minTerrainHeight;
        maxTerrainHeight = terrain.GetComponent<MeshGenerator>().maxTerrainHeight;
        mapLocations = terrain.GetComponent<MeshGenerator>().vertices;
        SpawnMinerals();
    }//end Start

    // Update is called once per frame
    void Update(){

    }//end Update

    void SpawnMinerals(){
        //Get the possible spaen locations
        int max = mapLocations.Length;

        //Don't add more minerals than there are vertices
        maxNumberRocks = Mathf.Min(maxNumberRocks, max);

        //Loop over minerals to add
        for(int i = 0; i != maxNumberRocks; i++){

            //Get a random spawn location (Check if we already got this value?)
            int n = Random.Range(0, max);
            Vector3 spawnLocation = mapLocations[n];

            //Get the normalized height so we can pair with color map
            float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, spawnLocation.y);

            //Check if random point within elevation threshold
            if(minSpawnElevation <= height && height <= maxSpawnElevation){
                //Get a random rockType and set the size
                int randomRock = Random.Range(0, rockType.Length);
                GameObject rock = rockType[randomRock];
                float size = rockSize[randomRock];
                rock.transform.localScale = new Vector3(size, size, size);

                //Set a random rotation
                float theta = Random.Range(0, 360.0f);

                //Add it to the scene
                Instantiate(rock, spawnLocation, Quaternion.Euler(0.0f, theta, 0.0f));
            }//check height
        }//loop over tree density
    }//end SpawnTrees

}//end TreeManager
