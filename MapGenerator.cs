using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour{

    [Range(0, 100)]
    public int chunkLength;
    [Range(0, 100)]
    public int chunkWidth;
    [Range(0, 10)]
    public int mapSize;
    [Range(0, 100)]
    public int mapOctaves;
    [Range(0, 100)]
    public float mapElevation;
    [Range(0, 1000)]
    public float mapSeed;
    [Range(0, 100)]
    public float mapRedistribution;
    [Range(0, 100)]
    public float mapFrequency;
    [Range(0,1)]
    public int toggleReset;
    public Gradient colorGradient;
    public Material mapMaterial;
    public int mapXOrigin;
    public int mapZOrigin;

    void OnValidate(){

        //Destroy the old gameObjects
        foreach (Transform child in transform){
            //StartCoroutine(Destroy(child.gameObject));
            UnityEditor.EditorApplication.delayCall+=()=>{
                 DestroyImmediate(child.gameObject);
            };
        }//

        //Create the new gameObjects
        for(int i = 0; i != mapSize; i++){
            for(int j = 0; j != mapSize; j++){
                CreateChunk(i*chunkLength,j*chunkWidth);
            }//end j
        }//end i
    }//

    void Start(){

    }//end Start


    void CreateChunk(int xOrigin, int zOrigin){
        GameObject chunk = new GameObject("chunk");
        chunk.gameObject.layer = 3;
        chunk.AddComponent<MeshFilter>();
        chunk.AddComponent<MeshRenderer>();
        chunk.AddComponent<MeshCollider>();
        Mesh chunkMesh = new Mesh();
        Vector3[] vertices = GetVertices(xOrigin + mapXOrigin, zOrigin + mapZOrigin);
        int[] triangles = GetTriangles();
        Color[] colors = GetColors(vertices, 0f, mapElevation);
        chunkMesh.Clear();
        chunkMesh.vertices = vertices;
        chunkMesh.triangles = triangles;
        chunkMesh.colors = colors;
        chunkMesh.RecalculateNormals();
        chunk.GetComponent<MeshFilter>().mesh = chunkMesh;
        chunk.GetComponent<MeshCollider>().sharedMesh = chunkMesh;
        chunk.GetComponent<MeshRenderer>().material = mapMaterial;
        chunk.transform.parent = transform;
    }//end CreateChunk


    Color[] GetColors(Vector3[] vertices, float minTerrainHeight, float maxTerrainHeight){
        Color[] colors = new Color[vertices.Length];
        for(int i = 0, z = 0; z <= chunkWidth; z++){
          for(int x = 0; x <= chunkLength; x++){
            float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
              colors[i] = colorGradient.Evaluate(height);
              i++;
          }//end x
        }//end i, z
        return colors;
    }//end GetColors


    int[] GetTriangles(){
        int vert = 0;
        int numTriangles = 0;
        int[] triangles = new int[chunkLength*chunkWidth*6];
        for(int z = 0; z < chunkWidth; z++){
            for(int x = 0; x < chunkLength; x++){
                triangles[numTriangles+0] = vert + 0;
                triangles[numTriangles+1] = vert + chunkLength + 1;
                triangles[numTriangles+2] = vert + 1;
                triangles[numTriangles+3] = vert + 1;
                triangles[numTriangles+4] = vert + chunkLength + 1;
                triangles[numTriangles+5] = vert + chunkLength + 2;
                vert++;
                numTriangles += 6;
            }//end x
            vert++;
        }//end z
        return triangles;
    }//end GetTriangles


    Vector3[] GetVertices(int xOrigin, int zOrigin){
        Vector3[] vertices = new Vector3[(chunkLength + 1)*(chunkWidth + 1)];
        for(int z = 0, i = 0; z <= chunkWidth; z++){
            for(int x = 0; x <= chunkLength; x++){
                float y = mapElevation * PerlinNoise.GetNoise(x + xOrigin, z + zOrigin, chunkLength, chunkWidth,
                                                              mapSize, mapOctaves, mapSeed, mapRedistribution,
                                                              mapFrequency);
                vertices[i] = new Vector3(x + xOrigin,y,z + zOrigin);
                i++;
            }//end x
        }//end z
        return vertices;
    }//end GetVertices

}//end MapGenerator
