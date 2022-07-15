using System;
using Client.Scripts;
using Client.Scripts.Shapes;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteAlways]
public class ShapeMaker : MonoBehaviour
{
    [SerializeField] private int _vertexCount;
    [SerializeField] private float _radius;
    [SerializeField] private float _phase;
    [Range(0, 1)] [SerializeField] private float _thickness;
    [Range(0, 1)] [SerializeField] private float _spikeValue;

    private MeshFilter _meshFilter;

    private void OnValidate()
    {
        _radius = Mathf.Max(0, _radius);
        _vertexCount = Mathf.Max(3, _vertexCount);
    }

    private void Reset()
    {
        _vertexCount = 3;
        _radius = 1;
    }

    private void OnEnable()
    {
        _meshFilter = GetComponent<MeshFilter>();
    }

    public void MakeShape()
    {
        Shape shape = _thickness == 0
            ? new BasicShape(_vertexCount, _radius, _phase)
            : new BasicHoledShape(_vertexCount, _radius, _phase, _thickness);

        shape = _spikeValue == 0 ? shape : new StarredShape(_vertexCount, _radius, _phase, _spikeValue);
        
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