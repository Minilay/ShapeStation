using UnityEngine;

namespace Client.Scripts.Shapes
{
    public class StarredHoledShape : Shape
    {
        private readonly float _thickness;
        private readonly float _spikeValue;
        
        private readonly int _localVertexCount;
        public StarredHoledShape(ShapeProperties shapeProperties, float thickness, float spikeValue) : base(shapeProperties)
        {
            _thickness = thickness;
            _spikeValue = spikeValue;
            _localVertexCount = shapeProperties.vertexCount / 2 + shapeProperties.vertexCount % 2;
        }

        public override Mesh CreateMesh()
        {
            var verts = new Vector3[4 *_localVertexCount];
            var uv = new Vector2[4 * _localVertexCount]; 
            var tris = new int[12 * _localVertexCount];
            var normals = new Vector3[4 * _localVertexCount];
            
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
                
                
                
                verts[_localVertexCount * 2 + i * 2] = outerVertCoordinate * _thickness;
                verts[_localVertexCount * 2 + i * 2 + 1] = innerVertCoordinate * _thickness;
                
                uv[_localVertexCount * 2 + i * 2] = outerVertCoordinate;
                uv[_localVertexCount * 2 + i * 2 + 1] = innerVertCoordinate;

                normals[_localVertexCount * 2 + i * 2] = Vector3.back;
                normals[_localVertexCount * 2 + i * 2 + 1] = Vector3.back;

            }
            for (var i = 0; i < _localVertexCount; i++)
            {
                tris[i * 6] = 2 * i;
                tris[i * 6 + 1] = 2 * i + 1;
                tris[i * 6 + 2] = 2 * i + 2 * _localVertexCount;
                
                tris[i * 6 + 3] = 2 * i;
                tris[i * 6 + 4] = 2 * i + 2 * _localVertexCount;
                tris[i * 6 + 5] = (2 * i - 1 + 2 * _localVertexCount) % (2 * _localVertexCount);
            }
            
            for (var i = 0; i < _localVertexCount; i++)
            {
                var offset = _localVertexCount * 6;
                
                tris[offset + i * 6] = 2 * i + 1;
                tris[offset + i * 6 + 1] = 2 * i + 1 + 2 * _localVertexCount;
                tris[offset + i * 6 + 2] = 2 * i + 1 + 2 * _localVertexCount - 1;
                
                tris[offset + i * 6 + 3] = (2 * i - 1 + 2 * _localVertexCount) % (2 * _localVertexCount);
                tris[offset + i * 6 + 4] = 2 * i + 2 * _localVertexCount;
                tris[offset + i * 6 + 5] = (2 * i - 1 + 2 * _localVertexCount) % (2 * _localVertexCount) + 2 * _localVertexCount;
            }
            return new Mesh {vertices = verts, triangles = tris, normals = normals, uv = uv};
        }
    }
}