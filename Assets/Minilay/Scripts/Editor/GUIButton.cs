using System;
using UnityEngine;

namespace Editor
{
    public static class GUIButton
    {
        public static void Button(Action action, string name = "Button")
        {
            if (GUILayout.Button(name))
            {
                action?.Invoke();
            }
        }
    }
}