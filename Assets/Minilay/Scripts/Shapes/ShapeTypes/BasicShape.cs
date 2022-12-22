using UnityEngine;

namespace Client.Scripts.Shapes
{
    public class BasicShape : Shape
    {
        public BasicShape(ShapeProperties shapeProperties) : base(shapeProperties)
        {
        }
        public override Mesh CreateMesh()
        {
            var verts = new Vector3[_vertexCount];
            var uv = new Vector2[_vertexCount]; 
            var tris = new int[3 * (_vertexCount - 2)];
            var normals = new Vector3[_vertexCount];

            for (var i = 0; i < _vertexCount; i++)
            {
                var angleStep = 360.0f / _vertexCount;

                var vertCoordinate = new Vector3(
                    Mathf.Cos((i * angleStep + _phase) * Mathf.Deg2Rad),
                    Mathf.Sin((i * angleStep + _phase) * Mathf.Deg2Rad)
                );

                verts[i] = vertCoordinate * _radius;
                uv[i] = vertCoordinate;
                normals[i] = Vector3.back;
            }

            for (var i = 0; i < _vertexCount - 2; i++)
            {
                tris[i * 3] = i + 1;
                tris[i * 3 + 1] = 0;
                tris[i * 3 + 2] = i + 2;
            }

            return new Mesh {vertices = verts, triangles = tris, normals = normals, uv = uv};
        }

    }
}