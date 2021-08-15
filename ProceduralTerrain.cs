using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrain : MonoBehaviour
{
    Mesh mesh;
    public int xWidth = 30;
    public int zDepth = 30;
    Vector3[] vertices;
    int[] triangles;

    // Start is called before the first frame update
    void Start(){
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateGeometry();
        updateMesh();
    }//

    // Update is called once per frame
    void Update(){

    }//

    private void OnDrawGizmos()
    {
        if (vertices == null) return;
        for (int i = 0; i < vertices.Length; i++){
            Gizmos.DrawSphere(vertices[i], .5f);
        }
    }//

    void CreateGeometry(){
        triangles = new int[xWidth * zDepth * 6];
        int vertexcounter = 0;
        int trianglecount = 0;

        for(int z = 0; z < zDepth; z++){
            for(int x = 0; x < xWidth; x++){
                triangles[0 + trianglecount] = vertexcounter + 0;
                triangles[1 + trianglecount] = vertexcounter + xWidth + 1;
                triangles[2 + trianglecount] = vertexcounter + 1;
                triangles[3 + trianglecount] = vertexcounter + 1;
                triangles[4 + trianglecount] = vertexcounter + xWidth + 1;
                triangles[5 + trianglecount] = vertexcounter + xWidth + 2;
                vertexcounter++;
                trianglecount += 6;
            }//
            vertexcounter++;
        }//

    }//end CreateGeometry

    public void updateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }//

}//end Class
