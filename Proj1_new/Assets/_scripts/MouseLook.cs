using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {


	public float sensitivity = 10F;
	public float minY = -60F;
	public float maxY = 60F;
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

		//float zRad = Mathf.Deg2Rad * rotationZ;
		//float rotationX = transform.localEulerAngles.y + (Input.GetAxis("Mouse Y") * Mathf.Sin(zRad) + Input.GetAxis("Mouse X") * Mathf.Cos(zRad))* sensitivity;


		rotationY += (Input.GetAxis("Mouse Y") * sensitivity);

		//rotationY  += (Input.GetAxis("Mouse Y") * Mathf.Cos(zRad) + Input.GetAxis("Mouse X") * Mathf.Sin(zRad))* sensitivity;

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

        //un comment all except the firdt terrainheight assignment
		//TerrainData t = Terrain.activeTerrain.terrainData;


		//float terrainHeight = t.GetHeight(Mathf.RoundToInt(this.transform.position.z-500),Mathf.RoundToInt(this.transform.position.x-500));

		//float normX = (this.transform.position.x-500) * 1.0f / (t.alphamapWidth - 1);
		//float normY = (this.transform.position.z-500) * 1.0f / (t.alphamapHeight - 1);

		//float terrainHeight = t.GetHeight(Mathf.RoundToInt(normY * t.heightmapHeight),Mathf.RoundToInt(normX * t.heightmapWidth) );

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