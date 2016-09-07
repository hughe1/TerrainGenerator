//Hugh Edwards (584183) University of Melbourne Graphics and Interaction 2016
using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {


	public float sensitivity = 10F;
	public float minY = -90F;
	public float maxY = 90F;
	public float speed=10.0F;
	public float boostFactor = 4.0F;
	public float tiltSpeed = 30.0f;
	public Vector3 startPosition = new Vector3 (0, 1, -10);


	private float rotationY = 0F;
	private float boost=1.0F;
	private float rotationZ = 0.0f;


	void Update ()
	{

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.visible == true)
            { Cursor.visible = false; }
            else
            { Cursor.visible = true; }
        }

        float rotationX = transform.localEulerAngles.y + (Input.GetAxis("Mouse X") * sensitivity);


		rotationY += (Input.GetAxis("Mouse Y") * sensitivity);

		rotationY = Mathf.Clamp (rotationY, minY, maxY);


		if (Input.GetKey (KeyCode.Q)) {
			rotationZ += tiltSpeed*Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.E)) {
			rotationZ -= tiltSpeed*Time.deltaTime;
		}

		transform.localEulerAngles = new Vector3(-rotationY, rotationX, rotationZ);

		if (Input.GetKey (KeyCode.LeftShift)) {
			boost = boostFactor;
		}
		else {
			boost = 1;
		}
		if (Input.GetKey (KeyCode.W)) {
			
			this.transform.position += transform.forward*speed*Time.deltaTime*boost;

		}
		if (Input.GetKey (KeyCode.A)) {
			this.transform.position -= transform.right * speed*Time.deltaTime*boost;
		}
		if (Input.GetKey (KeyCode.S)) {
			this.transform.position -= transform.forward*speed*Time.deltaTime*boost;
		}
		if (Input.GetKey (KeyCode.D)) {
			this.transform.position += transform.right*speed*Time.deltaTime*boost;
		}

		this.transform.position = new Vector3 (
			Mathf.Clamp(this.transform.position.x, -500, 500),
			Mathf.Clamp(this.transform.position.y, -1000, 1000),
			Mathf.Clamp(this.transform.position.z, -500, 500));

	}

	void Start ()
	{
		Cursor.visible = false;
	}
	
	void OnTriggerEnter(Collider other) 
	{
		this.transform.position = startPosition;
	}
	void OnTriggerStay(Collider other) 
	{
		this.transform.position = startPosition;
	}
}