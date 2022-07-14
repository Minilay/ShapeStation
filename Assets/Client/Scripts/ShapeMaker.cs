using Client.Scripts;
using Client.Scripts.Shapes;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteAlways]
public class ShapeMaker : MonoBehaviour
{
    [SerializeField] private int _vertexCount;
    [SerializeField] private float _radius;
    [SerializeField] private float _phase;
    [SerializeField] private bool _hasHole;
    [Range(0, 1)]
    [SerializeField] private float _thickness;
    [SerializeField] private bool _segmented;
    
    
    public void OnValidate()
    {
        _radius = Mathf.Max(0, _radius);
        _vertexCount = Mathf.Max(3, _vertexCount);
        _thickness = Mathf.Clamp(_thickness, 0, _radius);
    }
    private MeshFilter _meshFilter;

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
        Shape shape = _hasHole ? 
            new BasicHoledShape(_vertexCount, _radius, _phase, _thickness) : 
            new BasicShape(_vertexCount, _radius, _phase);
        
        _meshFilter.mesh = shape.CreateMesh();
    }

    public void SaveMeshInAssets()
    {
        var mesh = _meshFilter.sharedMesh;
        Utils.SaveMesh(mesh, mesh.name, true);
    }
    
    
}