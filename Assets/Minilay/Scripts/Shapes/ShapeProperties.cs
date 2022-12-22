using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct ShapeProperties
{
    [SerializeField] public int vertexCount;
    [SerializeField] public float radius;
    [SerializeField] public float phase;
    [Range(0, 1)] [SerializeField] public float holeSize;
    [Range(0, 1)] [SerializeField] public float spikeValue;


    public void Validation()
    {
        radius = Mathf.Max(0, radius);
        vertexCount = Mathf.Max(3, vertexCount);
        vertexCount += spikeValue == 0 ? 0 : vertexCount % 2;
    }

    /// <summary>
    /// Creates shape properties with random values.
    /// Ranges:
    /// VertexCount: 3 - 20,
    /// phase: 0 - 120,
    /// radius: 0.5 - 5,
    /// hole size: 0 - 0.99,
    /// spike value: 0 - 0.99
    /// </summary>
    /// <returns></returns>
    public static ShapeProperties RandomProperties()
    {
        return new()
        {
            vertexCount = Random.Range(3, 20),
            phase = Random.Range(0, 120), 
            radius = Random.Range(0.5f, 5), 
            holeSize = Random.Range(0, 0.99f), 
            spikeValue = Random.Range(0, 0.99f)
        };
    }
}