using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {

    GameObject[] mControllers;
    GameObject axis = null;

    public GameObject mSelected = null;

    void InitControllers(Vector3[] v)
    {
        mControllers = new GameObject[v.Length];
        for (int i =0; i<v.Length; i++ )
        {
            mControllers[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            mControllers[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            mControllers[i].transform.localPosition = v[i];
            mControllers[i].transform.parent = this.transform;
        }
    }

    public void SelectVertex(GameObject obj) {
        if (obj.name.Equals("Sphere")) {
            mSelected = obj;
            if (axis) {
                Destroy(axis);
            }
            axis = Instantiate(Resources.Load("VertexAxisFrame")) as GameObject;
            axis.transform.localPosition = obj.transform.localPosition;
        }
    }
}
