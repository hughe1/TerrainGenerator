using UnityEngine;
using System.Collections;

public class PointLight : MonoBehaviour
{
    public float moveSpeed = 40;
    public Color color;
	public float nightBoost = 10;

	private float boost = 1;

    public Vector3 GetWorldPosition()
    {
        return this.transform.position;
    }

    void Update()
    {
		if (this.transform.position.y < -500)
		{
			boost = nightBoost;
		}
		else
		{
			boost = 1;
		}

		transform.RotateAround(new Vector3(0,-500,0), new Vector3(1, 0, 0), moveSpeed * Time.deltaTime*boost);

    }
}

