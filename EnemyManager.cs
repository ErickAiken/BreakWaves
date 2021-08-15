using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour{

    public GameObject enemy;
    public GameObject terrain;
    public Vector3[] mapLocations;

    void Start(){
        mapLocations = terrain.GetComponent<MeshGenerator>().vertices;
        SpawnNewEnemy();
    }//end Start

    void SpawnNewEnemy(){
        int max = mapLocations.Length;
        int n = Random.Range(0, max);
        Vector3 spawnLocation = mapLocations[n];
        spawnLocation[1] += 5.0f;
        //GameObject obj = Instantiate(enemy, spawnLocation, Quaternion.identity) as GameObject;
    }//end SpawnNewEnemy


}//end EnemyManager
