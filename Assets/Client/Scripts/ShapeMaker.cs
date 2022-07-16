using System;
using Client.Scripts;
using Client.Scripts.Shapes;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteAlways]
public class ShapeMaker : MonoBehaviour
{
    [SerializeField] private ShapeProperties _shapeProperties;
    [Range(0, 1)] [SerializeField] private float holeSize;
    [Range(0, 1)] [SerializeField] private float _spikeValue;

    private MeshFilter _meshFilter;

    private void OnValidate()
    {
        _shapeProperties.Validation();
        
        _shapeProperties.vertexCount += _spikeValue == 0 ? 0 : _shapeProperties.vertexCount % 2;

    }

    private void Reset()
    {
        _shapeProperties.vertexCount = 3;
        _shapeProperties.radius = 1;
    }

    private void OnEnable()
    {
        _meshFilter = GetComponent<MeshFilter>();
    }

    public void MakeShape()
    {
        Shape shape = holeSize == 0 ? 
            _spikeValue == 0 ? 
                new BasicShape(_shapeProperties):
                new StarredShape(_shapeProperties, _spikeValue) :
            _spikeValue == 0 ? 
                new BasicHoledShape(_shapeProperties, holeSize) : 
                new StarredHoledShape(_shapeProperties, holeSize, _spikeValue);

        _meshFilter.mesh = shape.CreateMesh();
    }

    public void SaveMeshInAssets()
    {
        var mesh = _meshFilter.sharedMesh;
        Utils.SaveMesh(mesh, mesh.name, true);
    }

    public void GenerateCollider()
    {
        Debug.Log("Not implemented yet");
    }

    public int GetTrisCount() => _meshFilter.sharedMesh?.triangles.Length / 3 ?? 0;
}