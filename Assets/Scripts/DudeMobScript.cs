using UnityEngine;
using System.Collections;

public class DudeMobScript : MonoBehaviour {

	// Stuff variables
	public float 	child_radius;
	public int 		segments;
	float 			segment_offset;

	float 			_lockedY;
	float 			rot_mag;

	Transform 		tnsf;

	Vector3 		start;
	Vector3 		end;
		
	Vector3 		w_start;
	Vector3 		w_end;
	Vector3 		w_offset;
		
	Vector3 		plane_tangent_norm;
	Vector3 		rot_axis;
		
	Vector3 		radial;
	Vector3 		radial_norm;
	Vector3 		tangent_norm;

	Camera 			game_cam;

	float rotation;
	// Use this for initialization
	void Start () {

		segment_offset = 360.0f / segments;
		tnsf = GetComponent<Transform>();
		plane_tangent_norm = Vector3.forward;
		game_cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Private member functions
	// returns the scalar projection of proj onto vector norm
	float vectorProjection_(Vector3 norm, Vector3 proj)
	{
		float angle = Vector3.Angle(norm, proj);
		float mag = proj.magnitude * (Mathf.Cos(angle));

		if(angle > 90 && mag > 0)
		{
			mag *= -1f;
		}
		if(angle < 90 && mag < 0)
		{
			mag *= -1f;
		}

		return -(mag);
	}

	float snap_rotation_()
	{
		float cur_rot = tnsf.rotation.y;
		float rot_off = cur_rot % segment_offset;
		float ret_val;
		if(rot_off > segment_offset/2.0f)
		{
			ret_val = (segment_offset - rot_off);
		}
		else
		{
			ret_val =  -(rot_off);
		}

		return ret_val;
	}

	//
	// MonoBehavior overrides. 
	//=====================================
	void OnMouseDown()
	{
		Cursor.visible = false;

		start = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
	}

	void OnMouseDrag()
	{
		end = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		//rotation = -(offset.x/5);
		//tnsf.transform.RotateAround(Vector3.zero, Vector3.forward, rotation);

		w_end = game_cam.ScreenToWorldPoint(end);
		w_start = game_cam.ScreenToWorldPoint(start);
		w_offset = w_end - w_start;

		rot_axis = tnsf.position;
		radial = w_start - rot_axis;
		radial_norm = radial.normalized;

		tangent_norm = Vector3.Cross(radial_norm, plane_tangent_norm);
		rot_mag = vectorProjection_(tangent_norm, w_offset);
		Debug.Log("rot_mag: " + rot_mag);

		tnsf.transform.RotateAround(rot_axis, plane_tangent_norm, (rot_mag * 50));


		start = end;
		//rot_axis = prnt_tnsf.position;
		//radial = gameObject.transform.position - rot_axis;
		//radial_norm = radial.normalized;
		//tangent_norm = Vector3.Cross(radial_norm, plane_tangent_norm);

	}

	void OnMouseUp()
	{
		Cursor.visible = true;
		tnsf.transform.RotateAround(rot_axis, plane_tangent_norm, snap_rotation_());
	}
}
