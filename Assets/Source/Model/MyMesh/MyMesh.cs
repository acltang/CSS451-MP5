using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {
    int resolution = 20;
    // Use this for initialization
    void Start () {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        theMesh.Clear();    // delete whatever is there!!
        Vector3[] v = new Vector3[(resolution * resolution)];   // 2x2 mesh needs 3x3 vertices
        int[] t = new int[(resolution - 1)*(resolution - 1)*2*3];         // Number of triangles: 2x2 mesh and 2x triangles on each mesh-unit
        Vector3[] n = new Vector3[(resolution * resolution)];   // MUST be the same as number of vertices
        Vector2[] uv = new Vector2[(resolution * resolution)];

        //if its 2 +2 if its 3 +1 if its 4 +2/3
        // 2 / resolution -1
        float x = -1;
        float y = -1;
        for(int i = 0; i < (resolution * resolution); i++)
        {
            if(x > 1)
            {
                y += (2 / ((float)resolution - 1));
                x = -1;
            }
            v[i] = new Vector3(x, 0, y);
            x += (2 / ((float)resolution - 1));
        }/*
        v[0] = new Vector3(-1, 0, -1);
        v[1] = new Vector3( 0, 0, -1);
        v[2] = new Vector3( 1, 0, -1);

        v[3] = new Vector3(-1, 0, 0);
        v[4] = new Vector3( 0, 0, 0);
        v[5] = new Vector3( 1, 0, 0);

        v[6] = new Vector3(-1, 0, 1);
        v[7] = new Vector3( 0, 0, 1);
        v[8] = new Vector3( 1, 0, 1);*/

        // UV
        /* uv[0] = new Vector2(0, 0);
         uv[1] = new Vector2(0.5f, 0);
         uv[2] = new Vector2(1, 0);

         uv[3] = new Vector2(0, 0.5f);
         uv[4] = new Vector2(0.5f, 0.5f);
         uv[5] = new Vector2(1, 0.5f);

         uv[6] = new Vector2(0, 1);
         uv[7] = new Vector2(0.5f, 1);
         uv[8] = new Vector2(1, 1);*/

        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = new Vector2(v[i].x, v[i].z);
        }

        for (int i = 0; i < (resolution * resolution); i++)
        {
            n[i] = new Vector3(0, 1, 0);
        }

        // First triangle
        int p = 0;
        int squarenumber = 0;
        for(int i = 0; i < t.Length; i+=6)
        {
            t[i] = 0 + p; t[i+1] = resolution + p; t[i+2] = resolution + 1 + p;
            t[i+3] = 0 + p; t[i+4] = resolution + 1 + p; t[i+5] = 1 + p;
            p++;
            squarenumber++;
            if (squarenumber == resolution-1)
            {
                p++;
                squarenumber = 0;
            }
        }
        /*
        t[0] = 0; t[1] = 3; t[2] = 4;  // 0th triangle
        t[3] = 0; t[4] = 4; t[5] = 1;  // 1st triangle

        t[6] = 1; t[7] = 4; t[8] = 5;  // 2nd triangle
        t[9] = 1; t[10] = 5; t[11] = 2;  // 3rd triangle

        t[12] = 3; t[13] = 6; t[14] = 7;  // 4th triangle
        t[15] = 3; t[16] = 7; t[17] = 4;  // 5th triangle

        t[18] = 4; t[19] = 7; t[20] = 8;  // 6th triangle
        t[21] = 4; t[22] = 8; t[23] = 5;  // 7th triangle
        */
        theMesh.vertices = v; //  new Vector3[3];
        theMesh.triangles = t; //  new int[3];
        theMesh.normals = n;
        theMesh.uv = uv;

        // Second UV (create a slight offset to see the effect!)
        Vector2 offset = new Vector2(0.5f, 0.8f);
        for (int i = 0; i < uv.Length; i++)
            uv[i] += offset;
        theMesh.uv2 = uv;

        InitControllers(v);
        InitNormals(v, n);
    }

    // Update is called once per frame
    void Update () {
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] v = theMesh.vertices;
        Vector3[] n = theMesh.normals;
        for (int i = 0; i<mControllers.Length; i++)
        {
            v[i] = mControllers[i].transform.localPosition;
        }

        ComputeNormals(v, n, resolution);

        theMesh.vertices = v;
        theMesh.normals = n;
	}
}