using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices;
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
	struct float2
	{
		public float x;
		public float y;
	}

	[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
	struct float3
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

	public static unsafe Mesh LoadMesh(string filename, Mesh mesh)
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

		#region bone
		//skin_info = new mesh_skin_info_t;
		//skin_info.d3dsi = 0;

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
				Debug.Log(string.Format("{0},{1},{2}", ff->x, ff->y, ff->z));
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
				Debug.Log(string.Format("{0},{1},{2}", ff->x, ff->y, ff->z));
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
				Debug.Log(string.Format("{0} {1}",i, t));
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

		//SAFE_RELEASE(d3dmesh);
		//SAFE_RELEASE(skin_info.d3dsi);
		//SAFE_RELEASE(filedata);
		return mesh;
	}
	public static bool OpenFile(string filename, out byte[] data)
	{
		FileStream TextFile = File.Open(filename, FileMode.Open);
		data = new byte[TextFile.Length];
		int ret = TextFile.Read(data, 0, (int)TextFile.Length);
		TextFile.Close();
		return true;
	}
}
