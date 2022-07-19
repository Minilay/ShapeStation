using UnityEngine;

public abstract class Shape
{
    protected readonly int _vertexCount;
    protected readonly float _radius;
    protected readonly float _phase;

    protected Shape(ShapeProperties shapeProperties)
    {
        _vertexCount = shapeProperties.vertexCount;
        _radius = shapeProperties.radius;
        _phase = shapeProperties.phase;
    }

    public abstract Mesh CreateMesh();
}