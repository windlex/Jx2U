using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class KSpr : MonoBehaviour {
    public string sprPath;
    SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        string file = @"\UImage\第三版\ib系统\ib系统_底图.spr";
        CoreWrapper.InitFile();
        int size = CoreWrapper.GetFileSize(GLB.GBK.GetBytes(file));
        Debug.Log("size = " + size);
        byte[] data = new byte[size];
        //int read = CoreWrapper.LoadFile2(GLB.GBK.GetBytes(file), ref data, size);
        IntPtr read = CoreWrapper.LoadFile(GLB.GBK.GetBytes(file));
        Marshal.Copy(read, data, 0, size);
        Debug.Log(read);

        sr = gameObject.GetComponent<SpriteRenderer>();
        //Sprite sp = sr.sprite;
        //Color32[] colors1 = sp.texture.GetPixels32();
        //Debug.Log(colors1);


        //Color32[] colors = new Color32[100 * 100];
        //for (int i = 0; i < 100 * 100; i++)
        //{
        //    byte c = (byte)i;
        //    colors[i] = new Color32(c, c, c, c);
        //}
        //Texture2D tx = new Texture2D(100, 100);
        //tx.SetPixels32(colors);
        //Sprite newsp = Sprite.Create(tx, new Rect(0f, 0f, 100f, 100f), new Vector2(0.5f, 0.5f));

        Texture2D tx = SprMgr.GetInstance().LoadSpr(sprPath, 0);
        Sprite newsp = Sprite.Create(tx, new Rect(0f, 0f, tx.width, tx.height), new Vector2(0f, 1f));
        sr.sprite = newsp;
        sr.flipY = true;
        //transform.localPosition -= new Vector3(tx.width / 200, tx.height / 200, 0f);
    }

    int interval = 60;
    int nFrame = 0;
	// Update is called once per frame
	void FixedUpdate () {
        interval--;
        if (interval == 0)
        {
            nFrame++;
            interval = 60;
            Texture2D tx = SprMgr.GetInstance().LoadSpr(sprPath, nFrame);
            if (tx == null)
            {
                nFrame = -1;
                return;
            }
            Sprite newsp = Sprite.Create(tx, new Rect(0f, 0f, tx.width, tx.height), new Vector2(0.0f, 1.0f));
            sr.sprite = newsp;
        }
	}
}
