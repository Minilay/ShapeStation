using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class Tools
    {
        public static void SaveMesh(Mesh mesh, string name, bool optimizeMesh)
        {
            var path = EditorUtility.SaveFilePanel("Save Mesh Asset", "Assets/", name, "asset");
            if (string.IsNullOrEmpty(path)) return;
            path = FileUtil.GetProjectRelativePath(path);

            if (optimizeMesh)
                MeshUtility.Optimize(mesh);

            AssetDatabase.CreateAsset(mesh, path);
            AssetDatabase.SaveAssets();
        }
    }
}