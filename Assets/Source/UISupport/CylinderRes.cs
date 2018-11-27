using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderRes : MonoBehaviour {
    public SliderWithEcho X;
    public CylinderMesh MyMesh;
    //Use this for initialization
    void Start()
    {
        X.SetSliderListener(XValueChanged);
        X.InitSliderRange(4, 20, MyMesh.resolution);
    }

    void XValueChanged(float v)
    {
        MyMesh.cResolution = (int)v;
        MyMesh.GenerateCylinder();
    }
}
