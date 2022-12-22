using UnityEngine;

public abstract class Shape
{
    protected readonly int _vertexCount;
    protected readonly float _radius;
    protected readonly float _phase;
    protected readonly float _holeSize; 
    protected readonly float _spikeValue;
    protected Shape(ShapeProperties shapeProperties)
    {
        _vertexCount = shapeProperties.vertexCount;
        _radius = shapeProperties.radius;
        _phase = shapeProperties.phase;
        _holeSize = shapeProperties.holeSize;
        _spikeValue = shapeProperties.spikeValue;
    }

    public abstract Mesh CreateMesh();
}