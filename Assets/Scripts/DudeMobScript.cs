using UnityEngine;
using System.Collections;

public class DudeMobScript : MonoBehaviour {

	// public dude vars
	public GameObject[] 	dudes;
	public GameObject[]		correct_mask;

	public int 				right_most_index;

	public bool 			has_correct;
	// Stuff variables
	// TODO(clark): Make child_radius private and determined byt he height of the image. 
	public float 			child_radius;
	int 					segments;
	float 					segment_offset;
		
	float 					_lockedY;
	float 					rot_mag;

	float 					child_height;
		
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

	bool 					s_toggle;
		
	float rotation;
	// Use this for initialization
	void Start () {

		has_correct = false;

		segments = dudes.Length;
		Debug.Log(segments);
		segment_offset = 360.0f / segments;
		Debug.Log(segment_offset);
		tnsf = GetComponent<Transform>();

		float sin;
		float cos;
		for(int i = 0; i < segments; i++)
		{
			float seg_o = (segment_offset * i) * Mathf.Deg2Rad;
			sin = Mathf.Sin(seg_o);
			cos = Mathf.Cos(seg_o);

			Vector3 c_pos = new Vector3(cos, sin, 0);
			//c_pos += transform.position;

			//Debug.Log(c_pos);
			GameObject temp = (GameObject)Instantiate(dudes[i], c_pos, Quaternion.identity);
			temp.transform.parent = tnsf;
			temp.GetComponent<PieceInfo>().puzzle_index = i;

			child_height = (temp.GetComponent<SpriteRenderer>().sprite.rect.height) / 100f;
		}

		CircleCollider2D ccol = this.transform.GetComponents<CircleCollider2D>()[0];
		ccol.radius = child_height/2 + child_radius;

		plane_tangent_norm = Vector3.forward;

		game_cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

		s_toggle = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Private member functions
	// returns the scalar projection of proj onto vector norm
	bool check_right_most_()
	{
		bool ret_val;

		float pot_rotation = 360f - (segment_offset * right_most_index);
		//Debug.Log("pot_rotation: " + pot_rotation);
		float left_bound = pot_rotation + 10;
		//Debug.Log("left_bound: " + left_bound);
		float right_bound = pot_rotation - 10;
		//Debug.Log("right_bound: " + right_bound);
		float rot_ang = tnsf.rotation.eulerAngles.z;
		//Debug.Log("rot_ang: " + rot_ang);
		if( left_bound > rot_ang && rot_ang > right_bound )
		{
			ret_val = true;
		}
		else
		{
			ret_val = false;
		}

		return ret_val;
	}


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
		float rot_off = cur_rot % segment_offset;
		float ret_val;
		if(rot_off > segment_offset/2.0f)
		{
			ret_val = (segment_offset - rot_off);
		}
		else
		{
			ret_val = -(rot_off);
		}

		return ret_val;
	}

	//
	// MonoBehavior overrides. 
	//=====================================
	void OnMouseDown()
	{
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			Debug.Log("Mouse Down");
			//Cursor.visible = false;

			start = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
			s_toggle = true;
		}
	}

	void OnMouseDrag()
	{
		if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			&& s_toggle == true)
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

			tnsf.transform.RotateAround(rot_axis, plane_tangent_norm, (rot_mag * 20));


			start = end;
			//rot_axis = prnt_tnsf.position;
			//radial = gameObject.transform.position - rot_axis;
			//radial_norm = radial.normalized;
			//tangent_norm = Vector3.Cross(radial_norm, plane_tangent_norm);
		}
		else
		{
			s_toggle = false;
		}

	}

	void OnMouseUp()
	{
		Debug.Log("Mouse Up");
		//Cursor.visible = true;

		tnsf.transform.RotateAround(rot_axis, plane_tangent_norm, snap_rotation_());
		if(tnsf.transform.eulerAngles.z == -180)
		{
			tnsf.transform.eulerAngles = new Vector3(0, 0, 180);
		}

		has_correct = true;
		for(int i = 0; i < correct_mask.Length; i++ )
		{
			if( dudes[i] != correct_mask[i] ) has_correct = false;
		}
		if(has_correct)
		{
			has_correct = check_right_most_();
		}

		Debug.Log(has_correct);
	}

	public void add_game_object(int index, GameObject obj)
	{

	}


}
