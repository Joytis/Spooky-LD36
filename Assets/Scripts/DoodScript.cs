using UnityEngine;
using System.Collections;

public class DoodScript : MonoBehaviour {

	// Stuff variables
	float _lockedY;
	SpriteRenderer sprRnd;
	Color col = new Color(0,0,0);

	public int dude;
	public Sprite dudes;

	// Use this for initialization
	void Start () {
		dude = 12;
		sprRnd = GetComponent<SpriteRenderer>();
		sprRnd.color = new Color(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		col.r += Random.Range(-1f, 1f);
		if(col.r < 0) col.r = 0;
		if(col.r > 1) col.r = 1;
		col.g += Random.Range(-1f, 1f); 
		if(col.g < 0) col.g = 0;
		if(col.g > 1) col.g = 1;
		col.b += Random.Range(-1f, 1f);
		if(col.b < 0) col.b = 0;
		if(col.b > 1) col.b = 1;

		if(Random.Range(-1f,1f) < 0) sprRnd.flipY = true;
		else sprRnd.flipY = false;

		if(Random.Range(-1f,1f) < 0) sprRnd.flipX = true;
		else sprRnd.flipX = false;

		sprRnd.transform.Rotate(new Vector3(0,0,Random.Range(0,50)));
		//sprRnd.transform.RotateAround(Vector3.zero,Vector3.forward, 50 * Time.deltaTime);

		sprRnd.color = col;
	}
}
