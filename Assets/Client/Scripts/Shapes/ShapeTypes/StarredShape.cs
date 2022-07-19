using UnityEngine;

namespace Client.Scripts.Shapes
{
    public class StarredShape: Shape
    {
        private readonly float _spikeValue;
        private readonly int _localVertexCount;
        public StarredShape(ShapeProperties shapeProperties, float spikeValue) : base(shapeProperties)
        {
            _spikeValue = spikeValue;
            _localVertexCount = shapeProperties.vertexCount / 2 + shapeProperties.vertexCount % 2;
        }

        public override Mesh CreateMesh()
        {
            var verts = new Vector3[2 *_localVertexCount];
            var uv = new Vector2[2 * _localVertexCount]; 
            var tris = new int[6 * (_localVertexCount - 1)];
            var normals = new Vector3[2 * _localVertexCount];
            
            for (var i = 0; i < _localVertexCount; i++)
            {
                var angleStep = 360.0f / _localVertexCount;

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
            for (var i = 0; i < _localVertexCount; i++)
            {
                tris[i * 3] = (2 * i - 1 + 2 * _localVertexCount) % (2 * _localVertexCount);
                tris[i * 3 + 1] = 2 * i + 1;
                tris[i * 3 + 2] = 2 * i;
            }

            for (var i = 0; i < _localVertexCount - 2; i++)
            {
                var offset = _localVertexCount * 3;
                tris[offset + i * 3] = 2 * i + 1 + 2;
                tris[offset + i * 3 + 1] = 1 + 0;
                tris[offset + i * 3 + 2] = 2 * i + 1 + 4;
            }
            return new Mesh {vertices = verts, triangles = tris, normals = normals, uv = uv};
        }
    }
}