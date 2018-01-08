using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateRepresentObject()
	{
		GameObject go = new GameObject(uuid);

	}

	public struct ModelRes
	{
		public Mesh mesh;
		public Bones[] bones;
		public Transform root;
	}
	public Dictionary<string, ModelRes> ResMap = new Dictionary<string, ModelRes>();
	public bool GetMesh(meshName, out Mesh mesh, out root, out bones)
	{
		ModelRes res;
		if (!ResMap.TryGetValue(meshName, out res))
		{
			res = new ModelRes();
			bool ret = Utils.LoadMesh(meshName, out res.mesh, out res.bones, out root);
			if (!ret)
				return false;
			ResMap.Add(meshName, res);
		}
		mesh = res.mesh;
		bones = new Bones(res.bones);
		root = new Transform(res.root);
		return true;
	}

}
