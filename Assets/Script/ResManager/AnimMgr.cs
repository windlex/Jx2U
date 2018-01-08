using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	struct AnimList {
		string[] aniName;
		AnimationClip[] clip;
	}
	
	// todo: LRU
	public Dictionary<string, AnimList> AniMap = new Dictionary<string, AnimList>();
	public bool GetAni(string aniName, ref Animation anim)
	{
		AnimList animList;
		if (!AnimMap.TryGetValue(aniName, out animList))
		{
			animList = new AnimList();
			bool ret = Utils.LoadAnim(anipath, ref animList);
			if (!ret)
				return false;
			AnimMap.Add(aniName, animList);
		}
		foreach (auto var in animList){
			AnimationClip clip = var.key; 
			string ani_name = var.value;
			anim.AddClip(clip, ani_name)
		}
		return true;
	}
}
