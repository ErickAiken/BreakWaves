using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour{

    [Range(1, 100)]
    public int chunkLength;
    [Range(1, 100)]
    public int chunkWidth;
    [Range(1, 10)]
    public int mapSize;
    [Range(1, 10)]
    public int mapOctaves;
    [Range(1f, 100f)]
    public float mapElevation;
    [Range(1f, 1000f)]
    public float mapSeed;
    [Range(1f, 100f)]
    public float mapRedistribution;
    [Range(1f, 100f)]
    public float mapFrequency;
    [Range(1, 10)]
    public int waterOctaves;
    [Range(1f, 100f)]
    public float waterElevation;
    [Range(1f, 100f)]
    public float waterRedistribution;
    [Range(1f, 100f)]
    public float waterFrequency;
    public Gradient terrainColorGradient;
    public Gradient waterColorGradient;
    public Material mapMaterial;
    public int mapXOrigin;
    [Range(-100f, 100f)]
    public float waterLevel;
    public int mapZOrigin;
    [Range(0,1)]
    public int toggleReset;

    void OnValidate(){

        //Destroy the old gameObjects
        foreach (Transform child in transform){
            //StartCoroutine(Destroy(child.gameObject));
            UnityEditor.EditorApplication.delayCall+=()=>{
                 DestroyImmediate(child.gameObject);
            };
        }//

        //Create the new gameObjects
        for(int i = 0; i != mapSize + 1; i++){
            for(int j = 0; j != mapSize + 1; j++){
                CreateChunkTerrain(i*chunkLength,j*chunkWidth);
                CreateChunkWater(i*chunkLength, waterLevel, j*chunkWidth);
            }//end j
        }//end i

        //Create a border of water
        for(int i = 0; i != mapSize + 2; i++){
            for(int j = 0; j != mapSize + 2; j++){
                //CreateChunkWater(i*chunkLength, j*chun)
            }//end j
        }//end i
    }//

    void Start(){

    }//end Start


    void CreateChunkTerrain(int xOrigin, int zOrigin){
        GameObject chunk = new GameObject("chunkTerrain");
        chunk.gameObject.layer = 3;
        chunk.AddComponent<MeshFilter>();
        chunk.AddComponent<MeshRenderer>();
        chunk.AddComponent<MeshCollider>();
        Mesh chunkMesh = new Mesh();
        Vector3[] vertices = GetVertices(xOrigin + mapXOrigin, 0, zOrigin + mapZOrigin, mapOctaves, mapElevation, mapSeed, mapRedistribution, mapFrequency);
        int[] triangles = GetTriangles();
        Color[] colors = GetColors(vertices, 0f, mapElevation, terrainColorGradient);
        chunkMesh.Clear();
        chunkMesh.vertices = vertices;
        chunkMesh.triangles = triangles;
        chunkMesh.colors = colors;
        chunkMesh.RecalculateNormals();
        chunk.GetComponent<MeshFilter>().mesh = chunkMesh;
        chunk.GetComponent<MeshCollider>().sharedMesh = chunkMesh;
        chunk.GetComponent<MeshRenderer>().material = mapMaterial;
        chunk.transform.parent = transform;
    }//end CreateChunkTerrain



    void CreateChunkWater(int xOrigin, float yOrigin, int zOrigin){
        GameObject chunk = new GameObject("chunkWater");
        chunk.gameObject.layer = 4;
        chunk.AddComponent<MeshFilter>();
        chunk.AddComponent<MeshRenderer>();
        chunk.AddComponent<MeshCollider>();
        Mesh chunkMesh = new Mesh();
        Vector3[] vertices = GetVertices(xOrigin + mapXOrigin, yOrigin, zOrigin + mapZOrigin, waterOctaves, waterElevation, mapSeed, waterRedistribution, waterFrequency);
        int[] triangles = GetTriangles();
        Color[] colors = GetColors(vertices, 0f, mapElevation, waterColorGradient);
        chunkMesh.Clear();
        chunkMesh.vertices = vertices;
        chunkMesh.triangles = triangles;
        chunkMesh.colors = colors;
        chunkMesh.RecalculateNormals();
        chunk.GetComponent<MeshFilter>().mesh = chunkMesh;
        chunk.GetComponent<MeshCollider>().sharedMesh = chunkMesh;
        chunk.GetComponent<MeshRenderer>().material = mapMaterial;
        chunk.transform.parent = transform;
    }//end CreateChunkWater


    Color[] GetColors(Vector3[] vertices, float minTerrainHeight, float maxTerrainHeight, Gradient colorGradient){
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


    Vector3[] GetVertices(int xOrigin, float yOrigin, int zOrigin, int octaves, float elevation, float seed, float redistribution, float frequency){
        Vector3[] vertices = new Vector3[(chunkLength + 1)*(chunkWidth + 1)];
        for(int z = 0, i = 0; z <= chunkWidth; z++){
            for(int x = 0; x <= chunkLength; x++){
                float y = elevation * PerlinNoise.GetNoise(x + xOrigin, z + zOrigin, chunkLength, chunkWidth,
                                                              mapSize, octaves, seed, redistribution,
                                                              frequency);
                y += yOrigin;
                vertices[i] = new Vector3(x + xOrigin,y,z + zOrigin);
                i++;
            }//end x
        }//end z
        return vertices;
    }//end GetVertices

}//end MapGenerator
