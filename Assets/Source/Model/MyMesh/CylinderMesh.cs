using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderMesh : MyMesh {

	public int cResolution = 10;

	public float degrees = 250f;
	private float rotation;

    // Use this for initialization
    void Start () {
		rotation = degrees * Mathf.Deg2Rad;
		InitializeMesh(cResolution);
		GenerateCylinder();
    }

    // Update is called once per frame
    void Update () {
		rotation = degrees * Mathf.Deg2Rad;
		Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] v = theMesh.vertices;
        Vector3[] n = theMesh.normals;
        for (int i = 0; i<mControllers.Length; i++)
        {
            v[i] = mControllers[i].transform.localPosition;
        }

        ComputeNormals(v, n, cResolution);

        theMesh.vertices = v;
        theMesh.normals = n;
	}

	public void GenerateCylinder() {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        InitializeMesh(cResolution);
		int vertex = 0;
        for (int i = 0; i < cResolution; i++)
        {
			for (int j = 0; j < cResolution; j++) {
				v[vertex] = new Vector3(0, 2.0f * i / (float)cResolution, 0);
				v[vertex].x = 0.5f * Mathf.Cos(j * rotation / cResolution);
				v[vertex].z = 0.5f * Mathf.Sin(j * rotation / cResolution);

				int p = 0;
        		int squarenumber = 0;
        		for(int k = 0; k < t.Length; k+=6) {
            		t[k] = 0 + p; t[k+1] = cResolution + p; t[k+2] = cResolution + 1 + p;
           		 	t[k+3] = 0 + p; t[k+4] = cResolution + 1 + p; t[k+5] = 1 + p;
            		p++;
            		squarenumber++;
            		if (squarenumber == cResolution-1) {
                		p++;
                		squarenumber = 0;
            		}
				}
				vertex++;
			}
        }

		for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = new Vector2(v[i].x, v[i].z);
        }

        for (int i = 0; i < (cResolution * cResolution); i++)
        {
            n[i] = new Vector3(0, 1, 0);
        }

		theMesh.vertices = v;
        theMesh.triangles = t;
        theMesh.normals = n;
        theMesh.uv = uv;

		Vector2 offset = new Vector2(0.5f, 0.8f);
        for (int i = 0; i < uv.Length; i++)
            uv[i] += offset;
        theMesh.uv2 = uv;

		InitCylinderControllers(v, cResolution);
        InitNormals(v, n);
	}
}
