using UnityEditor;
using UnityEngine;
namespace Client.Scripts
{
    public static class Utils
    {
        public static void SaveMesh (Mesh mesh, string name, bool optimizeMesh) {
            var path = EditorUtility.SaveFilePanel("Save Mesh Asset", "Assets/", name, "asset");
            if (string.IsNullOrEmpty(path)) return;
            path = FileUtil.GetProjectRelativePath(path);
            
            if (optimizeMesh)
                MeshUtility.Optimize(mesh);
        
            AssetDatabase.CreateAsset(mesh, path);
            AssetDatabase.SaveAssets();
        }
        
        public static Vector3 PolarToCartesian(float angle, float radius = 1) =>
            new (
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );

    }
}