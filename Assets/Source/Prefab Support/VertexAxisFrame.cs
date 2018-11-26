using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexAxisFrame : MonoBehaviour {

	public MyMesh MyMesh = null;
	public CylinderMesh cMesh = null;
	public GameObject Vertex = null;

	public GameObject X = null;
	public GameObject Y = null;
	public GameObject Z = null;

	private bool XSelected = false;
	private bool YSelected = false;
	private bool ZSelected = false;

	private bool set = false;
	private float offset;

	private GameObject previous = null;

	// Use this for initialization
	void Start () {
		MyMesh = (MyMesh)FindObjectOfType(typeof(MyMesh));
		cMesh = (CylinderMesh)FindObjectOfType(typeof(CylinderMesh));
		Vertex = MyMesh.mSelected;
	}
	
	public void AxisSelected(GameObject obj, Ray ray) {
		if (obj.name.Equals("Sphere") || obj.name.Equals("Cylinder")) {
			obj = previous;
		}
		if (obj.name.Equals("X-Axis") && !YSelected && !ZSelected) {
			XSelected = true;
			previous = obj;
			X.GetComponent<Renderer>().material.color = Color.yellow;
			//MoveVertexX(ray);
			MoveCylinderVertexX(ray);			
		}
		else if (obj.name.Equals("Y-Axis") && !XSelected && !ZSelected) {
			YSelected = true;
			previous = obj;
			Y.GetComponent<Renderer>().material.color = Color.yellow;
			//MoveVertexY(ray); 
			MoveCylinderVertexY(ray);
		}
		else if (obj.name.Equals("Z-Axis") && !XSelected && !YSelected) {
			ZSelected = true;
			previous = obj;
			Z.GetComponent<Renderer>().material.color = Color.yellow; 
			//MoveVertexZ(ray);
			MoveCylinderVertexZ(ray);
		}
	}

	private void MoveVertexX(Ray ray) {
		UnityEngine.Plane plane = new UnityEngine.Plane(Vector3.up, Vector3.up * 0f);
		float distance;
		if(plane.Raycast(ray, out distance)) {
			if (set) {
				offset = ray.GetPoint(distance).x - transform.localPosition.x;
				set = false;
			}
    		transform.position = new Vector3(ray.GetPoint(distance).x - offset, transform.localPosition.y, transform.localPosition.z);
			Vertex.transform.position = new Vector3(ray.GetPoint(distance).x - offset, transform.localPosition.y, transform.localPosition.z); 
		}
	}

	private void MoveVertexY(Ray ray) {
		UnityEngine.Plane plane = new UnityEngine.Plane(Vector3.right, Vector3.right);
		float distance;
		if(plane.Raycast(ray, out distance)) {
			if (set) {
				offset = ray.GetPoint(distance).y - transform.localPosition.y;
				set = false;
			}
    		transform.position = new Vector3(transform.localPosition.x, ray.GetPoint(distance).y - offset, transform.localPosition.z);
			Vertex.transform.position = new Vector3(transform.localPosition.x, ray.GetPoint(distance).y - offset, transform.localPosition.z); 
		}
	}

	private void MoveVertexZ(Ray ray) {
		UnityEngine.Plane plane = new UnityEngine.Plane(Vector3.up, Vector3.up * 0f);
		float distance;
		if(plane.Raycast(ray, out distance)) {
			if (set) {
				offset = ray.GetPoint(distance).z - transform.localPosition.z;
				set = false;
			}
    		transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y, ray.GetPoint(distance).z - offset);
			Vertex.transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y, ray.GetPoint(distance).z - offset); 
		}
	}

	private void MoveCylinderVertexX(Ray ray) {
		UnityEngine.Plane plane = new UnityEngine.Plane(Vector3.up, Vector3.up * 0f);
		float distance;
		if(plane.Raycast(ray, out distance)) {
			if (set) {
				offset = ray.GetPoint(distance).x - transform.localPosition.x;
				set = false;
			}
			for (int i = cMesh.mSelectedIndex; i < cMesh.mSelectedIndex + cMesh.cResolution; i++) {
				Vector3 dir = cMesh.mControllers[i].transform.position;
				float dist = ray.GetPoint(distance).x - offset;
				Vector3 move = dir.normalized * dist;

    			transform.position = new Vector3(dist, transform.localPosition.y, transform.localPosition.z);
				cMesh.mControllers[i].transform.position = new Vector3(move.x, cMesh.mControllers[i].transform.localPosition.y, move.z); 
			}
		}
	}

	private void MoveCylinderVertexY(Ray ray) {
		UnityEngine.Plane plane = new UnityEngine.Plane(Vector3.right, Vector3.right);
		float distance;
		if(plane.Raycast(ray, out distance)) {
			if (set) {
				offset = ray.GetPoint(distance).y - transform.localPosition.y;
				set = false;
			}
			for (int i = cMesh.mSelectedIndex; i < cMesh.mSelectedIndex + cMesh.cResolution; i++) {
				Vector3 dir = cMesh.mControllers[i].transform.position - Vector3.down;
				float dist = ray.GetPoint(distance).y - offset;
				Vector3 move = dir.normalized * dist;

    			transform.position = new Vector3(transform.localPosition.x, dist, transform.localPosition.z);
				cMesh.mControllers[i].transform.position = new Vector3(cMesh.mControllers[i].transform.localPosition.x, move.y, cMesh.mControllers[i].transform.localPosition.z); 
			}
		}
	}

	private void MoveCylinderVertexZ(Ray ray) {
		UnityEngine.Plane plane = new UnityEngine.Plane(Vector3.up, Vector3.up * 0f);
		float distance;
		if(plane.Raycast(ray, out distance)) {
			if (set) {
				offset = ray.GetPoint(distance).z - transform.localPosition.z;
				set = false;
			}
			for (int i = cMesh.mSelectedIndex; i < cMesh.mSelectedIndex + cMesh.cResolution; i++) {
				Vector3 dir = cMesh.mControllers[i].transform.position;
				float dist = ray.GetPoint(distance).z - offset;
				Vector3 move = dir.normalized * dist;

    			transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y, dist);
				cMesh.mControllers[i].transform.position = new Vector3(move.x, cMesh.mControllers[i].transform.localPosition.y, move.z); 
			}
		}
	}

	public void AxisDeselected() {
		XSelected = false;
		YSelected = false;
		ZSelected = false;
		X.GetComponent<Renderer>().material.color = Color.red;
		Y.GetComponent<Renderer>().material.color = Color.green; 
		Z.GetComponent<Renderer>().material.color = Color.blue;
	}

	public void SetOffset() {
		set = true;
	}
}
