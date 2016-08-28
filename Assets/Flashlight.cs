using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{
    private Vector3 offset;

    // Use this for initialization
    void Start ()
    {
	}

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -0.75f));
    }

}
