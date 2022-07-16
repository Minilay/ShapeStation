using System;
using UnityEngine;

namespace Client.Scripts.Shapes
{
    [Serializable]
    public struct ShapeProperties
    {
        [SerializeField] public int vertexCount;
        [SerializeField] public float radius;
        [SerializeField] public float phase;
        
        public void Validation()
        {
            radius = Mathf.Max(0, radius);
            vertexCount = Mathf.Max(3, vertexCount);
        }
    }
    
}