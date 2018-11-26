using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionControl : MonoBehaviour {
    public SliderWithEcho X;
    public MyMesh MyMesh;
     //Use this for initialization
    void Start () {
        X.SetSliderListener(XValueChanged);
        X.InitSliderRange(2, 20, MyMesh.resolution);
    }
	
    void XValueChanged(float v)
    {
        MyMesh.resolution = (int)v;
        MyMesh.UpdateResolution();
    }
    

}
