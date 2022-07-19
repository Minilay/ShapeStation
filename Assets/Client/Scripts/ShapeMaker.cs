using System.Collections.Generic;
using Client.Scripts.Shapes;
using UnityEngine;
using UnityEngine.Diagnostics;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteAlways]
public class ShapeMaker : MonoBehaviour
{
    [SerializeField] private ShapeProperties _shapeProperties;
    [Range(0, 1)] [SerializeField] private float _holeSize;
    [Range(0, 1)] [SerializeField] private float _spikeValue;

    private MeshFilter _meshFilter;
    private Material _defaultMaterial;
    private PolygonCollider2D _polygonCollider;
    private EdgeCollider2D _edgeCollider;
    private void OnValidate()
    {
        _shapeProperties.Validation();

        _shapeProperties.vertexCount += _spikeValue == 0 ? 0 : _shapeProperties.vertexCount % 2;
    }

    private void Reset()
    {
        _shapeProperties.vertexCount = 3;
        _shapeProperties.radius = 1;

        _defaultMaterial = new Material(Shader.Find("Sprites/Default"));
        GetComponent<MeshRenderer>().sharedMaterial = _defaultMaterial;
        MakeShape();
    }

    private void OnEnable()
    {
        _meshFilter = GetComponent<MeshFilter>();
    }

    public void MakeShape()
    {
        Shape shape = _holeSize == 0 ? _spikeValue == 0 ? new BasicShape(_shapeProperties) :
            new StarredShape(_shapeProperties, _spikeValue) :
            _spikeValue == 0 ? new BasicHoledShape(_shapeProperties, _holeSize) :
            new StarredHoledShape(_shapeProperties, _holeSize, _spikeValue);

        _meshFilter.mesh = shape.CreateMesh();
    }

    public void GeneratePolygonCollider()
    {
        _polygonCollider = TryGetComponent(out PolygonCollider2D polygonCollider2D)
            ? polygonCollider2D
            : gameObject.AddComponent<PolygonCollider2D>();

        _polygonCollider.SetPath(0, GenerateColliderPath());
    }

    public void GenerateEdgeCollider()
    {
        _edgeCollider = TryGetComponent(out EdgeCollider2D edgeCollider2D)
            ? edgeCollider2D
            : gameObject.AddComponent<EdgeCollider2D>();
        
        _edgeCollider.SetPoints(GenerateColliderPath());
    }

    public void DeleteColliders()
    {
        DestroyImmediate(_edgeCollider);
        DestroyImmediate(_polygonCollider);
        
    }

    //TODO: Separate this method.
    private List<Vector2> GenerateColliderPath()
    {
        var outsidePoints = new List<Vector2>();
        var insidePoints = new List<Vector2>();

        var vertexCount = _spikeValue == 0
            ? _shapeProperties.vertexCount
            : _shapeProperties.vertexCount / 2 + _shapeProperties.vertexCount % 2;

        for (var i = 0; i < vertexCount; i++)
        {
            var angleStep = 360.0f / vertexCount;

            var angle = i * angleStep + _shapeProperties.phase;
            var outerVertCoordinate = Utils.PolarToCartesian(angle) * (_shapeProperties.radius * (1 + _spikeValue));
            var innerVertCoordinate = Utils.PolarToCartesian(angle + angleStep / 2) *
                                      (_shapeProperties.radius * (1 - _spikeValue));

            outsidePoints.Add(outerVertCoordinate);
            insidePoints.Add(outerVertCoordinate * _holeSize);

            if (_spikeValue > 0)
            {
                outsidePoints.Add(innerVertCoordinate);
                insidePoints.Add(innerVertCoordinate * _holeSize);
            }
        }

        outsidePoints.Add(outsidePoints[0]);
        insidePoints.Add(insidePoints[0]);

        if (_holeSize > 0)
            outsidePoints.AddRange(insidePoints);

        return outsidePoints;
    }

    public int GetTrisCount() => _meshFilter.sharedMesh?.triangles.Length / 3 ?? 0;
}