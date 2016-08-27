using UnityEngine;
using System.Collections;

public class DoodScript : MonoBehaviour {

	// Stuff variables
	float _lockedY;
	SpriteRenderer sprRnd;
	Color col = new Color(0,0,0);

	Vector3 plane_tangent_norm;
	Vector3 rot_axis;

	float radius_pos;
	Vector3 radial;
	Vector3 radial_norm;
	Vector3 tangent_norm;

	Transform prnt_tnsf;

	public Sprite dudes;

	// Use this for initialization
	void Start () {
		tangent_norm = Vector3.zero;
		plane_tangent_norm = Vector3.forward;
		rot_axis = Vector3.zero;

		sprRnd = GetComponent<SpriteRenderer>();
		sprRnd.color = new Color(0,0,0);

		radius_pos = this.transform.parent.GetComponent<DudeMobScript>().child_radius;
		this.transform.position = this.transform.position.normalized * radius_pos;

		prnt_tnsf = this.transform.parent.GetComponent<Transform>();
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

		// Rotation calculations about parental center.
		rot_axis = prnt_tnsf.position;
		radial = gameObject.transform.position - rot_axis;
		radial_norm = radial.normalized;
		tangent_norm = Vector3.Cross(radial_norm, plane_tangent_norm);

		float rot =  Mathf.Atan(tangent_norm.y / tangent_norm.x);

		gameObject.transform.eulerAngles = new Vector3(0, 0, rot * Mathf.Rad2Deg);


		//sprRnd.transform.Rotate(new Vector3(0,0,Random.Range(0,50)));
		//sprRnd.transform.RotateAround(Vector3.zero,Vector3.forward, 50 * Time.deltaTime);

		sprRnd.color = col;
	}
}
