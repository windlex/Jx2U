using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testpose : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Animation animation = gameObject.GetComponent<Animation>();

		{
			AnimationCurve curve1 = AnimationCurve.Linear(0.0F, 1.0F, 2.0F, 0.0F);
			AnimationClip clip1 = new AnimationClip();
			clip1.legacy = true;
			clip1.SetCurve("", typeof(Transform), "localPosition.x", curve1);
			animation.AddClip(clip1, "test");
		}

		SkinnedMeshRenderer renderer = GetComponent<SkinnedMeshRenderer>();

		Mesh mesh = new Mesh();

		mesh.vertices = new Vector3[] { new Vector3(1, 0, 0), new Vector3(1, 5, 0), new Vector3(2, 5, 0), new Vector3(2, 10, 0) };
		Debug.Log(mesh.vertices);
		mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

		mesh.triangles = new int[] { 0, 1, 2, 1, 3, 2 };

		mesh.RecalculateNormals();

		renderer.material = new Material(Shader.Find(" Diffuse"));

		BoneWeight[] weights = new BoneWeight[4];

		weights[0].boneIndex0 = 0;

		weights[0].weight0 = 1;

		weights[1].boneIndex0 = 0;

		weights[1].weight0 = 1;

		weights[2].boneIndex0 = 1;

		weights[2].weight0 = 1;

		weights[3].boneIndex0 = 1;

		weights[3].weight0 = 1;

		mesh.boneWeights = weights;

		Transform[] bones = new Transform[2];

		Matrix4x4[] bindPoses = new Matrix4x4[2];

		bones[0] = new GameObject("Lower").transform;

		bones[0].parent = transform;

		bones[0].localRotation = Quaternion.identity;

		bones[0].localPosition = Vector3.zero;

		bindPoses[0] = bones[0].ToMatrix4x4().inverse;//Matrix4x4.zero;// bones[0].worldToLocalMatrix * transform.localToWorldMatrix;
		Debug.Log(bindPoses[0]);
		bones[1] = new GameObject("Upper").transform;

		bones[1].parent = transform;

		bones[1].localRotation = Quaternion.identity;

		bones[1].localPosition = new Vector3(0, 5, 0);

		bindPoses[1] = bones[1].ToMatrix4x4().inverse; //Matrix4x4.zero; //bones[1].worldToLocalMatrix * transform.localToWorldMatrix;
		Debug.Log(bindPoses[1]);

		mesh.bindposes = bindPoses;

		renderer.bones = bones;

		renderer.sharedMesh = mesh;

		AnimationCurve curve = new AnimationCurve();

		curve.keys = new Keyframe[] { new Keyframe(0, 0, 0, 0), new Keyframe(1, 3, 0, 0), new Keyframe(2, 0.0F, 0, 0) };

		AnimationClip clip = new AnimationClip();
		clip.legacy = true;
		clip.wrapMode = WrapMode.Loop;
		clip.SetCurve("Lower", typeof(Transform), "localPosition.z", curve);
		animation.AddClip(clip, "test1");

		animation.Play("test1");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
