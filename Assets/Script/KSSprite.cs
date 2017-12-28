using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSSprite : MonoBehaviour {
	
	SpriteRenderer sr;
	Sprite sp;

	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>();
		if (!sr)
			sr = gameObject.AddComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos()
	{
	}
	public void SetSprite(Sprite sp)
	{
		sr.sprite = sp;
	}
}
