using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {

    public GameObject[] mControllers;
    GameObject axis = null;

    public GameObject mSelected = null;
    public int mSelectedIndex;

    public void InitControllers(Vector3[] v)
    {
        mControllers = new GameObject[v.Length];
        for (int i = 0; i < v.Length; i++ )
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            mControllers[i] = sphere;
            mControllers[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            mControllers[i].transform.localPosition = v[i];
            mControllers[i].transform.parent = this.transform;

            sphere.SetActive(false);
        }
    }

    public void InitCylinderControllers(Vector3[] v, int res)
    {
        mControllers = new GameObject[v.Length];
        for (int i = 0; i < v.Length; i++ )
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            if (i % res != 0) {
                sphere.GetComponent<Renderer>().material.color = Color.black;
                sphere.name = "Node";
            }
            else {
                sphere.name = "Sphere" + i;
            }
            mControllers[i] = sphere;
            mControllers[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            mControllers[i].transform.localPosition = v[i];
            mControllers[i].transform.parent = this.transform;
            sphere.SetActive(false);
        }
    }

    public void SelectVertex(GameObject obj) {
        if (obj.name.Contains("Sphere")) {
            mSelected = obj;
            int number;
            int.TryParse(obj.name.Substring(6), out number);
            mSelectedIndex = number;
            Debug.Log(mSelectedIndex);
            if (axis) {
                Destroy(axis.gameObject);
            }
            axis = Instantiate(Resources.Load("VertexAxisFrame")) as GameObject;
            axis.transform.localPosition = obj.transform.localPosition;
        }
    }
}
