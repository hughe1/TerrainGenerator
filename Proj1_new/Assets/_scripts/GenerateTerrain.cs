//Hugh Edwards (584183) University of Melbourne Graphics and Interaction 2016

using UnityEngine;
using System.Collections;

public class GenerateTerrain : MonoBehaviour {

	public int n;
	public float r = 0.5f;
	public float p = 1.0f;

	public int cornerMin = 250;
	public int cornerMax = 480;

    public Shader shader;
    public PointLight sun;

    private float[,] a;
	

	void Start () {

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

		Terrain.activeTerrain.terrainData.SetHeights(0, 0, a);

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


	void setCorners(float r, int n) {
		a [0, 0] = randBetween (cornerMin, cornerMax)/600;
		a [0, n-1] = randBetween (cornerMin, cornerMax)/600;
		a [n-1, n-1] = randBetween (cornerMin, cornerMax)/600;
		a [n-1, 0] = randBetween (cornerMin, cornerMax)/600;
	}
		

		
	void OnTriggerEnter(Collider other) {
		Application.Quit ();
	}

    void Update()
    {

        Terrain t = Terrain.activeTerrain;

        t.materialType = Terrain.MaterialType.Custom;
        t.materialTemplate = new Material(shader);
        t.materialTemplate.SetColor("_PointLightColor", this.sun.color);
        t.materialTemplate.SetVector("_PointLightPosition", this.sun.GetWorldPosition());
    }
}
