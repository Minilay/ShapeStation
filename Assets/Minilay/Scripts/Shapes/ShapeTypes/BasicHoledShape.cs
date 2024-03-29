﻿using UnityEngine;
using UnityEngine.Diagnostics;
public class BasicHoledShape : Shape
{

    public BasicHoledShape(ShapeProperties shapeProperties) : base(shapeProperties)
    {
    }

    public override Mesh CreateMesh()
    {
        var verts = new Vector3[_vertexCount * 2];
        var uv = new Vector2[_vertexCount * 2];
        var tris = new int[_vertexCount * 6];
        var normals = new Vector3[_vertexCount * 2];


        for (var i = 0; i < _vertexCount; i++)
        {
            var angleStep = 360.0f / _vertexCount;
            var angle = i * angleStep + _phase;
            var coordinates = Utils.PolarToCartesian(angle);

            verts[_vertexCount - i - 1] = coordinates * _radius;
            uv[_vertexCount - i - 1] = coordinates;
            normals[i] = Vector3.back;

            verts[_vertexCount * 2 - i - 1] = coordinates * (_radius * _holeSize);
            uv[_vertexCount * 2 - i - 1] = coordinates;
            normals[_vertexCount + i] = Vector3.back;
        }

        for (var i = 0; i < _vertexCount; i++)
        {
            tris[i * 3] = i;
            tris[i * 3 + 1] = (i + 1) % _vertexCount;
            tris[i * 3 + 2] = i + _vertexCount;

            tris[_vertexCount * 3 + i * 3] = i;
            tris[_vertexCount * 3 + i * 3 + 1] = i + _vertexCount;
            tris[_vertexCount * 3 + i * 3 + 2] = (i + _vertexCount - 1) % _vertexCount + _vertexCount;
        }

        return new Mesh {vertices = verts, triangles = tris, normals = normals, uv = uv};
    }
}