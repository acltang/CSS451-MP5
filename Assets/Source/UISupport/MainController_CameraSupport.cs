using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle
using UnityEngine.EventSystems;

public partial class MainController : MonoBehaviour {

    private Vector3 Origin;
    private Vector3 Difference;
    private bool Drag1 = false;
    private bool Drag2 = false;

    public bool OrbitHorizontal = false;
    const float RotateDelta = 20f / 60;  // about 10-degress per second
    float Direction = 2f;

    // Mouse click selection 
    // Mouse click checking
    void CheckMouseCamera() {
        Tumble();
        Track();
        Dolly();
    }

     void Tumble() {
        if (Input.GetMouseButton(0)) {
            Vector3 cameraToTarget = Target.localPosition - MainCamera.transform.localPosition;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = cameraToTarget.magnitude;
            Difference = (MainCamera.ScreenToWorldPoint(mousePos)) - MainCamera.transform.localPosition;
            if (!Drag1)
            {
                Drag1 = true;
                Origin = MainCamera.ScreenToWorldPoint(mousePos);
            }
        } 
        else {
            Drag1 = false;
        }
        if (Drag1) {
            Vector3 dist = Origin - Difference;
            Vector3 cameraToTarget = Target.localPosition - MainCamera.transform.localPosition;
            // orbit with respect to the transform.right axis

            // 1. Rotation of the viewing direction by right axis
            Quaternion q = Quaternion.AngleAxis(Direction * RotateDelta, Vector3.Cross(dist, cameraToTarget));

            // 2. we need to rotate the camera position
            Matrix4x4 r = Matrix4x4.TRS(Vector3.zero, q, Vector3.one);
            Matrix4x4 invP = Matrix4x4.TRS(-Target.localPosition, Quaternion.identity, Vector3.one);
            r = invP.inverse * r * invP;
            Vector3 newCameraPos = r.MultiplyPoint(MainCamera.transform.localPosition);
            MainCamera.transform.localPosition = newCameraPos;
            //MainCamera.transform.LookAt(Target);

            if (Mathf.Abs(Vector3.Dot(newCameraPos.normalized, Vector3.up)) > 0.9f) // this is about 45-degrees
            {
                Direction *= -1f;
                //Drag1 = false;
            }
        }
    }

    void Track() {
        if (Input.GetMouseButton(1)) {
            Vector3 cameraToTarget = Target.localPosition - MainCamera.transform.localPosition;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = cameraToTarget.magnitude;
            Difference = (MainCamera.ScreenToWorldPoint(mousePos)) - MainCamera.transform.localPosition;
            if (!Drag2)
            {
                Drag2 = true;
                Origin = MainCamera.ScreenToWorldPoint(mousePos);
            }
        } 
        else {
            Drag2 = false;
        }
        if (Drag2) {
            Vector3 dist = Origin - Difference;
            Vector3 cameraToTarget = Target.localPosition - MainCamera.transform.localPosition;
            MainCamera.transform.localPosition = dist;
            Target.localPosition = dist + cameraToTarget;
        }
    }

    void Dolly() {
        MainCamera.transform.Translate(0,0,Input.GetAxis("Mouse ScrollWheel") * 10);
    }
}
