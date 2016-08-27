using UnityEngine;
using System.Collections;

public class DudeMobScript : MonoBehaviour {

	// Stuff variables
	float _lockedY;

	Transform tnsf;

	Vector2 start;
	Vector2 offset;
	Vector2 end;

	float rotation;
	// Use this for initialization
	void Start () {
		tnsf = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		Cursor.visible = false;
		Debug.Log("YES");

		start = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	}

	void OnMouseDrag()
	{
		end = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		offset = end - start;
		start = end;
		rotation = -(offset.x/5);
		tnsf.transform.RotateAround(Vector3.zero, Vector3.forward, rotation);
	}

	void OnMouseUp()
	{
		Cursor.visible = true;
	}
}
