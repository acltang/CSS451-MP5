using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class XfromControl : MonoBehaviour {
    public Toggle T, R, S;
    public SliderWithEcho X, Y, Z;
    public Text ObjectName;
    private float tx = 0;
    private float ty = 0;
    private float sx = 1;
    private float sy = 1;
    private Vector2[] uvMain;
    public MyMesh MyMesh;
    private Vector3 mPreviousSliderValues = Vector3.zero;

	// Use this for initialization
	void Start () {
        Mesh theMesh = MyMesh.GetComponent<MeshFilter>().mesh;
        uvMain = theMesh.uv;
        T.onValueChanged.AddListener(SetToTranslation);
        R.onValueChanged.AddListener(SetToRotation);
        S.onValueChanged.AddListener(SetToScaling);
        X.SetSliderListener(XValueChanged);
        Y.SetSliderListener(YValueChanged);
        Z.SetSliderListener(ZValueChanged);
        T.isOn = true;
        R.isOn = false;
        S.isOn = false;
        SetToTranslation(true);
	}
	
    void SetToTranslation(bool v)
    {
        if(T.isOn){
            S.isOn = false;
            Vector3 p = GetSelectedXformParameter();
            mPreviousSliderValues = p;
            X.InitSliderRange(-4, 4, p.x);
            Y.InitSliderRange(-4, 4, p.y);
            Z.InitSliderRange(0, 0, p.z);
        }
    }

    void SetToScaling(bool v)
    {
        if (S.isOn)
        {
            T.isOn = false;
            Vector3 s = GetSelectedXformParameter();
            mPreviousSliderValues = s;
            X.InitSliderRange(0.1f, 10f, s.x);
            Y.InitSliderRange(0.1f, 10f, s.y);
            Z.InitSliderRange(1f, 1, s.z);
        }
    }

    void SetToRotation(bool v)
    {
        if (R.isOn)
        {
            Vector3 r = GetSelectedXformParameter();
            mPreviousSliderValues = r;
            X.InitSliderRange(0, 0, r.x);
            Y.InitSliderRange(0, 0, r.y);
            Z.InitSliderRange(-180, 180, r.z);
            mPreviousSliderValues = r;
        }
    }

    void XValueChanged(float v)
    {
        // if not in rotation, next two lines of work would be wasted
        float dx = v - mPreviousSliderValues.x;
        mPreviousSliderValues.x = v;
        Vector2 b = GetSelectedXformParameter();
        b.x = v;
        SetSelectedXform(ref b, ref dx);
    }
    
    void YValueChanged(float v)
    {
        // if not in rotation, next two lines of work would be wasted
        float dy = v - mPreviousSliderValues.y;
        mPreviousSliderValues.y = v;
        Vector2 b = GetSelectedXformParameter();
        b.y = v;
        SetSelectedXform(ref b, ref dy);
    }

    void ZValueChanged(float v)
    {
        // if not in rotation, next two lines of work would be wasterd
        float dz = v - mPreviousSliderValues.z;
        mPreviousSliderValues.z = v;
        Vector2 b = Vector2.zero;
        SetSelectedXform(ref b, ref dz);
    }

    public void ObjectSetUI()
    {
        Vector3 p = GetSelectedXformParameter();
        X.SetSliderValue(p.x);  // do not need to call back for this comes from the object
        Y.SetSliderValue(p.y);
        Z.SetSliderValue(p.z);
    }

    private Vector3 GetSelectedXformParameter()
    {
        Vector3 p = Vector3.zero;
        if (T.isOn)
        {
            p.x = tx;
            p.y = ty;

        }
        else if (S.isOn)
        {
            p.x = sx;
            p.y = sy;
        }
        else
        {
            p = Vector3.zero;
        }
        return p;
    }

    private void SetSelectedXform(ref Vector2 p, ref float q)
    {
        if (T.isOn)
        {
            float temp1, temp2;
            temp1 = tx;
            temp2 = ty;
            tx = p.x;
            ty = p.y;
            p.x = p.x - temp1;
            p.y = p.y - temp2;
            
            //mSelected.localPosition = p;
            Mesh theMesh = MyMesh.GetComponent<MeshFilter>().mesh;
            Vector2[] uv = theMesh.uv;
            Matrix3x3 m = Matrix3x3Helpers.CreateTRS(p, 0, Vector2.one);
            for (int i = 0; i < uv.Length; i++)
            {
                uv[i] = Matrix3x3.MultiplyVector3(m, uv[i]);
            }
            theMesh.uv = uv;
        }
        else if (S.isOn && p.x > 0 && p.y > 0)
        {
            sx = p.x;
            sy = p.y;
            Mesh theMesh = MyMesh.GetComponent<MeshFilter>().mesh;
            Vector2[] uv = theMesh.uv;
            float x2 = 0 + tx;
            float y2 = 0 + ty;
            float resolution = Mathf.Sqrt(uv.Length);
            for (int i = 0; i < uv.Length; i++)
            {
                if (x2 > p.x + tx)
                {
                    y2 += (p.y / ((float)resolution - 1));
                    x2 = 0 + tx;
                }
                uv[i] = new Vector2(x2, y2);
                x2 += (p.x / ((float)resolution - 1));
            }
            theMesh.uv = uv;
            /*//mSelected.localScale = p;
            Mesh theMesh = MyMesh.GetComponent<MeshFilter>().mesh;
            Vector2[] uv = theMesh.uv;
            Matrix3x3 m = Matrix3x3Helpers.CreateTRS(Vector2.zero, 0, p);
            for (int i = 0; i < uv.Length; i++)
            {
                uv[i] = Matrix3x3.MultiplyVector3(m, uv[i]);
            }
            theMesh.uv = uv;*/
        }
        else
        {
            //mSelected.localRotation *= q;
            Mesh theMesh = MyMesh.GetComponent<MeshFilter>().mesh;
            Vector2[] uv = theMesh.uv;
            Matrix3x3 m = Matrix3x3Helpers.CreateTRS(Vector2.zero, q  , Vector2.one);
            for (int i = 0; i < uv.Length; i++)
            {
                uv[i] = Matrix3x3.MultiplyVector3(m, uv[i]);
            }
            theMesh.uv = uv;
        }
    }
}