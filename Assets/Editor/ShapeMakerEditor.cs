using Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShapeMaker))]
public class ShapeMakerEditor : UnityEditor.Editor
{
    
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
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
        
        EditorGUILayout.HelpBox($"Tris count: {_shapeMaker.GetTrisCount()}", MessageType.None);

        GUIButton.Button(() =>
        {
            _shapeMaker.SaveMeshInAssets();
        }, "Export mesh");
        
        GUIButton.Button(() =>
        {
            _shapeMaker.GenerateCollider();
        }, "Generate Collider");

    }

}