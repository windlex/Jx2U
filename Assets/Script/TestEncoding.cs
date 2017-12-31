using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEncoding : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GLB.GBK = Encoding.GetEncoding("GB2312");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnDestroy()
    {
        Debug.Log("Destroyed");
    }
}
