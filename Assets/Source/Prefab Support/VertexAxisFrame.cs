using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexAxisFrame : MonoBehaviour {

	public MyMesh Mesh = null;
	public GameObject Vertex = null;

	public GameObject X = null;
	public GameObject Y = null;
	public GameObject Z = null;

	private bool set = false;
	private float offset;

	private GameObject previous = null;

	// Use this for initialization
	void Start () {
		Mesh = (MyMesh)FindObjectOfType(typeof(MyMesh));
		Vertex = Mesh.mSelected;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AxisSelected(GameObject obj, Ray ray) {
		if (obj.name.Equals("Sphere") || obj.name.Equals("Cylinder")) {
			obj = previous;
		}

		if (obj.name.Equals("X-Axis")) {
			previous = obj;
			X.GetComponent<Renderer>().material.color = Color.yellow;
			Y.GetComponent<Renderer>().material.color = Color.green; 
			Z.GetComponent<Renderer>().material.color = Color.blue;
			MoveVertexX(ray);		
		}
		else if (obj.name.Equals("Y-Axis")) {
			previous = obj;
			X.GetComponent<Renderer>().material.color = Color.red; 
			Y.GetComponent<Renderer>().material.color = Color.yellow;
			Z.GetComponent<Renderer>().material.color = Color.blue;
			MoveVertexY(ray); 
		}
		else if (obj.name.Equals("Z-Axis")) {
			previous = obj;
			X.GetComponent<Renderer>().material.color = Color.red;
			Y.GetComponent<Renderer>().material.color = Color.green; 
			Z.GetComponent<Renderer>().material.color = Color.yellow; 
			MoveVertexZ(ray);
		}
	}

	private void MoveVertexX(Ray ray) {
		float planeY = 0f;
		UnityEngine.Plane plane = new UnityEngine.Plane(Vector3.up, Vector3.up * planeY);
		float distance;
		if(plane.Raycast(ray, out distance)) {
			if (set) {
				offset = ray.GetPoint(distance).x - transform.localPosition.x;
				set = false;
			}
			Debug.Log(offset);
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
		float planeY = 0f;
		UnityEngine.Plane plane = new UnityEngine.Plane(Vector3.up, Vector3.up * planeY);
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

	public void AxisDeselected() {
		X.GetComponent<Renderer>().material.color = Color.red;
		Y.GetComponent<Renderer>().material.color = Color.green; 
		Z.GetComponent<Renderer>().material.color = Color.blue; 
	}

	public void SetOffset() {
		set = true;
	}
}
