using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSModel : MonoBehaviour {
	
	SkinnedMeshRenderer rend;

	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent<SkinnedMeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos()
	{
	}

	public bool LoadMesh(string meshName)
	{
		Transform[] bones;
		Mesh mesh;
		GameObject root;
		if (!GLB.meshMgr.GetMesh(meshName, out Mesh mesh, out root, out bones))
			return false;
		rend.bones = bones;
		rend.mesh = mesh;
		root.transform.parent = transform.param;
		return true;
	}
	public bool LoadAni(string aniFileName)
	{
		Animation anim = GetComponent<Animation>();
		if (!GLB.aniMgr.GetAni(aniName, ref anim))
			return false;
		
		return true;
	}

	public bool PlayAni(string aniName, bool loop = true)
	{
		return false;
	}
	public bool LoadMtl(string mtlName)
	{
		return false;
	}
}
