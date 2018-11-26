using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {
    public int resolution = 2;

    protected Mesh theMesh;
	protected Vector3[] v;
    protected int[] t;
	protected Vector3[] n;
    protected Vector2[] uv;

    // Use this for initialization
    void Start () {
        InitializeMesh(resolution);
        UpdateResolution();
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

    public void InitializeMesh(int res) {
        theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        theMesh.Clear();    // delete whatever is there!!
        v = new Vector3[(res * res)];   // 2x2 mesh needs 3x3 vertices
        t = new int[(res - 1)*(res - 1)*2*3];         // Number of triangles: 2x2 mesh and 2x triangles on each mesh-unit
        n = new Vector3[(res * res)];   // MUST be the same as number of vertices
        uv = new Vector2[(res * res)];
    }

    public void UpdateResolution() {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        InitializeMesh(resolution);
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
        }

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

    public void Show() {
        for (int i = 0; i < mControllers.Length; i++) {
            mControllers[i].SetActive(true);
        }

        for (int i = 0; i < mNormals.Length; i++) {
            mNormals[i].gameObject.SetActive(true);
        }
    }

    public void Hide() {
        for (int i = 0; i < mControllers.Length; i++) {
            mControllers[i].SetActive(false);
        }

        for (int i = 0; i < mNormals.Length; i++) {
            mNormals[i].gameObject.SetActive(false);
        }
    }
}