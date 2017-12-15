using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class KSMesh : MonoBehaviour {

	public Mesh	mesh;
	public string	meshpath;
	public string	anipath;
	public Avatar avatar;
	// Use this for initialization
	void Start () {
		SkinnedMeshRenderer rend = gameObject.GetComponent<SkinnedMeshRenderer>();
		mesh = new Mesh();

		Utils.LoadMesh(meshpath, mesh, rend, transform);
		Animation anim = GetComponent<Animation>();
		Utils.LoadAnim(anipath, anim);
		//anim.Play("test");
		//anim.Play("站立01");

		avatar = AvatarBuilder.BuildGenericAvatar(gameObject, "");
		avatar.name = "Bob";
		Debug.Log(avatar.isHuman ? "is human" : "is generic");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos()
	{
		List<Transform> childs = new List<Transform>();
		childs.Add(transform);

		while (childs.Count > 0)
		{
			Transform trans = childs[0];
			childs.Remove(trans);
			for (int i = 0; i < trans.childCount; i++)
			{
				childs.Add(trans.GetChild(i));
			}

			if (trans.parent)
			{
				Gizmos.DrawLine(trans.parent.position, trans.position);

			}
		}
	}

}
