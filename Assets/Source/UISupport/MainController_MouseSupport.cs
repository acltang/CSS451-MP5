using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle
using UnityEngine.EventSystems;

public partial class MainController : MonoBehaviour {

    private bool held = false;

    void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, 1);
            if (hit && !EventSystem.current.IsPointerOverGameObject())
            {
                MyMesh.SelectVertex(hitInfo.transform.gameObject);
            }
        }
    }

    void CheckMouseHold() {
        if (axis) {
            if (Input.GetMouseButton(0)) {
               RaycastHit hitInfo = new RaycastHit();
               Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
               bool hit = Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1);
               if (hit && !EventSystem.current.IsPointerOverGameObject())
               {
                   if (!held) {
                       axis.SetOffset();
                       held = true;
                   }
                   axis.AxisSelected(hitInfo.transform.gameObject, ray);
               }
            }
            else {
                axis.AxisDeselected();
                held = false;
            }
        }
    }
}