using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {
    LineSegment[] mNormals;

    public void InitNormals(Vector3[] v, Vector3[] n)
    {
        mNormals = new LineSegment[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            mNormals[i] = o.AddComponent<LineSegment>();
            mNormals[i].SetWidth(0.01f);
            mNormals[i].transform.SetParent(this.transform);
        }
        UpdateNormals(v, n);
    }

    void UpdateNormals(Vector3[] v, Vector3[] n)
    {
        for (int i = 0; i < v.Length; i++)
        {
            mNormals[i].SetEndPoints(v[i], v[i] + 0.5f * n[i]);
        }
    }

    Vector3 FaceNormal(Vector3[] v, int i0, int i1, int i2)
    {
        Vector3 a = v[i1] - v[i0];
        Vector3 b = v[i2] - v[i0];
        return Vector3.Cross(a, b).normalized;
    }

    public void ComputeNormals(Vector3[] v, Vector3[] n, int res)
    {
          Vector3[] triNormal = new Vector3[(res - 1) * (res - 1) * 2];
          Vector3[] verticies = new Vector3[(res - 1) * (res - 1) * 2];
          int p = 0;
          int squarenumber = 0;
          for (int i = 0; i < triNormal.Length; i += 2)
          {
              triNormal[i] = FaceNormal(v, 0 + p, res + p, res + 1 + p);
              triNormal[i + 1] = FaceNormal(v, 0 + p, res + 1 + p, 1 + p);
              verticies[i].x = 0 + p;
              verticies[i].y = res + p;
              verticies[i].z = res + 1 + p;
              verticies[i + 1].x = 0 + p;
              verticies[i + 1].y = res + 1 + p;
              verticies[i + 1].z = 1 + p;
              p++;
              squarenumber++;
              if (squarenumber == res - 1)
              {
                  p++;
                  squarenumber = 0;
              }
          }

          //run through each triangle and if the vertice is in that triangle add it to be normalized
          for (int i = 0; i < res * res; i++)
          {
              for(int j = 0; j < verticies.Length; j++)
              {
                  if(verticies[j].x == i)
                  {
                      n[i] = (triNormal[j] + n[i]).normalized;
                  }
                  if (verticies[j].y == i)
                  {
                      n[i] = (triNormal[j] + n[i]).normalized;
                  }
                  if (verticies[j].z == i)
                  {
                      n[i] = (triNormal[j] + n[i]).normalized;
                  }
              }
          }
        UpdateNormals(v, n);
           
    }
}
