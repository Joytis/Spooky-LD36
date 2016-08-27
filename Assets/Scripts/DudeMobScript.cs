using UnityEngine;
using System.Collections;

public class DudeMobScript : MonoBehaviour {

	// public dude vars
	public GameObject[] 	dudes;

	// Stuff variables
	// TODO(clark): Make child_radius private and determined byt he height of the image. 
	public float 			child_radius;
	int 					segments;
	float 					segment_offset;
		
	float 					_lockedY;
	float 					rot_mag;
		
	Transform 				tnsf;
		
	Vector3 				start;
	Vector3 				end;
				
	Vector3 				w_start;
	Vector3 				w_end;
	Vector3 				w_offset;
				
	Vector3 				plane_tangent_norm;
	Vector3 				rot_axis;
				
	Vector3 				radial;
	Vector3 				radial_norm;
	Vector3 				tangent_norm;
		
	Camera 					game_cam;
		
	float rotation;
	// Use this for initialization
	void Start () {


		segments = dudes.Length;
		Debug.Log(segments);
		segment_offset = 360.0f / segments;
		Debug.Log(segment_offset);
		tnsf = GetComponent<Transform>();

		for(int i = 0; i < segments; i++)
		{
			Vector3 norm = new Vector3(1, 0, 0);
			Vector3 c_pos = Quaternion.Euler(0, 0, (segment_offset * i)) * norm;
			GameObject temp = (GameObject)Instantiate(dudes[i], c_pos, Quaternion.identity);
			temp.transform.parent = tnsf;
		}

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
		float cur_rot = tnsf.rotation.eulerAngles.z;
		Debug.Log(cur_rot);
		float rot_off = cur_rot % segment_offset;
		Debug.Log(rot_off);
		float ret_val;
		if(rot_off > segment_offset/2.0f)
		{
			ret_val = (segment_offset - rot_off);
		}
		else
		{
			ret_val = -(rot_off);
		}
		Debug.Log(ret_val);

		return ret_val;
	}

	//
	// MonoBehavior overrides. 
	//=====================================
	void OnMouseDown()
	{
		Debug.Log("Mouse Down");
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

		tnsf.transform.RotateAround(rot_axis, plane_tangent_norm, (rot_mag * 50));

		Debug.Log(tnsf.rotation.z);

		start = end;
		//rot_axis = prnt_tnsf.position;
		//radial = gameObject.transform.position - rot_axis;
		//radial_norm = radial.normalized;
		//tangent_norm = Vector3.Cross(radial_norm, plane_tangent_norm);

	}

	void OnMouseUp()
	{
		Debug.Log("Mouse Up");
		Cursor.visible = true;
		//Debug.Log(snap_rotation_());
		tnsf.transform.RotateAround(rot_axis, plane_tangent_norm, snap_rotation_());
	}
}
