using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour
{

    public float moveSpeed;
    public Color color;

    public Vector3 GetWorldPosition()
    {
        return this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), moveSpeed * Time.deltaTime);

    }
}