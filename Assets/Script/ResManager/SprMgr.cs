using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprMgr : MonoBehaviour {
	public Dictionary<long, KSSprite> SprMap = new Dictionary<long, KSpr>();
	public Dictionary<string, List<Texture2D>> SprResMap = new Dictionary<string, List<Texture2D>>();
	
	#region 2 Buffer for View
	public GameObject front;
	public GameObject back;
	public GameObject del;
	public void Swap()
	{
		for each child in back
		{
			child.transform.present = del.transform;
		}
		GameObject temp = back;
		back = front;
		front = temp;
		front.active = true;
		back.active = false;
	}
	#endregion

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public GameObject GetSpr(long uuid)
	{
		KSSprite spr;
		SprMap.TryGetValue(uuid, out spr);
		if (spr)
			return spr.gameObject;
		
		return null;
	}

	public Sprite LoadSpr(string filename, int nFrame)
	{
		//filename = filename.lower();
		List<Texture2D> SprResList = SprResMap.TryGetValue(filename);
		if (!SprResList)
		{
			SprResList = new List<Texture2D>();
			Utils.LoadSpr(filename, ref SprResList);
			SprResMap.Add(filename, SprResList);
		}
		if (SprResList.Count < nFrame)
			return null;
		return SprResList[nFrame];
	}
	public void RemoveSpr(KSSprite spr)
	{
		SprMap.Earse(spr);
	}
	public static bool DrawSpr(long cmd_addr, spr_cmd cmds)
	{
		this = SprMap.GetInstance();
		GameObject go = GetSpr(cmd_addr);
		if (!go)
		{
			Texture2D tx = LoadSpr(cmds.szImage, nFrame);
			if (!tx)
			{
				Debug.LogError("Load Spr Error: " + cmds.szImage);
				return false;
			}
			Sprite sp = Sprite.Create(tx, new Rect(0f,0f,tx.Width,tx.Height), new Vector2(0.5f, 0.5f));
			go = new GameObject();
			KSSprite ksp = go.AddComponent<KSSprite>();
			ksp.SetSprite(sp);
			SprMap.Add(cmd_addr, ksp);
		}
		go.transform.present = front.transform;
		go.transform.localposition = new Vector3(cmds.pos.x, cmds.pos.y, cmds.pos.z);
	}
}
