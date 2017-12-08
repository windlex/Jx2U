using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class KSMesh : MonoBehaviour {

	public Mesh	mesh;
	public string	meshpath;
	public string	anipath;
	// Use this for initialization
	void Start () {
		SkinnedMeshRenderer rend = gameObject.GetComponent<SkinnedMeshRenderer>();
		mesh = new Mesh();

		Utils.LoadMesh(meshpath, mesh, rend, transform);
		Animation anim = GetComponent<Animation>();
		//Utils.LoadAnim(anipath, anim);
		//anim.Play("test");
		//anim.Play("站立01");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
