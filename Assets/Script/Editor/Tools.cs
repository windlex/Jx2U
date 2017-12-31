using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Toools {

	[MenuItem("JX2U/LoadMesh")]
	public static void test()
	{
		Mesh mesh = new Mesh();
		//Utils.LoadMesh(@"d:\project\JX2U\Assets\Resources\001\001.Mesh", mesh);
	}

	[MenuItem("Jx2U/InitCore")]
	public static void InitCore()
	{
		//CoreHandler.Init();
	}
}
