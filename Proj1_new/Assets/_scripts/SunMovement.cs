using UnityEngine;
using System.Collections;

public class SunMovement : MonoBehaviour {

	public float moveSpeed;

	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(Vector3.zero, new Vector3 (1,0,0), moveSpeed * Time.deltaTime);

	}
}
