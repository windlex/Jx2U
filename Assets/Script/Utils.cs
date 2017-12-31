using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

using byte2 = System.Int16;
using byte4 = System.Int32;

public static class MatrixExtensions
{
	public static Matrix4x4 ToMatrix4x4(this Transform transform)
	{
		Matrix4x4 mat = new Matrix4x4();
		mat.SetTRS(transform.localPosition, transform.localRotation, transform.localScale);
		return mat;
	}
	public static Quaternion ExtractRotation(this Matrix4x4 matrix)
	{
		Vector3 forward;
		forward.x = matrix.m02;
		forward.y = matrix.m12;
		forward.z = matrix.m22;

		Vector3 upwards;
		upwards.x = matrix.m01;
		upwards.y = matrix.m11;
		upwards.z = matrix.m21;

		return Quaternion.LookRotation(forward, upwards);
	}

	public static Vector3 ExtractPosition(this Matrix4x4 matrix)
	{
		Vector3 position;
		position.x = matrix.m03;
		position.y = matrix.m13;
		position.z = matrix.m23;
		return position;
	}

	public static Vector3 ExtractScale(this Matrix4x4 matrix)
	{
		Vector3 scale;
		scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
		scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
		scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
		return scale;
	}
}

public class Utils {
	[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
	struct mesh_head_t
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public char[] common_tag;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public char[] module_tag;
		public int version;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		public char[] description;
		public int filemask;
		public int length;
		public int materail_offset;
		public int animation_offset;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public int[] reserved;
		public int mesh_count;
		public int vertex_count;
		public int face_count;
		public int subset_count;
		public int pos_offset;
		public int normal_offset;
		public int color1_offset;
		public int tex1_offset;
		public int tex2_offset;
		public int tex3_offset;
		public int index_buffer_offset;
		public int attri_buffer_offset;
		public int skin_info_offset;
		public int lod_info_offset;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
		public int[] reserved_ex;
	}

	public static byte[] StructToBytes(object structure)
	{
		int size = Marshal.SizeOf(structure);
		IntPtr buffer = Marshal.AllocHGlobal(size);
		try
		{
			Marshal.StructureToPtr(structure, buffer, false);
			byte[] bytes = new byte[size];
			Marshal.Copy(buffer, bytes, 0, size);
			return bytes;
		}
		finally
		{
			Marshal.FreeHGlobal(buffer);
		}
	}
	//将Byte转换为结构体类型
	public static object BytesToStruct(Byte[] bytes, Type strcutType)
	{
		int size = Marshal.SizeOf(strcutType);
		IntPtr buffer = Marshal.AllocHGlobal(size);
		try
		{
			Marshal.Copy(bytes, 0, buffer, size);
			return Marshal.PtrToStructure(buffer, strcutType);
		}
		finally
		{
			Marshal.FreeHGlobal(buffer);
		}
	}


	//struct mesh_skin_info_t
	//{
	//	List<_mesh_bone_t> bone_vec;
	//	List<int> real_bone_index_vec;
	//	List<_mesh_socket_t> socket_vec;
	//	LPD3DXSKININFO d3dsi;			//temp
	//};

	[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
	public struct float2
	{
		public float x;
		public float y;
	}

	[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
	public struct float3
	{
		public float x;
		public float y;
		public float z;
	}

	[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
	struct xyz_normal_uv_t
	{
		public float3 xyz;
		public float3 normal;
		public float2 uv;
	};
	static int D3DXMESH_32BIT = 1;

    public struct matrix
    {
		public float _11, _12, _13, _14;
		public float _21, _22, _23, _24;
		public float _31, _32, _33, _34;
		public float _41, _42, _43, _44;
    }
	static Dictionary<string, string> BoneTree = new Dictionary<string, string>();

	public static unsafe Mesh LoadMesh(string filename, Mesh mesh, SkinnedMeshRenderer rend, Transform transform)
	{
		byte[] filedata;
		bool ret = OpenFile(filename, out filedata);

		if (!ret)
			return null;

		mesh_head_t head = (mesh_head_t)BytesToStruct(filedata, typeof(mesh_head_t));
		fixed ( byte *data = &filedata[0])
		{
			//mesh_head_t *head2 = (mesh_head_t *)data;
		}
		if (head.filemask != 0x4D455348)
		{
			return null;
		}
		if (head.pos_offset == 0 || head.tex1_offset == 0 || head.index_buffer_offset == 0)
		{
			return null;
		}

		#region meshinfo
		////create the original mesh 
		int mesh_option = 0;
		//if (!skin_info.d3dsi && (gpu().get_creation_parameters().vertex_processing_method == _gpu_vertex_processing_method_hardware_ || gpu().get_creation_parameters().vertex_processing_method == _gpu_vertex_processing_method_purehardware_))
		//	mesh_option |= D3DXMESH_MANAGED;		//non-skinned && hardware-vp
		//else
		//	mesh_option |= D3DXMESH_SYSTEMMEM;	//skinned or soft-vp

		if (head.vertex_count > 0x0000ffff)
			mesh_option |= D3DXMESH_32BIT;

		//LPD3DXMESH d3dmesh = 0;
		//if (FAILED(D3DXCreateMeshFVF(head.face_count, head.vertex_count, mesh_option, D3DFVF_XYZ | D3DFVF_NORMAL | D3DFVF_TEX1, gpu().d3ddevice(), &d3dmesh)))
		//{
		//	clear();
		//	return;
		//}

		//head.face_count;
		//head.vertex_count;

		//load data
		//vertex buffer
		Debug.Log(head);
		Debug.LogWarning(string.Format("vertex_count = {0}", head.vertex_count));
		List<Vector3> ves = new List<Vector3>();
		fixed (byte* v = &filedata[head.pos_offset])
		{
			float3* ff = (float3*)v;
			for (int i = 0; i < head.vertex_count; i++,ff++)
			{
				//Debug.Log(string.Format("{0},{1},{2}", ff->x, ff->y, ff->z));
				//mesh.vertices[i] = new Vector3(ff->x, ff->y, ff->z);
				ves.Add(new Vector3(ff->x, ff->y, ff->z));
			}			
		}
		mesh.SetVertices(ves);

		Debug.LogWarning(string.Format("vertex_count = {0}", head.vertex_count));
		mesh.uv = new Vector2[head.vertex_count];
		List<Vector2> uvs = new List<Vector2>();
		fixed (byte* v = &filedata[head.tex1_offset])
		{
			float3* ff = (float3*)v;
			for (int i = 0; i < head.vertex_count; i++, ff++)
			{
				//Debug.Log(string.Format("{0},{1},{2}", ff->x, ff->y, ff->z));
				mesh.uv[i] = new Vector2(ff->x, ff->y);
				uvs.Add(new Vector2(ff->x, 1 - ff->y));
			}
		}
		mesh.SetUVs(0, uvs);

		if (head.normal_offset != 0)
		{
			Debug.LogWarning(string.Format("normal = {0}", head.vertex_count));
			List<Vector3> normal = new List<Vector3>();
			fixed (byte* v = &filedata[head.normal_offset])
			{
				float3* ff = (float3*)v;
				for (int i = 0; i < head.vertex_count; i++, ff++)
				{
					//Debug.Log(string.Format("{0},{1},{2}", ff->x, ff->y, ff->z));
					normal.Add(new Vector2(ff->x, ff->y));
				}
			}
			mesh.SetNormals(normal);
		}

		//float3* uv = (float3*)((byte*)filedata[head.tex1_offset]);
		//float3* normal = (float3*)(filedata[head.normal_offset]);
		//int bNormal = head.normal_offset == 0 ? 1 : 0;
		//xyz_normal_uv_t* vb_v;
		//d3dmesh.LockVertexBuffer(0, (void**)&vb_v);
		//for (int i = 0; i < head.vertex_count; i++)
		//{
		//	vb_v[i].xyz = v[i];
		//	vb_v[i].uv.x = uv[i].x;
		//	vb_v[i].uv.y = uv[i].y;
		//	if (bNormal == 1)
		//		vb_v[i].normal = normal[i];
		//}
		//d3dmesh.UnlockVertexBuffer();
		//if (!normal)
		//{
		//	D3DXComputeNormals(d3dmesh, 0);
		//}

		// Build basic mesh
		//Mesh mesh = new Mesh();
		//index buffer
		
		Debug.LogWarning(string.Format("Face Count = {0}", head.face_count));

		int triCount = head.face_count * 3;
		mesh.triangles = new int[triCount];
		int[] trs = new int[triCount];
		int[] trs2 = new int[triCount];
		fixed (byte *v = &filedata[head.index_buffer_offset])
		{
			short* b2 = (short*)v;
			int* b4 = (int*)v;
			for (int i = 0; i < triCount; i++, b2++, b4++)
			{
				int t = 0;
				if ((mesh_option & D3DXMESH_32BIT) == 0)
					t = *b4;
				else
					t = *b2;
				//Debug.Log(string.Format("{0} {1}",i, t));
				mesh.triangles[i] = t;
				trs[i] = t;
			}
		}
		for (int i = 0; i < triCount; i++)
		{
			trs2[i] = trs[triCount - i - 1];
		}
		mesh.SetTriangles(trs2, 0, true);

		//void* ib_f;
		//d3dmesh.LockIndexBuffer(0, (void**)&ib_f);
		//if ((mesh_option & D3DXMESH_32BIT) == 0)
		//	memcpy(ib_f, f, head.face_count * 3 * 4);
		//else
		//{
		//	for (int i = 0; i < head.face_count * 3; i++)
		//	{
		//		((byte2*)ib_f)[i] = (byte2)f[i];
		//	}
		//}
		//d3dmesh.UnlockIndexBuffer();
		mesh.RecalculateNormals();	//todo: head.normal_offset
		mesh.RecalculateBounds();
		mesh.RecalculateTangents();

		////attribute buffer
		//int* ab_ss = 0;
		//d3dmesh.LockAttributeBuffer(0, (int**)&ab_ss);
		//if (!head.attri_buffer_offset)
		//{
		//	memset(ab_ss, 0, head.face_count * 4);
		//}
		//else
		//{
		//	int* ss = (int*)((byte*)filedata.GetBufferPointer() + head.attri_buffer_offset);
		//	memcpy(ab_ss, ss, head.face_count * 4);
		//}
		//d3dmesh.UnlockAttributeBuffer();
		////end of creating original mesh

		////generate geo info
		//geo_info = new mesh_geo_info_t;

		////clear ptrs
		//geo_info.vb = 0;
		//geo_info.ib = 0;
		//geo_info.decl = 0;
		//geo_info.attri_table_buf = 0;

		//if (!skin_info.d3dsi)	//non-skinned
		//{
		//	//optimize & generate the attribute table
		//	int* adj = new int[head.face_count * 3];
		//	d3dmesh.GenerateAdjacency(0, adj);
		//	d3dmesh.OptimizeInplace(D3DXMESHOPT_ATTRSORT, adj, 0, 0, 0);
		//	SAFE_DELETE_ARRAY(adj);

		//	//retrieve datas
		//	d3dmesh.GetVertexBuffer(&geo_info.vb);
		//	geo_info.stride = d3dmesh.GetNumBytesPerVertex();
		//	d3dmesh.GetIndexBuffer(&geo_info.ib);
		//	geo_info.fvf = d3dmesh.GetFVF();
		//	d3dmesh.GetAttributeTable(0, &geo_info.subset_count);
		//	D3DXCreateBuffer(geo_info.subset_count * sizeof(D3DXATTRIBUTERANGE), &geo_info.attri_table_buf);
		//	d3dmesh.GetAttributeTable((D3DXATTRIBUTERANGE*)geo_info.attri_table_buf.GetBufferPointer(), &geo_info.subset_count);
		//	geo_info.palette_count = 0;
		//	geo_info.max_infl_count = 0;
		//}
		//else	//skinned
		//{
		//	//generate adjacency
		//	int* adj = new int[head.face_count * 3];
		//	d3dmesh.GenerateAdjacency(0, adj);

		//	//generate blended mesh
		//	LPD3DXMESH blended_mesh = 0;
		//	//pal count
		//	switch (gpu().get_creation_parameters().vertex_processing_method)
		//	{
		//	case _gpu_vertex_processing_method_software_:
		//	case _gpu_vertex_processing_method_mixed_:
		//		geo_info.palette_count = min((int)skin_info.real_bone_index_vec.size(), (256 - 17) / 3);
		//		break;
		//	default:
		//		geo_info.palette_count = min((int)skin_info.real_bone_index_vec.size(), (gpu().get_hardware_caps().max_vertex_shader_const - 17) / 3);
		//	}


		//	int flags = D3DXMESHOPT_VERTEXCACHE;
		//	switch (gpu().get_creation_parameters().vertex_processing_method)
		//	{
		//	case _gpu_vertex_processing_method_software_:
		//	case _gpu_vertex_processing_method_mixed_:
		//		flags |= D3DXMESH_SYSTEMMEM;	//if the hardware indexed skinning is avaiable, we won't use mixed or software vertex processing method
		//		break;
		//	default:
		//		flags |= D3DXMESH_MANAGED;
		//	}

		//	//convert
		//	if (FAILED(skin_info.d3dsi.ConvertToIndexedBlendedMesh(d3dmesh,
		//		flags,
		//		geo_info.palette_count,
		//		adj,
		//		0, 0, 0,
		//		(int*)&geo_info.max_infl_count,
		//		(int*)&geo_info.subset_count,
		//		&geo_info.attri_table_buf,
		//		&blended_mesh)) ||
		//		geo_info.max_infl_count > 4)
		//	{
		//		SAFE_DELETE_ARRAY(adj);
		//		SAFE_DELETE(geo_info);
		//		SAFE_RELEASE(d3dmesh);
		//		clear();
		//		return;
		//	}

		//	//retrieve datas
		//	blended_mesh.GetVertexBuffer(&geo_info.vb);
		//	geo_info.stride = blended_mesh.GetNumBytesPerVertex();
		//	blended_mesh.GetIndexBuffer(&geo_info.ib);

		//	//fvf
		//	geo_info.fvf = blended_mesh.GetFVF();

		//	//decl will be generated later

		//	HRESULT r = D3DXSaveMeshToX(
		//		"OUT.X",
		//		blended_mesh,
		//		adj,
		//		NULL, //_In_ const D3DXMATERIAL       *pMaterials,
		//		NULL, //_In_ const D3DXEFFECTINSTANCE *pEffectInstances,
		//		NULL, //_In_       DWORD              NumMaterials,
		//		0	//D3DXF_FILEFORMAT_TEXT
		//		);
		//	SAFE_DELETE_ARRAY(adj);
		//	SAFE_RELEASE(blended_mesh);
		//}
		#endregion


		#region bone
		//skin_info = new mesh_skin_info_t;
		//skin_info.d3dsi = 0;


		// bindPoses was created earlier and was updated with the required matrix.
		// The bindPoses array will now be assigned to the bindposes in the Mesh.


		byte[] buff = new byte[30];
		Transform[] bones;
		Matrix4x4[] bindPoses;
		BoneWeight[] boneweights = new BoneWeight[head.vertex_count];
		Dictionary<string, GameObject> BoneList = new Dictionary<string,GameObject>();
		Dictionary<string, Matrix4x4> MatrixList = new Dictionary<string,Matrix4x4>();
		Dictionary<string, int> PoseList = new Dictionary<string,int>();

		fixed (byte* ptrb = &filedata[head.skin_info_offset])
		{
			char* ptr = (char*)ptrb;
			int bone_size = *(int*)ptr;
			Debug.Log("Bone Size : " + bone_size);
			bones = new Transform[bone_size];
			bindPoses = new Matrix4x4[bone_size];
			ptr += 4 / 2;

			for (int i = 0; i < bone_size; i++)
			{
				//bone.name = (char *)ptr;
				//Marshal.Copy(new IntPtr(ptr), buff, 0, 30);
				//string name = Encoding.Default.GetString(buff);
				string name = Marshal.PtrToStringAnsi(new IntPtr(ptr));
				Debug.Log(name);
				ptr += 30 / 2;

				//bone.parent_name = (char *)ptr;
				//Marshal.Copy(new IntPtr(ptr), buff, 0, 30);
				string parent_anme = Marshal.PtrToStringAnsi(new IntPtr(ptr));
				//Debug.Log(parent_anme);
				ptr += 30 / 2;

				int num_child = *(int*)ptr;
				ptr += (4 + num_child * 30) / 2;

				//bone_vec[i].offset_matrix = *(matrix*)ptr;
				//matrix offset_matrix = new matrix();
				//Marshal.Copy(new IntPtr(ptr), offset_matrix, 0, sizeof(matrix));
				matrix offset_matrix = (matrix)Marshal.PtrToStructure(new IntPtr(ptr), typeof(matrix));
				Matrix4x4 om = (Matrix4x4)Marshal.PtrToStructure(new IntPtr(ptr), typeof(Matrix4x4));
				//om = om.inverse;
				Debug.Log(om);
				//om = om.inverse;
				if (!om.ValidTRS())
					Debug.LogError("Not TRS");
				ptr += sizeof(matrix) / 2;

				Matrix4x4 bm = (Matrix4x4)Marshal.PtrToStructure(new IntPtr(ptr), typeof(Matrix4x4));
				//Debug.Log(bm);
				//om = bm;
				ptr += sizeof(matrix) / 2;

				int num_infl = *(int*)ptr;
				ptr += 4 / 2;
				//Debug.Log("num_infl : " + num_infl);

				// weights
				char *ptr1 = ptr;
				char* ptr2 = ptr + num_infl * sizeof(int) / 2;
				for (int w = 0; w < num_infl; w++)
				{
					//real_bone_index_vec.push_back(i);
					//skin_info.d3dsi.SetBoneInfluence((int)skin_info.real_bone_index_vec.size() - 1, num_infl, (int*)ptr, (float*)(ptr + 4 * num_infl));
					int vecIdx = *(int*)ptr1;
					ptr1 += sizeof(int) / 2;

					float weight = *(float*)ptr2;
					ptr2 += sizeof(float) / 2;

					//Debug.Log(string.Format("{0}, {1}, {2}", w, vecIdx, weight));
					if (boneweights[vecIdx].weight0 == 0)
					{
						boneweights[vecIdx].boneIndex0 = i;
						boneweights[vecIdx].weight0 = weight;
					}
					else if (boneweights[vecIdx].weight1 == 0)
					{
						boneweights[vecIdx].boneIndex1 = i;
						boneweights[vecIdx].weight1 = weight;
					}
					else if (boneweights[vecIdx].weight2 == 0)
					{
						boneweights[vecIdx].boneIndex2 = i;
						boneweights[vecIdx].weight2 = weight;
					}
					else if (boneweights[vecIdx].weight3 == 0)
					{
						boneweights[vecIdx].boneIndex3 = i;
						boneweights[vecIdx].weight3 = weight;
					}
					else
					{
						Debug.LogWarning("Bone Weight Count Error!!!");
					}
				}
				ptr += (num_infl * (sizeof(int) + sizeof(float))) / 2;

				bones[i] = new GameObject(name).transform;
				bones[i].parent = transform;
				// Set the position relative to the parent
				bones[i].localRotation = Quaternion.identity;// om.ExtractRotation();//
				bones[i].localPosition = new Vector3(0, 0, 0);//om.ExtractPosition();//

				//bones[i].localScale = om.ExtractScale();
				BoneList.Add(name, bones[i].gameObject);
				BoneTree.Add(name, parent_anme);
				MatrixList.Add(name, om);
				PoseList.Add(name, i);
				// The bind pose is bone's inverse transformation matrix
				// In this case the matrix we also make this matrix relative to the root
				// So that we can move the root game object around freely
				bindPoses[i] = Matrix4x4.identity;
				//bindPoses[i] = bones[i].worldToLocalMatrix * transform.localToWorldMatrix;
				//bindPoses[i] = om * transform.localToWorldMatrix;
				//bindPoses[i] = om.inverse;
			}
		}
		Debug.LogWarning("----------------------------------------------");
		foreach (var item in BoneTree)
		{
			string name = item.Key;
			string parent = item.Value;
			GameObject go;
			GameObject goParent;
			if (BoneList.TryGetValue(name, out go) && BoneList.TryGetValue(parent, out goParent))
			{
				go.transform.SetParent(goParent.transform);
			}
		}

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
			string name = trans.name;
			string parent;
			if (!BoneTree.TryGetValue(name, out parent))
				continue;

			Debug.Log(string.Format("2 {0} -> {1}", name, parent));
			GameObject go;
			GameObject goParent;
			if (BoneList.TryGetValue(name, out go))
			{
				Matrix4x4 om = MatrixList[name];
				Debug.Log(om);
				int pose = PoseList[name];

				go.transform.localPosition = om.ExtractPosition();
				go.transform.localRotation = om.ExtractRotation();
				//go.transform.localScale = om.ExtractScale();


				bindPoses[pose] = go.transform.ToMatrix4x4().inverse;
				if (parent != "Scene Root" && BoneList.TryGetValue(parent, out goParent))
				{
					//Matrix4x4 im = goParent.transform.ToMatrix4x4().inverse;
					//go.transform.localRotation = (om * im).ExtractRotation();//Quaternion.identity;// 
					//go.transform.localPosition = (om * im).ExtractPosition();//new Vector3(0, 0, 0);//
					//go.transform.localScale = om.ExtractScale();
					//bindPoses[pose] = go.transform.worldToLocalMatrix * goParent.transform.localToWorldMatrix;
				}
				else
				{
					Debug.Log(string.Format("{0} no parent", name));
					//go.transform.localPosition = om.ExtractPosition();
					//go.transform.localRotation = om.ExtractRotation();
					//go.transform.localScale = om.ExtractScale();
					//bindPoses[pose] = go.transform.worldToLocalMatrix * transform.localToWorldMatrix;
				}
			}
		}

		mesh.boneWeights = boneweights;
		mesh.bindposes = bindPoses;
		// Assign bones and bind poses
		rend.bones = bones;
		rend.sharedMesh = mesh;

		////non-skinned mesh
		//if (!head.skin_info_offset)
		//	return;

		////skinned mesh
		//byte* ptr = (byte*)filedata.GetBufferPointer() + head.skin_info_offset;

		//skin_info.bone_vec.resize(*(int*)ptr);
		//ptr += 4;

		//if (!skin_info.bone_vec.empty())
		//{
		//	//create d3dx skin info (may be larger than needed)
		//	if (FAILED(D3DXCreateSkinInfoFVF(head.vertex_count, D3DFVF_XYZ | D3DFVF_NORMAL | D3DFVF_TEX1, (int)skin_info.bone_vec.size(), &skin_info.d3dsi)))
		//	{
		//		SAFE_DELETE(skin_info);
		//		clear();
		//		return;
		//	}
		//}

		//for (int i = 0; i < skin_info.bone_vec.size(); i++)
		//{
		//	strcpy(skin_info.bone_vec[i].name, (char*)ptr);
		//	ptr += 30;

		//	skin_info.bone_vec[i].parent_name = (char*)ptr;
		//	ptr += 30;

		//	int num_child = *(int*)ptr;
		//	ptr += 4 + num_child * 30;

		//	skin_info.bone_vec[i].offset_matrix = *(matrix*)ptr;
		//	ptr += 2 * sizeof(matrix);

		//	int num_infl = *(int*)ptr;
		//	ptr += 4;
		//	if (num_infl > 0)
		//	{
		//		skin_info.real_bone_index_vec.push_back(i);
		//		skin_info.d3dsi.SetBoneInfluence((int)skin_info.real_bone_index_vec.size() - 1, num_infl, (int*)ptr, (float*)(ptr + 4 * num_infl));
		//		ptr += num_infl * (sizeof(int) + sizeof(float));
		//	}
		//}

		//if (skin_info.real_bone_index_vec.empty())
		//{
		//	//non-skinned in fact
		//	SAFE_RELEASE(skin_info.d3dsi);
		//}

		////compute derived bone members
		////parent index
		//for (int i = 0; i < skin_info.bone_vec.size(); i++)
		//{
		//	skin_info.bone_vec[i].parent_index = -1;

		//	for (int j = 0; j < skin_info.bone_vec.size(); j++)
		//	{
		//		if (strcmp(skin_info.bone_vec[i].parent_name.c_str(), skin_info.bone_vec[j].name) == 0)
		//		{
		//			skin_info.bone_vec[i].parent_index = j;
		//			skin_info.bone_vec[i].parent_name.clear();
		//			break;
		//		}
		//	}
		//}

		////soft type
		//for (int i = 0; i < skin_info.bone_vec.size(); i++)
		//{
		//	if (strncmp(skin_info.bone_vec[i].name, "FL_", 3) == 0)
		//		skin_info.bone_vec[i].soft_type = _bone_soft_type_soft_;
		//	else if (strncmp(skin_info.bone_vec[i].name, "GL_", 3) == 0)
		//		skin_info.bone_vec[i].soft_type = _bone_soft_type_gravity_;
		//	else if (strncmp(skin_info.bone_vec[i].name, "AL_", 3) == 0)
		//		skin_info.bone_vec[i].soft_type = _bone_soft_type_adjsoft_;
		//	else
		//		skin_info.bone_vec[i].soft_type = _bone_soft_type_normal_;
		//}

		////soft level
		//for (int i = 0; i < skin_info.bone_vec.size(); i++)
		//	skin_info.bone_vec[i].soft_level = -1;
		//for (int i = 0; i < skin_info.bone_vec.size(); i++)
		//	_compute_bone_soft_level_recursive(skin_info, i);

		////inv-offset matrix & relative matrix
		//for (int i = 0; i < skin_info.bone_vec.size(); i++)
		//{
		//	D3DXMatrixInverse(&skin_info.bone_vec[i].inv_offset_matrix, 0, &skin_info.bone_vec[i].offset_matrix);
		//	if (skin_info.bone_vec[i].parent_index != -1)
		//		skin_info.bone_vec[i].relative_matrix = skin_info.bone_vec[i].inv_offset_matrix * skin_info.bone_vec[skin_info.bone_vec[i].parent_index].offset_matrix;
		//	else
		//		skin_info.bone_vec[i].relative_matrix = skin_info.bone_vec[i].inv_offset_matrix;
		//}
		////end of computing derived bone members

		////sockets
		//skin_info.socket_vec.resize(*(int*)ptr);
		//ptr += 4;

		//for (int i = 0; i < skin_info.socket_vec.size(); i++)
		//{
		//	strcpy(skin_info.socket_vec[i].name, (char*)ptr);
		//	ptr += 30;

		//	skin_info.socket_vec[i].parent_bone_name = (char*)ptr;
		//	ptr += 30;

		//	skin_info.socket_vec[i].relative_matrix = *(matrix*)ptr;
		//	ptr += sizeof(matrix);
		//}

		////compute derived socket members
		//for (int i = 0; i < skin_info.socket_vec.size(); i++)
		//{
		//	skin_info.socket_vec[i].parent_bone_index = -1;

		//	for (int j = 0; j < skin_info.bone_vec.size(); j++)
		//	{
		//		if (strcmp(skin_info.socket_vec[i].parent_bone_name.c_str(), skin_info.bone_vec[j].name) == 0)
		//		{
		//			skin_info.socket_vec[i].parent_bone_index = j;
		//			skin_info.socket_vec[i].parent_bone_name.clear();
		//			break;
		//		}
		//	}
		//}

		#endregion

		//SAFE_RELEASE(d3dmesh);
		//SAFE_RELEASE(skin_info.d3dsi);
		//SAFE_RELEASE(filedata);
		return mesh;
	}
	public static bool OpenFile(string filename, out byte[] data)
	{
        try
        {
            FileStream TextFile = File.Open(filename, FileMode.Open);
            data = new byte[TextFile.Length];
            int ret = TextFile.Read(data, 0, (int)TextFile.Length);
            TextFile.Close();
            return true;
        }
        catch (Exception e)
        {
            //Debug.LogError(e);
            data = null;
            return false;
        }
	}

	enum animation_type_t
	{
		animation_type_vertex = 1,
		animation_type_vertex_relative = 2,
		animation_type_bone = 10,
	};

	public static unsafe bool LoadAnim(string filename, Animation anim)
	{
		byte[] filedata;
		OpenFile(filename, out filedata);

		fixed (byte* data = &filedata[0])
		{
			char *ptr = (char *)data;
			if (*(int*)ptr != 0x414E494D)
			{
				return false;
			}

			ptr += 8 / 2;

			//ani_vec.resize(*(int*)ptr);
			int ani_size = *(int*)ptr;
			ptr += 4 / 2;

			int real_count = 0;
			for (int i = 0; i < ani_size; i++)
			{
				int ani_type = *(int*)ptr;
				//ani_vec[real_count].type = ani_type;
				ptr += 4 / 2;

				//strcpy(ani_vec[real_count].name, (char*)ptr);
				AnimationClip clip = new AnimationClip();
				string ani_name = Marshal.PtrToStringAnsi(new IntPtr(ptr));
				byte[] destination = new byte[30];
				Marshal.Copy(new IntPtr(ptr), destination, 0, 30);
				ani_name = Encoding.Default.GetString(destination);
				Debug.Log(ani_name);

				ptr += 30 / 2;

				if (ani_type != (int)animation_type_t.animation_type_bone)
						Debug.LogError("Unsupport Ani Type: " + ani_type);

				switch (ani_type)
				{
					case (int)animation_type_t.animation_type_bone:
						{
							int function_count = *(int*)ptr;
							//ani_vec[real_count].func_vec.resize(*(int*)ptr);
							ptr += 4 / 2;

							int frame_count = *(int*)ptr;
							ptr += 4 / 2;

							float interval = *(float*)ptr;
							ptr += sizeof(float) / 2;

							char *ptr1 = ptr;
							char *ptr2 = ptr + 30 * function_count / 2;
							for (int ifunc = 0; ifunc < function_count; ifunc++)
							{
								AnimationCurve[] curve = new AnimationCurve[10];
								for (int c = 0; c < 10; c++)
									curve[c] = new AnimationCurve();

								string bone_name = Marshal.PtrToStringAnsi(new IntPtr(ptr1));
								//Debug.Log(bone_name);
								string real_name = bone_name;
								string parent = "";
								while (BoneTree.TryGetValue(bone_name, out parent))
								{
									if (parent == "Scene Root")
										break;
									real_name = parent + "/" + real_name;
									bone_name = parent;
								}
								bone_name = real_name;

								//Debug.Log(bone_name);
								//func_map[bone_name] = ifunc;
								ptr1 += 30 / 2;
								for (int f = 0; f < frame_count; f++)
								{
									float time = f * interval / 1000;
									Matrix4x4 mat = (Matrix4x4)Marshal.PtrToStructure(new IntPtr(ptr2), typeof(Matrix4x4));
									mat = mat.inverse;
									Vector3 pos = mat.ExtractPosition();
									Quaternion rot = mat.ExtractRotation();
									Vector3 sc = mat.ExtractScale();
									//Debug.Log(mat);
									//Debug.Log(pos);
									//Debug.Log(rot.eulerAngles);
									//Debug.Log(sc);
									curve[0].AddKey(time, pos.x);
									curve[1].AddKey(time, pos.y);
									curve[2].AddKey(time, pos.z);
									curve[3].AddKey(time, rot.x);
									curve[4].AddKey(time, rot.y);
									curve[5].AddKey(time, rot.z);
									curve[6].AddKey(time, rot.w);
									curve[7].AddKey(time, sc.x);
									curve[8].AddKey(time, sc.y);
									curve[9].AddKey(time, sc.z);

									ptr2 += sizeof(Matrix4x4) / 2;
								}
								clip.SetCurve(bone_name, typeof(Transform), "localPosition.x", curve[0]);
								clip.SetCurve(bone_name, typeof(Transform), "localPosition.y", curve[1]);
								clip.SetCurve(bone_name, typeof(Transform), "localPosition.z", curve[2]);
								//clip.SetCurve(bone_name, typeof(Transform), "localRotation.x", curve[3]);
								//clip.SetCurve(bone_name, typeof(Transform), "localRotation.y", curve[4]);
								//clip.SetCurve(bone_name, typeof(Transform), "localRotation.z", curve[5]);
								//clip.SetCurve(bone_name, typeof(Transform), "localRotation.w", curve[6]);
								//clip.SetCurve(bone_name, typeof(Transform), "localScale.x", curve[7]);
								//clip.SetCurve(bone_name, typeof(Transform), "localScale.y", curve[8]);
								//clip.SetCurve(bone_name, typeof(Transform), "localScale.z", curve[9]);
							}

							// Create the clip with the curve
							clip.legacy = true;

							// Add and play the clip
							clip.wrapMode = WrapMode.Loop;
							ptr += (30 + sizeof(matrix) * frame_count) * function_count / 2;
							//for (int ifunc = 0; ifunc < function_count; ifunc++)
							{
								//ani_vec[real_count].func_vec[ifunc].resize(ani_vec[real_count].frame_count);

								//memcpy(&ani_vec[real_count].func_vec[ifunc][0], ptr, sizeof(matrix) * ani_vec[real_count].frame_count);
							}

							real_count++;
						}
						break;
					/*
					case (int)animation_type_t.animation_type_vertex:
					case (int)animation_type_t.animation_type_vertex_relative:
						{
							ptr += 4;

							ani_vec[real_count].function_count = *(int*)ptr;
							ptr += 4;

							ani_vec[real_count].frame_count = *(int*)ptr;
							ptr += 4;

							ani_vec[real_count].interval = *(float*)ptr;
							ptr += sizeof(float);

							ani_vec[real_count].vertex_index_vec.resize(ani_vec[real_count].function_count);
							memcpy(&ani_vec[real_count].vertex_index_vec[0], ptr, 4 * ani_vec[real_count].function_count);
							ptr += 4 * ani_vec[real_count].function_count;

							ani_vec[real_count].vertex_value_vec.resize(ani_vec[real_count].function_count * ani_vec[real_count].frame_count);
							memcpy(&ani_vec[real_count].vertex_value_vec[0], ptr, sizeof(float3) * ani_vec[real_count].function_count * ani_vec[real_count].frame_count);
							ptr += sizeof(float3) * ani_vec[real_count].function_count * ani_vec[real_count].frame_count;

							real_count++;
						}
						break;
					case 100:
						{
							ptr += 4;

							int blade_count = *(int*)ptr;
							ptr += 4;

							int frame_count = *(int*)ptr;
							ptr += 4;

							ptr += 4 + 8 * blade_count * frame_count;
						}
						break;
					*/
				}
				anim.AddClip(clip, ani_name);
			}

			//ani_vec.resize(real_count);
		}

		return true;
	}

  	[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
  	public struct head_t
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[]	comment;	
		public byte2	width;		
		public byte2	height;		
		public byte2	center_x;	
		public byte2	center_y;	
		public byte2	frame_count;
		public byte2	pal_entry_count;
		public byte2	direction_count;
		public byte2	interval;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public byte2[]	reserved;
	};

	[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct offset_table_t
	{
        public byte4 offset;
        public byte4 size;
	};

 	[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
	public struct SPRFRAME_NEW
	{
		public byte2	Width;		// 帧最小宽度
		public byte2	Height;		// 帧最小高度
		public byte2	OffsetX;	// 水平位移（相对于原图左上角）
		public byte2	OffsetY;	// 垂直位移（相对于原图左上角）
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    	public byte[]	Sprite;	// RLE压缩图形数据
	};

    public static unsafe bool LoadSpr(string filename, ref List<Texture2D> SprResList)
    {
        byte[] filedata;
        if (!OpenFile(filename, out filedata))
        {
            try
            {
                int size = CoreWrapper.GetFileSize(GLB.GBK.GetBytes(filename));
                Debug.Log("Loading " + filename + ", Size " + size);
                if (size <= 0)
                    return false;
                IntPtr read = CoreWrapper.LoadFile(GLB.GBK.GetBytes(filename));
                filedata = new byte[size];
                Marshal.Copy(read, filedata, 0, size);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

        }
        if (filedata == null)
        {
            Debug.LogError("Load Image Error: " + filename);
            return false;
        }
        if (filename.Substring(filename.Length - 3) == "spr")
            return LoadSpr(filename, filedata, ref SprResList);
        else
        {
            Texture2D tx = new Texture2D(1,1);
            tx.LoadImage(filedata);
            SprResList.Add(tx);
            return true;
        }
    }
    public static unsafe bool LoadSpr(string filename, byte[] filedata, ref List<Texture2D> SprResList)
    {
        //Head
 		head_t head = (head_t)BytesToStruct(filedata, typeof(head_t));
        //Debug.Log(head.width);

        if (head.reserved[0] != 0)
            Debug.LogError("NEW FRAME!!!" + filename);

        //Palette Colors
        int colors = head.pal_entry_count;
        Debug.Log("Color: " + colors);
        Color32[] PaletteColors = new Color32[colors];
        long off = 0;
        fixed (byte* data = &filedata[32]) // sizeof(head_t)
        {
            byte *c = data;
            byte a = 0xff;
            for (int i = 0; i < colors; i++)
            {
                byte r,g,b;
                r = *c++;
                g = *c++;
                b = *c++;
                PaletteColors[i] = new Color32(r,g,b,a);
                //Debug.Log(string.Format("Color[{0}]: {1}", i, PaletteColors[i]));
            }
            off = c - data + 32;
        }
        // frame offset
        int frameCount = head.frame_count;
        offset_table_t[] frames = new offset_table_t[frameCount];
        fixed (byte *data = &filedata[off])
        {
            int *pdata = (int *)data;
            for (int i = 0; i < frameCount; i++)
            {
                frames[i].offset = *pdata++;
                frames[i].size = *pdata++;
                Debug.Log(string.Format("Frames {0}: [{1}, {2}]", i, frames[i].offset, frames[i].size));
            }
            off += (byte*)pdata - data;
        }

        // frames
        int w = head.width;
        int h = head.height;

        fixed (byte* data = &filedata[off])
        {
            byte* c = data;
            for (int f = 0; f < head.frame_count; f++)
            {
                SPRFRAME_NEW spr = (SPRFRAME_NEW)Marshal.PtrToStructure(new IntPtr(c), typeof(SPRFRAME_NEW));
                c += 8;
                w = spr.Width;
                h = spr.Height;
                Debug.Log(string.Format("new frame {0} x {1}", w, h));
                Color32[] pixels = new Color32[w * h];
                int nNumLines = h;
                int p = 0;
                for (; nNumLines > 0; nNumLines--)
                {
                    int nLineLen = w;
                    while (nLineLen > 0)
                    {
                        int nNumPixels = *c++;
                        int nAlpha = *c++;
                        nLineLen -= nNumPixels;
                        for (int n = 0; n < nNumPixels; n++)
                        {
                            try
                            {
                                if (nAlpha == 0)
                                {
                                    pixels[p++] = new Color32(0, 0, 0, 0);
                                }
                                else
                                {
                                    pixels[p++] = PaletteColors[*c++];
                                }
                            }
                            catch (Exception e)
                            {
                                Debug.LogError(e);
                                Debug.Log(p);
                                Debug.Log(w * h);
                                return false;
                            }
                        }
                    }
                }
                Debug.Log(p);
                Debug.Log(c - data);
                Texture2D tx = new Texture2D(w, h);
                tx.SetPixels32(pixels);
                tx.Apply(false);

                SprResList.Add(tx);
            }
        }

        return true;
    }
}
