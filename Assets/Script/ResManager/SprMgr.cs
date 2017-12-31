using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprMgr : MonoBehaviour {
    static SprMgr _instance;
    public static SprMgr GetInstance()
    {
        return _instance;
    }
    public Dictionary<long, KSSprite> SprMap = new Dictionary<long, KSSprite>();
	public Dictionary<string, List<Texture2D>> SprResMap = new Dictionary<string, List<Texture2D>>();
	
	#region 2 Buffer for View
	public GameObject front;
	public GameObject back;
	public GameObject del;
	public void Swap()
	{
        Transform[] grandFa = back.GetComponentsInChildren<Transform>();

        foreach (Transform child in grandFa)  
		{
			child.transform.parent = del.transform;
		}
		GameObject temp = back;
		back = front;
		front = temp;
		front.SetActive(true);
		back.SetActive(false);
	}
	#endregion

	// Use this for initialization
	void Start () {
        _instance = this;
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

    public Texture2D LoadSpr(string filename, int nFrame)
	{
		//filename = filename.lower();
        List<Texture2D> SprResList;
        SprResMap.TryGetValue(filename, out SprResList);
		if (SprResList == null)
		{
			SprResList = new List<Texture2D>();
			if (Utils.LoadSpr(filename, ref SprResList))
    			SprResMap.Add(filename, SprResList);
		}
		if (SprResList.Count <= nFrame)
			return null;
		return SprResList[nFrame];
	}
	public void RemoveSpr(KSSprite spr)
	{
		//SprMap.(spr);
	}
    public static bool DrawSpr(long cmd_addr, pic_cmd_t cmds)
	{
        
        SprMgr self = SprMgr.GetInstance();
        GameObject go = self.GetSpr(cmd_addr);
		if (!go)
		{
            string szImage = GLB.GBK.GetString(cmds.filename);
            Debug.Log(szImage);
            Texture2D tx = self.LoadSpr(szImage, cmds.frame_index);
			if (!tx)
			{
                Debug.LogError("Load Spr Error: " + szImage);
				return false;
			}
			Sprite sp = Sprite.Create(tx, new Rect(0f,0f,tx.width,tx.height), new Vector2(0.5f, 0.5f));
			go = new GameObject();
			KSSprite ksp = go.AddComponent<KSSprite>();
			ksp.SetSprite(sp);
            self.SprMap.Add(cmd_addr, ksp);
		}
		go.transform.parent = self.front.transform;
		go.transform.localPosition = new Vector3(cmds.pos1.x, cmds.pos1.y, cmds.pos1.z);
        return true;
	}
}
