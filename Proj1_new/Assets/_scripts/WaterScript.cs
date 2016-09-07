using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {
    public Shader shader;
    public PointLight sun;

    // Use this for initialization
    void Start()
    {
        // Add a MeshFilter component to this entity. This essentially comprises of a
        // mesh definition, which in this example is a collection of vertices, colours 
        // and triangles (groups of three vertices). 
        //MeshFilter cubeMesh = this.gameObject.AddComponent<MeshFilter>();
        //cubeMesh.mesh = this.CreateCubeMesh();

        // Add a MeshRenderer component. This component actually renders the mesh that
        // is defined by the MeshFilter component.

        //MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
       // renderer.material.shader = shader;

        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        renderer.material.shader = shader;


        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
            colors[i] = new Color (0.2F, 0.3F, 0.5F, 0.5F);

        mesh.colors = colors;




    }

    // Called each frame
    void Update()
    {
        // Get renderer component (in order to pass params to shader)
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        // Pass updated light positions to shader
        renderer.material.SetColor("_PointLightColor", this.sun.color);
        renderer.material.SetVector("_PointLightPosition", this.sun.GetWorldPosition());
    }
}
