using System;
using Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Tools = Editor.Tools;

[CustomEditor(typeof(ShapeMaker))]
public class ShapeMakerEditor : UnityEditor.Editor
{
    private ShapeMaker _shapeMaker;

    private enum ColliderType
    {
        Polygon, 
        Edge
    }

    private ColliderType _colliderType;
    
    private void OnEnable()
    {
        _shapeMaker = (ShapeMaker) target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        
        RenderEditor();

        if (!EditorGUI.EndChangeCheck()) return;
        _shapeMaker.MakeShape();
    }

    private void RenderEditor()
    {
        base.OnInspectorGUI();
        
        EditorGUILayout.HelpBox($"Tris count: {_shapeMaker.GetTrisCount()}", MessageType.None);

        GUIButton.Button(() =>
        {
            Tools.SaveMesh(_shapeMaker.GetComponent<MeshFilter>().sharedMesh, "new mesh", true);
        }, "Export mesh");
        
        GUIButton.Button(() =>
        {
            switch (_colliderType)
            {
                case ColliderType.Edge:
                    _shapeMaker.GenerateEdgeCollider();
                    break;
                    
                case ColliderType.Polygon:
                    _shapeMaker.GeneratePolygonCollider();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }, "Generate Collisions");
        
        _colliderType = (ColliderType) EditorGUILayout.EnumPopup("Collider type", _colliderType);
        serializedObject.ApplyModifiedProperties();

    }
    

}