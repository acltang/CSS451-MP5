using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle

public partial class MainController : MonoBehaviour
{

    // reference to all UI elements in the Canvas
    public Camera MainCamera = null;
    public MyMesh MyMesh = null;
    public CylinderMesh CylinderMesh = null;
    public Transform Target;
    public VertexAxisFrame axis = null;
    public GameObject AxisFrame;

    public bool MeshActive = true;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(MainCamera != null);
        Debug.Assert(MyMesh != null);
        CylinderMesh.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        axis = (VertexAxisFrame)FindObjectOfType(typeof(VertexAxisFrame));
        MainCamera.transform.LookAt(Target);
        if (!Input.GetKey(KeyCode.LeftAlt)) {
            if (Input.GetKey(KeyCode.LeftControl)) {
                MyMesh.Show();
                CylinderMesh.Show();
                CheckMouseClick();
                CheckMouseHold();
            }
            else {
                if (axis) {
                    axis.AxisDeselected();
                }
                else {
                    MyMesh.Hide();
                    CylinderMesh.Hide();
                }
            }
        }
    }

    public void ProcessUserSelection(String selection) {
        if (selection == "Mesh") {
            MeshActive = true;
            if (axis) {
                Destroy(axis.gameObject);
            }
            if (CylinderMesh) {
                CylinderMesh.gameObject.SetActive(false);
            }
            if (MyMesh) {
                MyMesh.gameObject.SetActive(true);
            }
            AxisFrame.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (selection == "Cylinder") {
            MeshActive = false;
            if (axis) {
                Destroy(axis.gameObject);
            }
            if (MyMesh) {
                MyMesh.gameObject.SetActive(false);
            }
            if (CylinderMesh) {
                CylinderMesh.gameObject.SetActive(true);
            }
            AxisFrame.transform.localPosition = new Vector3(0, 1, 0);
        }
    }
}