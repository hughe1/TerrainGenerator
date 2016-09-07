using UnityEngine;
using System.Collections;

public class GenerateTerrainTexture : MonoBehaviour {

	public int n;
	public float r = 0.5f;
	public float p = 1.0f;

	public int snow = 550;
	public int rockMax = 500;
	public int rockMin = 450;
	public int grassMax = 350;
	public int grassMin = 280;
	public int sand = 260;
	public float textureRand = 0.1f;
	public int cornerMin = 250;
	public int cornerMax = 480;

    public Shader shader;
    public PointLight sun;

    private float[,] a;
	

	void Start () {

        //Vector3 UNITY_LIGHTMODEL_AMBIENT = new Vector3(1.0F, 1.0F, 1.0F);

            //r = r * n;

            //init empty 2d array
            a = new float[n,n];

		init ();

		// set corners
		setCorners (r, n);

		//perform ds algorithm recursively
		int i = n-1;
		while (i > 0) {
			if (ds (i, n, r)==true) { break; }
			i = i/2;
			r = r *p;
		}

		//printArray ();

		Terrain.activeTerrain.terrainData.SetHeights(0, 0, a);

		setSplat ();

        //ADDED


        // Add a MeshFilter component to this entity. This essentially comprises of a
        // mesh definition, which in this example is a collection of vertices, colours 
        // and triangles (groups of three vertices). 
        //MeshFilter cubeMesh = this.gameObject.AddComponent<MeshFilter>();
        //cubeMesh.mesh = this.CreateCubeMesh();

        // Add a MeshRenderer component. This component actually renders the mesh that
        // is defined by the MeshFilter component.

        //MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
        // renderer.material.shader = shader;



        //commented out below
        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        //Vector3[] vertices = mesh.vertices;
        //Color[] colors = new Color[vertices.Length];

        //for (int j = 0; j < vertices.Length; j++)
        //    colors[j] = new Color(0.8F, 0.3F, 0.1F, 0.5F);

        //mesh.colors = colors;


        //end added

    }

	float rand(float r)
	{
		return (Random.value*r)-(r/2)+1;

	}

	float randBetween(int min, int max) {
		return min + (max - min) * Random.value;
	}

	void init()
	{
		for (int k = 0; k<n;k++) {
			for (int j = 0; j<n;j++) {
				a[k,j] = -1.0f;
			}
		}
	}

	bool ds(int squaresize, int n, float f) {

		for (int i = 0; i < n-squaresize; i += squaresize) {
			for (int j = 0; j < n-squaresize; j += squaresize)
				square (i, j, squaresize, n, r);
		}

		if (n==1) { return true; }
		for (int i = 0; i < n-squaresize; i += squaresize) {
			for (int j = 0; j < n-squaresize; j += squaresize)
				diamond (i, j, squaresize, n, r);
		}
		return false;
	}

	void square(int x, int y, int squaresize, int n, float r) {
		setPoint (x, y, squaresize, n, r);
		setPoint (x+squaresize, y, squaresize, n, r);
		setPoint (x+squaresize, y+squaresize, squaresize, n, r);
		setPoint (x, y+squaresize, squaresize, n, r);
	}

	void diamond(int x, int y, int squaresize, int n, float r) {
		setPointD (x+squaresize/2, y+squaresize/2, squaresize, n, r);
	}
		
	void setPoint(int x, int y, int squaresize, int n, float r) {
		float total = 0;
		float num = 0;

		if (a[x,y] >= 0) { return; }

		if ((x-squaresize) >= 0 && a[x-squaresize,y] >= 0) {
			total += a[x-squaresize, y]* rand(r);
			num +=1;
		}
		if ((x+squaresize) < n && a[x+squaresize,y] >= 0) {
			total += a[x+squaresize, y]* rand(r);
			num +=1;
		}
		if ((y-squaresize) >= 0 && a[x,y-squaresize] >= 0) {
			total += a[x, y-squaresize]* rand(r);
			num +=1;
		}
		if ((y+squaresize) < n && a[x,y+squaresize] >= 0) {
			total += a[x, y+squaresize]* rand(r);
			num +=1;
		}

		total = total/num;
		a [x, y] = (total);

	}

	void setPointD(int x, int y, int squaresize, int n, float r) {
		float total = 0;
		float num = 0;

		int s = squaresize / 2;

		if (a[x,y] >= 0) { return; }

			total += a[x-s, y-s]* rand(r);
			total += a[x+s, y+s]* rand(r);
			total += a[x+s, y-s]* rand(r);
			total += a[x-s, y+s]* rand(r);
		num +=4;

		total = total/num;
		a [x, y] = (total);

	}

	void printArray() {
		for (int k = 0; k < n; k++) {
			for (int j = 0; j < n; j++) {
				if (a [k, j] > 0.99) {
					print (a [k, j].ToString ());
				}

			}
		}
	}

	void setCorners(float r, int n) {
		a [0, 0] = randBetween (cornerMin, cornerMax)/600;
		a [0, n-1] = randBetween (cornerMin, cornerMax)/600;
		a [n-1, n-1] = randBetween (cornerMin, cornerMax)/600;
		a [n-1, 0] = randBetween (cornerMin, cornerMax)/600;
	}
		
	void setSplat () {

		TerrainData t = Terrain.activeTerrain.terrainData;
		float[,,] map = new float[t.alphamapWidth, t.alphamapHeight, 4];

		// For each point on the alphamap...
		for (int y = 0; y < t.alphamapHeight; y++) {
			for (int x = 0; x < t.alphamapWidth; x++) {
				// Get the normalized terrain coordinate that
				// corresponds to the the point.
				float normX = x * 1.0f / (t.alphamapWidth - 1);
				float normY = y * 1.0f / (t.alphamapHeight - 1);

				float height = t.GetHeight(Mathf.RoundToInt(normY * t.heightmapHeight),Mathf.RoundToInt(normX * t.heightmapWidth) );
				Mathf.Clamp01((t.heightmapHeight - height));

				height = Mathf.FloorToInt((height * rand (textureRand)));


				map[x, y, 0] = 0.0f;
				map [x, y, 1] = 0.0f;
				map[x, y, 2] = 0.0f;
				map[x, y, 3] = 0.0f;

				if (height > rockMax) {
					map [x, y, 1] = 1.0f - (height - rockMax) / (snow-rockMax);
					map [x, y, 3] = (height - rockMax) / (snow-rockMax);
				} else if (height > rockMin) {
					map [x, y, 1] = 1.0f;
				} else if (height > grassMax) {
					map [x, y, 0] = 1.0f - (height - grassMax) / (rockMin-grassMax);
					map [x, y, 1] = (height - grassMax) / (rockMin-grassMax);
				} else if (height > grassMin) {
					map [x, y, 0] = 1.0f;
				} else if (height > sand) {
					map [x, y, 2] = 1.0f - (height - sand) / (grassMin-sand);
					map [x, y, 0] = (height - sand) / (grassMin-sand);
				} else {
					map [x, y, 2] = 1.0f;
				}
			}
		}
		t.SetAlphamaps(0, 0, map);
	}
		
	void OnTriggerEnter(Collider other) {
		Application.Quit ();
	}

    void Update()
    {

        Terrain t = Terrain.activeTerrain;//.terrainData;

        t.materialType = Terrain.MaterialType.Custom;
        t.materialTemplate = new Material(shader);
        t.materialTemplate.SetColor("_PointLightColor", this.sun.color);
        t.materialTemplate.SetVector("_PointLightPosition", this.sun.GetWorldPosition());


        // Get renderer component (in order to pass params to shader)
        //MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        // Pass updated light positions to shader
        //renderer.material.SetColor("_PointLightColor", this.sun.color);
        //renderer.material.SetVector("_PointLightPosition", this.sun.GetWorldPosition());
    }
}
