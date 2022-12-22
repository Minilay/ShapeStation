using Client.Scripts.Shapes;
using Minilay.ShapeStation.Scripts;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteAlways]
public class ShapeMaker : MonoBehaviour
{
    [SerializeField] private ShapeProperties _shapeProperties;

    [SerializeField] private Color _color = Color.white;


    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private PolygonCollider2D _polygonCollider;
    private EdgeCollider2D _edgeCollider;

    private void OnValidate()
    {
        _shapeProperties.Validation();

    }

    private void Reset()
    {
        _shapeProperties.vertexCount = 3;
        _shapeProperties.radius = 1;
        
        MakeShape();
    }

    private void OnEnable()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshRenderer.sharedMaterial == null)
            _meshRenderer.material = new Material(Shader.Find("Sprites/Default"));

        _meshFilter = GetComponent<MeshFilter>();
    }

    public void MakeShape()
    {
        SetMesh();
        SetColor();
    }

    private void SetMesh()
    {
        Shape shape = _shapeProperties.holeSize == 0 ? 
            _shapeProperties.spikeValue == 0 ?
                new BasicShape(_shapeProperties) :
                new StarredShape(_shapeProperties) :
            _shapeProperties.spikeValue == 0 ? 
                new BasicHoledShape(_shapeProperties) :
                new StarredHoledShape(_shapeProperties);

        _meshFilter.mesh = shape.CreateMesh();
    }
    private void SetColor() => _meshRenderer.sharedMaterial.color = _color;
    
    
    public void GeneratePolygonCollider()
    {
        _polygonCollider = TryGetComponent(out PolygonCollider2D polygonCollider2D)
            ? polygonCollider2D
            : gameObject.AddComponent<PolygonCollider2D>();

        _polygonCollider.SetPath(0, ColliderPathGenerator.GeneratePath(_shapeProperties));
    }

    public void GenerateEdgeCollider()
    {
        _edgeCollider = TryGetComponent(out EdgeCollider2D edgeCollider2D)
            ? edgeCollider2D
            : gameObject.AddComponent<EdgeCollider2D>();

        _edgeCollider.SetPoints(ColliderPathGenerator.GeneratePath(_shapeProperties));
    }

    public void DeleteColliders()
    {
        DestroyImmediate(_edgeCollider);
        DestroyImmediate(_polygonCollider);
    }

    public int GetTrisCount() => _meshFilter.sharedMesh?.triangles.Length / 3 ?? 0;

    [ContextMenu("Set Random Properties")]
    public void SetRandomProperties()
    {
        _shapeProperties = ShapeProperties.RandomProperties();
        _color = Random.ColorHSV();
        MakeShape();
    }
}