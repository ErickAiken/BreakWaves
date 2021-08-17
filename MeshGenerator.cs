using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {

    public Mesh mesh;
    public Vector3[] vertices;
    int[] triangles;
    Color[] colors;
    private MeshCollider meshCollider;

    public int mapSize;
    public int xOrigin = 0;
    public int zOrigin = 0;
    public int elevation = 10;
    public int seed = 1;
    public int octaves = 1;
    public float redistribution = 1.0f;
    public float frequency = 1.0f;
    public bool debug = false;
    public float minTerrainHeight;
    public float maxTerrainHeight;
    public Gradient colorGradient;

    void Awake(){
        mesh = new Mesh();
        meshCollider = GetComponent<MeshCollider>();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }//end Start

    void CreateShape(){
        vertices = new Vector3[(GameData.chunkDimension + 1)*(GameData.chunkDimension + 1)];
        for(int z = 0, i = 0; z <= GameData.chunkDimension; z++){
            for(int x = 0; x <= GameData.chunkDimension; x++){
                float y = GetNoise(x + xOrigin, z + zOrigin);
                vertices[i] = new Vector3(x,y,z);
                GameData.mapLocations[GameData.mapIndex] = new Vector3(x + xOrigin, y, z + zOrigin);
                GameData.mapIndex++;
                if(y < minTerrainHeight){
                  minTerrainHeight = y;
                }//
                if(y > maxTerrainHeight){
                  maxTerrainHeight = y;
                }//
                if(y < GameData.minTerrainHeight){
                  GameData.minTerrainHeight = y;
                }//
                if(y > GameData.maxTerrainHeight){
                  GameData.maxTerrainHeight = y;
                }//
                i++;
            }//end x
        }//end z

        int vert = 0;
        int numTriangles = 0;
        triangles = new int[GameData.chunkDimension*GameData.chunkDimension*6];
        for(int z = 0; z < GameData.chunkDimension; z++){
            for(int x = 0; x < GameData.chunkDimension; x++){
                triangles[numTriangles+0] = vert + 0;
                triangles[numTriangles+1] = vert + GameData.chunkDimension + 1;
                triangles[numTriangles+2] = vert + 1;
                triangles[numTriangles+3] = vert + 1;
                triangles[numTriangles+4] = vert + GameData.chunkDimension + 1;
                triangles[numTriangles+5] = vert + GameData.chunkDimension + 2;
                vert++;
                numTriangles += 6;
            }//end x
            vert++;
        }//end z

        //Handle coloring the terrain
        colors = new Color[vertices.Length];
        for(int i = 0, z = 0; z <= GameData.chunkDimension; z++){
          for(int x = 0; x <= GameData.chunkDimension; x++){
            float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
              colors[i] = colorGradient.Evaluate(height);
              i++;
          }//end x
        }//end i, z
    }//end CreateShape

    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }//end UpdateMesh


    private void OnDrawGizmos(){
        if(vertices == null){
            return;
        }//
        if(debug){
            for(int i = 0; i != vertices.Length; i++){
                Gizmos.DrawSphere(vertices[i], 0.1f);
            }//end i
        }//
    }//end OnDrawGizmos


    private float GetNoise(int x, int z){
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
        return B*elevation;
    }//end GetNoise


}//end MeshGenerator
