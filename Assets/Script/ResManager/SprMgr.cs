using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SprMgr : MonoBehaviour {
    static SprMgr _instance;
    public static SprMgr GetInstance()
    {
        return _instance;
    }
    [SerializeField]
    public Dictionary<long, KSSprite> SprMap = new Dictionary<long, KSSprite>();
    [SerializeField]
    public Dictionary<string, List<Texture2D>> SprResMap = new Dictionary<string, List<Texture2D>>();
    [SerializeField]
    public List<Texture2D> _res = new List<Texture2D>();

	#region 2 Buffer for View
	public GameObject front;
	public GameObject back;
	public GameObject del;
    public GameObject texts;

	public void Swap()
	{
        Transform[] grandFa = back.GetComponentsInChildren<Transform>();

        foreach (Transform child in grandFa)  
		{
            if (child != back.transform)
    			child.parent = del.transform;
		}
		GameObject temp = back;
		back = front;
		front = temp;
		front.SetActive(true);
		back.SetActive(false);

        while (texts.transform.childCount > 0)
        {
            Transform tf = texts.transform.GetChild(0);
            if (tf != null)
            {
                Destroy(tf.gameObject);
            }
        }
	}
	#endregion

	// Use this for initialization
	void Start () {
        _instance = this;
        del.SetActive(false);
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
        //Debug.Log("draw spr:" + filename + "," + nFrame);
        List<Texture2D> SprResList;
        SprResMap.TryGetValue(filename, out SprResList);
		if (SprResList == null)
		{
			SprResList = new List<Texture2D>();
            if (Utils.LoadSpr(filename, ref SprResList))
            {
                SprResMap.Add(filename, SprResList);
                _res.AddRange(SprResList);
            }
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
        Vector3 pos = new Vector3(cmds.pos1.x, cmds.pos1.y, cmds.pos1.z);
        bool bReload = false;
        string szImage = Encoding.UTF8.GetString(Encoding.Convert(GLB.GBK, Encoding.UTF8, cmds.filename));
        KSSprite ksp;
        if (!go)
		{
            go = new GameObject(szImage);
            ksp = go.AddComponent<KSSprite>();
            self.SprMap.Add(cmd_addr, ksp);
            //pos += new Vector3(tx.width/200, -tx.height/200, 0f);
            bReload = true;
        }
        else
        {
            ksp = go.GetComponent<KSSprite>();
            if (ksp.GetFrame() != cmds.frame_index)
            {
                bReload = true;
            }
        }
        if (bReload)
        {
            Texture2D tx = self.LoadSpr(szImage, cmds.frame_index);
            if (!tx)
            {
                Debug.LogError("Load Spr Error: " + szImage);
                return false;
            }
            Sprite sp = Sprite.Create(tx, new Rect(0f, 0f, tx.width, tx.height), new Vector2(0.0f, 1.0f),100.0f,0,SpriteMeshType.FullRect);
            ksp.SetSprite(sp, cmds.frame_index);
        }

		go.transform.parent = self.front.transform;
        go.transform.localPosition = pos;
        return true;
	}

    public uint ImageSeed = 7878;
    public Dictionary<long, GameObject> ImagePool = new Dictionary<long, GameObject>();
    public static uint CreateImage(string imageName, int width, int height)
    {
        SprMgr self = SprMgr.GetInstance();
        GameObject go = null;// new GameObject(imageName);
        self.ImagePool.Add(self.ImageSeed, go);
        return self.ImageSeed++;
    }

    public Dictionary<long, GameObject> TxtPool = new Dictionary<long, GameObject>();
    public static bool DrawText(long uuid, string text, float x, float y, float z = 0f)
    {
        SprMgr self = SprMgr.GetInstance();
        GameObject go;
        if (!self.TxtPool.TryGetValue(uuid, out go))
        {
            go = new GameObject(text);
            go.AddComponent<MeshRenderer>();
            TextMesh _tm = go.AddComponent<TextMesh>();
            _tm.characterSize = 0.04f;
            _tm.fontSize = 30;
            self.TxtPool.Add(uuid, go);
        }
        go.transform.parent = self.front.transform;
        TextMesh tm = go.GetComponent<TextMesh>();
        tm.text = text;
        go.transform.localPosition = new Vector3(x, y, z);
        return true;
    }

    public Dictionary<long, GameObject> modelPool = new Dictionary<long, GameObject>();
    public static void DrawModel(long uuid, space_obj_primitive_cmd_t cmds)
    {
        IntPtr obj;

        SprMgr self = SprMgr.GetInstance();
        GameObject go;
        if (!self.modelPool.TryGetValue(uuid, out go))
        {
            go = new GameObject();
            go.AddComponent<SkinnedMeshRenderer>();
            go.AddComponent<Animation>();
            go.AddComponent<KSMesh>();
            self.modelPool.Add(uuid, go);
        }
        go.transform.parent = self.front.transform;
        go.transform.localPosition = new Vector3(cmds.pos.x, cmds.pos.y, cmds.pos.z);
    }

}
