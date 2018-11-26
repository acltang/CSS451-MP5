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
    public Transform Target;
    public VertexAxisFrame axis = null;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(MainCamera != null);
        Debug.Assert(MyMesh != null);

        SetSliders();
    }

    // Update is called once per frame
    void Update()
    {
        axis = (VertexAxisFrame)FindObjectOfType(typeof(VertexAxisFrame));
        MainCamera.transform.LookAt(Target);
        if (!Input.GetKey(KeyCode.LeftAlt)) {
            if (Input.GetKey(KeyCode.LeftControl)) {
                CheckMouseClick();
                CheckMouseHold();
            }
            else {
                if (axis) {
                    axis.AxisDeselected();
                }
            }
        }
    }

    void SetSliders() {

    }
}