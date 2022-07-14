using System;
using Codice.Client.Common;
using Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShapeMaker))]
public class ShapeMakerEditor : UnityEditor.Editor
{
    private enum ShapeType
    {
        Basic, Starred
    }
    [SerializeField] private ShapeType _shapeType;
    
    private ShapeMaker _shapeMaker;
    private void OnEnable()
    {
        _shapeMaker = (ShapeMaker) target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        
        RenderEditor();
        
        if (EditorGUI.EndChangeCheck())
            _shapeMaker.MakeShape();
        
    }

    private void RenderEditor()
    {
        _shapeType = (ShapeType) EditorGUILayout.EnumPopup("Shape type", _shapeType);
        EditorGUILayout.Space();
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
        GUIButton.Button(() =>
        {
            _shapeMaker.SaveMeshInAssets();
        }, "Export mesh");
        
        GUIButton.Button(() =>
        {
        }, "Generate Collider");

    }

}