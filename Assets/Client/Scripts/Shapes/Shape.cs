using UnityEngine;

namespace Client.Scripts.Shapes
{
    public abstract class Shape
    {
        protected readonly int _vertexCount;
        protected readonly float _radius;
        protected readonly float _phase;

        protected Shape(int vertexCount, float radius, float phase)
        {
            _vertexCount = vertexCount;
            _radius = radius;
            _phase = phase;
        }

        public abstract Mesh CreateMesh();
    }
}