using UnityEngine;
using UnityEditor;

#nullable enable

namespace jwelloneEditor
{
    public sealed class OutlinePreviewGUI : UIShaderPreviewGUI
    {
        int _size;
        Color _color = Color.black;

        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);

            _size = EditorGUILayout.IntSlider("Size", _size, 0, 16);
            _color = EditorGUILayout.ColorField("Color", _color);
        }

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Outline");
        }

        protected override void UpdateMaterialProperty()
        {
            _material.SetInt("_OutlineSize", _size);
            _material.SetColor("_OutlineColor", _color);
        }
    }
}
