using UnityEngine;

namespace Client.Scripts.Shapes
{
    public class StarredShape: Shape
    {
        private readonly float _spikeValue;
        public StarredShape(int vertexCount, float radius, float phase, float spikeValue) : base(vertexCount, radius, phase)
        {
            _spikeValue = spikeValue;
        }

        public override Mesh CreateMesh()
        {
            var verts = new Vector3[2 *_vertexCount];
            var uv = new Vector2[2 * _vertexCount]; 
            var tris = new int[6 * (_vertexCount - 1)];
            var normals = new Vector3[2 * _vertexCount];
            
            for (var i = 0; i < _vertexCount; i++)
            {
                var angleStep = 360.0f / _vertexCount;

                var angle = i * angleStep + _phase;
                var outerVertCoordinate = Utils.PolarToCartesian(angle) * (_radius * (1 +_spikeValue));
                var innerVertCoordinate = Utils.PolarToCartesian(angle + angleStep / 2) * (_radius * (1 - _spikeValue));

                verts[i * 2] = outerVertCoordinate;
                verts[i * 2 + 1] = innerVertCoordinate;
                
                uv[i * 2] = outerVertCoordinate;
                uv[i * 2 + 1] = innerVertCoordinate;

                normals[i * 2] = Vector3.back;
                normals[i * 2 + 1] = Vector3.back;
            }
            for (var i = 0; i < _vertexCount; i++)
            {
                tris[i * 3] = 2 * i;
                tris[i * 3 + 1] = 2 * i + 1;
                tris[i * 3 + 2] = (2 * i - 1 + 2 * _vertexCount) % (2 * _vertexCount);
            }

            for (var i = 0; i < _vertexCount - 2; i++)
            {
                var offset = _vertexCount * 3;
                tris[offset + i * 3] = 2 * i + 1 + 4;
                tris[offset + i * 3 + 1] = 1 + 0;
                tris[offset + i * 3 + 2] = 2 * i + 1 + 2;
            }
            return new Mesh {vertices = verts, triangles = tris, normals = normals, uv = uv};
        }
    }
}