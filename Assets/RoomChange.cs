using UnityEngine;
using System.Collections;

public class RoomChange : MonoBehaviour {

	// Use this for initialization

	SpriteRenderer sprRnd;
	public Sprite doorSprite;
	public int foo;

	void Start () {

		sprRnd = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
