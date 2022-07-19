using UnityEditor;
using UnityEngine;

public class ShapeMakerEditorWindow : EditorWindow
{
    [MenuItem("GameObject/2D Object/ShapeStation", false, 22)]
    public static void InstantiateShape(MenuCommand menuCommand)
    {
        var gameObject = new GameObject("ShapeMaker");
        gameObject.AddComponent<ShapeMaker>();
         
        GameObjectUtility.SetParentAndAlign(gameObject, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
        Selection.activeObject = gameObject;
    }
}