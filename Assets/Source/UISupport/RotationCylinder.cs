using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCylinder : MonoBehaviour {
    public SliderWithEcho X;
    public CylinderMesh MyMesh;
    //Use this for initialization
    void Start()
    {
        X.SetSliderListener(XValueChanged);
        X.InitSliderRange(10, 360, 275);
    }

    void XValueChanged(float v)
    {
        MyMesh.degrees = v;
        MyMesh.GenerateCylinder();
    }
}
