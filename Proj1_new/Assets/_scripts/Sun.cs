//Hugh Edwards (584183) University of Melbourne Graphics and Interaction 2016
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

    void Update()
    {
        transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), moveSpeed * Time.deltaTime);

    }
}