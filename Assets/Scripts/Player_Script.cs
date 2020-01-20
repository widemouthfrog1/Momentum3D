using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    
    private Mesh sphere;
    private Mesh cube;

    [SerializeField]
    private int size;
    [SerializeField]
    private float density;
    [SerializeField]
    private float interpolationFrames;

    Vector3[] vertices;
    //Vector2[] newUV;
    Vector3[] normals;
    int[] triangles;

    private enum Sides { FRONT, RIGHT, BACK, LEFT, BOTTOM, TOP }
    private Vector3[] possibleNormals;
    private bool change = false;
    private bool changing = false;
    private int direction = -1;
    private int frameNumber = 0;

    void Start()
    {
        vertices = new Vector3[(size + 1) * (size + 1) * 6];
        triangles = new int[6 * 6 * size * size];
        possibleNormals = new Vector3[] { new Vector3(0, 0, -1), new Vector3(1, 0, 0), new Vector3(0, 0, 1), new Vector3(-1, 0, 0), new Vector3(0, -1, 0), new Vector3(0, 1, 0) };
        normals = new Vector3[(size + 1) * (size + 1) * 6];
        for (int side = 0; side < 6; side += 1)
        {
            for (int y = 0; y <= size; y += 1)
            {
                for (int x = 0; x <= size; x += 1)
                {
                    if (side == 0)
                    {
                        vertices[x + y * (size + 1) + side * (size + 1) * (size + 1)] = new Vector3(-size * density / 2 + x * density, -size * density / 2 + y * density, -size * density / 2);
                        
                    }
                    if (side == 1)
                    {
                        vertices[x + y * (size + 1) + side * (size + 1) * (size + 1)] = new Vector3(-size * density / 2 + size * density, -size * density / 2 + y * density, -size * density / 2 + x * density);
                    }
                    if (side == 2)
                    {
                        vertices[x + y * (size + 1) + side * (size + 1) * (size + 1)] = new Vector3(-size * density / 2 + size * density - x * density, -size * density / 2 + y * density, -size * density / 2 + size * density);
                    }
                    if (side == 3)
                    {
                        vertices[x + y * (size + 1) + side * (size + 1) * (size + 1)] = new Vector3(-size * density / 2, -size * density / 2 + y * density, -size * density / 2 + size * density - x * density);
                    }
                    if (side == 4)
                    {
                        vertices[x + y * (size + 1) + side * (size + 1) * (size + 1)] = new Vector3(-size * density / 2 + size * density - x * density, -size * density / 2, -size * density / 2 + y * density);
                    }
                    if (side == 5)
                    {
                        vertices[x + y * (size + 1) + side * (size + 1) * (size + 1)] = new Vector3(-size * density / 2 + x * density, -size * density / 2 + size * density, -size * density / 2 + y * density);
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
                    triangles[1 + x * 6 + y * size * 6 + side * size * size * 6] = x + 1 + (y + 1) * (size + 1) + side * (size + 1) * (size + 1);
                    triangles[2 + x * 6 + y * size * 6 + side * size * size * 6] = x + 1 + y * (size + 1) + side * (size + 1) * (size + 1);
                    

                    //top triangle
                    triangles[3 + x * 6 + y * size * 6 + side * size * size * 6] = x + y * (size + 1) + side * (size + 1) * (size + 1);
                    triangles[4 + x * 6 + y * size * 6 + side * size * size * 6] = x + (y + 1) * (size + 1) + side * (size + 1) * (size + 1);
                    triangles[5 + x * 6 + y * size * 6 + side * size * size * 6] = x + 1 + (y + 1) * (size + 1) + side * (size + 1) * (size + 1);

                }
            }
        }
        for (int side = 0; side < 6; side += 1)
        {
            for (int y = 0; y <= size; y += 1)
            {
                for (int x = 0; x <= size; x += 1)
                {
                    if (side == 0)
                    {
                        normals[x + y * (size + 1) + side * (size + 1) * (size + 1)] = possibleNormals[(int)Sides.FRONT];

                    }
                    if (side == 1)
                    {
                        normals[x + y * (size + 1) + side * (size + 1) * (size + 1)] = possibleNormals[(int)Sides.RIGHT];
                    }
                    if (side == 2)
                    {
                        normals[x + y * (size + 1) + side * (size + 1) * (size + 1)] = possibleNormals[(int)Sides.BACK];
                    }
                    if (side == 3)
                    {
                        normals[x + y * (size + 1) + side * (size + 1) * (size + 1)] = possibleNormals[(int)Sides.LEFT];
                    }
                    if (side == 4)
                    {
                        normals[x + y * (size + 1) + side * (size + 1) * (size + 1)] = possibleNormals[(int)Sides.BOTTOM];
                    }
                    if (side == 5)
                    {
                        normals[x + y * (size + 1) + side * (size + 1) * (size + 1)] = possibleNormals[(int)Sides.TOP];
                    }
                }
            }
        }
        
        
        Mesh mesh = new Mesh();
        mesh.name = "My Cube";
        mesh.vertices = vertices;
        //mesh.uv = newUV;
        mesh.triangles = triangles;
        mesh.normals = normals;
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        cube = mesh;
    }

    void Update()
    {
        if (Input.GetButtonDown("Change"))
        {
            change = true;
        }
    }

    private void FixedUpdate()
    {
        if (change)
        {
            if (direction == -1)
            {
                direction = 1;

            }
            else
            {
                direction = -1;
            }
            change = false;
            changing = true;
        }
        if (changing)
        {
            //interpolate in the direction, 1 to sphere, -1 to cube
            float radius = size * density / 2;
            Vector3[] newVertices = new Vector3[(size + 1) * (size + 1) * 6];
            //triangles can stay the same
            frameNumber += direction;
            for (int i = 0; i < vertices.Length; i++){
                float magnitude = vertices[i].magnitude;
                float difference = magnitude - radius;
                float step = difference / (interpolationFrames + 1);
                newVertices[i] = vertices[i] * (magnitude - step * frameNumber) / magnitude;
            }

            Vector3[] newNormals = new Vector3[(size + 1) * (size + 1) * 6];

            for (int i = 0; i < normals.Length; i++)
            {
                Vector3 targetDirection = vertices[i].normalized;
                Vector3 cross = Vector3.Cross(normals[i], targetDirection);
                float step = Vector3.Angle(normals[i], targetDirection) / (interpolationFrames + 1);
                Quaternion q = Quaternion.AngleAxis(step*frameNumber, cross.normalized);
                newNormals[i] = q*normals[i];
            }

            Mesh mesh = GetComponent<MeshFilter>().mesh;
            mesh.vertices = newVertices;
            mesh.normals = newNormals;
            GetComponent<MeshFilter>().mesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;

            if (direction == 1 && frameNumber == interpolationFrames || direction == -1 && frameNumber == 0)
            {
                changing = false;
            }
        }
    }
}
