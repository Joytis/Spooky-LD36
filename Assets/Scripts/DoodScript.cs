using UnityEngine;
using System.Collections;

public class DoodScript : MonoBehaviour {

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
		prnt_tnsf = this.transform.parent.transform;
		rot_axis = prnt_tnsf.position;

		sprRnd = GetComponent<SpriteRenderer>();
		//sprRnd.color = new Color(0,0,0);

		radius_pos = this.transform.parent.GetComponent<DudeMobScript>().child_radius;
		this.transform.position = (this.transform.position * radius_pos) + prnt_tnsf.position;


		this.tag = "piece";
	}
	
	// Update is called once per frame
	void Update () {
		//col.r += Random.Range(-1f, 1f);
		//if(col.r < 0) col.r = 0;
		//if(col.r > 1) col.r = 1;
		//col.g += Random.Range(-1f, 1f); 
		//if(col.g < 0) col.g = 0;
		//if(col.g > 1) col.g = 1;
		//col.b += Random.Range(-1f, 1f);
		//if(col.b < 0) col.b = 0;
		//if(col.b > 1) col.b = 1;
//
		//if(Random.Range(-1f,1f) < 0) sprRnd.flipY = true;
		//else sprRnd.flipY = false;
//
		//if(Random.Range(-1f,1f) < 0) sprRnd.flipX = true;
		//else sprRnd.flipX = false;

		// Rotation calculations about parental center.
		rot_axis = prnt_tnsf.position;
		radial = gameObject.transform.position - rot_axis;
		radial_norm = radial.normalized;
		tangent_norm = Vector3.Cross(radial_norm, plane_tangent_norm);

		float tan = tangent_norm.y / tangent_norm.x;
		float rot =  Mathf.Atan(tan);
		float rot_dev = rot * Mathf.Rad2Deg;	

		Debug.DrawLine(prnt_tnsf.position, gameObject.transform.position, Color.red);
		Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + tangent_norm, Color.red);


		if((tangent_norm.x < 0))
		{
			sprRnd.flipX = true;
			sprRnd.flipY = true;
		}
		else{
			sprRnd.flipX = false;
			sprRnd.flipY = false;	
		}

		gameObject.transform.eulerAngles = new Vector3(0, 0, rot_dev);


		//sprRnd.transform.Rotate(new Vector3(0,0,Random.Range(0,50)));
		//sprRnd.transform.RotateAround(Vector3.zero,Vector3.forward, 50 * Time.deltaTime);

		//sprRnd.color = col;
	}
}
