﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{

    private Mesh mesh;
    private Mesh sphere;
    private Mesh cube;

    [SerializeField]
    private int size;
    [SerializeField]
    private float density;

    Vector3[] vertices;
    Vector2[] newUV;
    int[] triangles;

    private enum Sides { FRONT, RIGHT, BACK, LEFT, BOTTOM, TOP }
    private Vector3[] normals;

    void Start()
    {
        vertices = new Vector3[(size + 1) * (size + 1) * 6];
        triangles = new int[6 * 6 * size * size];
        normals = new Vector3[] { new Vector3(0, 0, -1), new Vector3(1, 0, 0), new Vector3(0, 0, 1), new Vector3(-1, 0, 0), new Vector3(0, -1, 0), new Vector3(0, 1, 0) };
        cube = new Mesh();
        for (int side = 0; side < 6; side += 1)
        {
            for (int y = 0; y <= size; y += 1)
            {
                for (int x = 0; x <= size; x += 1)
                {
                    if (side == 0)
                    {
                        vertices[x + y * (size + 1) + side * (size + 1) * (size + 1)] = new Vector3(x * density, y * density, 0);
                        
                    }
                    if (side == 1)
                    {
                        vertices[x + y * (size + 1) + side * (size + 1) * (size + 1)] = new Vector3(size * density, y * density, x * density);
                    }
                    if (side == 2)
                    {
                        vertices[x + y * (size + 1) + side * (size + 1) * (size + 1)] = new Vector3(size * density - x * density, y * density, size * density);
                    }
                    if (side == 3)
                    {
                        vertices[x + y * (size + 1) + side * (size + 1) * (size + 1)] = new Vector3(0, y * density, size * density - x * density);
                    }
                    if (side == 4)
                    {
                        vertices[x + y * (size + 1) + side * (size + 1) * (size + 1)] = new Vector3(x * density, 0, y * density);
                    }
                    if (side == 5)
                    {
                        vertices[x + y * (size + 1) + side * (size + 1) * (size + 1)] = new Vector3(x * density, size * density, y * density);
                    }
                }
            }
        }
        for (int side = 0; side < 6; side += 1)
        {
            for (int y = 0; y < size; y += 1)
            {
                for (int x = 0; x < size; x += 1)
                {
                    //bottom triangle
                    triangles[x * 6 + y * size * 6 + side * size * size * 6] = x + y * (size + 1) + side * (size + 1) * (size + 1);
                    triangles[1 + x * 6 + y * size * 6 + side * size * size * 6] = x + 1 + y * (size + 1) + side * (size + 1) * (size + 1);
                    triangles[2 + x * 6 + y * size * 6 + side * size * size * 6] = x + 1 + (y + 1) * (size + 1) + side * (size + 1) * (size + 1);

                    //top triangle
                    triangles[3 + x * 6 + y * size * 6 + side * size * size * 6] = x + y * (size + 1) + side * (size + 1) * (size + 1);
                    triangles[4 + x * 6 + y * size * 6 + side * size * size * 6] = x + 1 + (y + 1) * (size + 1) + side * (size + 1) * (size + 1);
                    triangles[5 + x * 6 + y * size * 6 + side * size * size * 6] = x + (y + 1) * (size + 1) + side * (size + 1) * (size + 1);
                    
                }
            }
        }
        Debug.Log(vertices[4]);
        Debug.Log(vertices[5]);
        Debug.Log(vertices[6]);
        Debug.Log(vertices[7]);

        mesh = new Mesh();
        mesh.name = "My Cube";
        mesh.vertices = vertices;
        mesh.uv = newUV;
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh = mesh;
        

    }

    void Update()
    {
        
    }
}
