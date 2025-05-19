using UnityEngine;
using UnityEditor;
using jwellone;

#nullable enable

namespace jwelloneEditor
{
    public sealed class DitheringPreviewGUI : UIShaderPreviewGUI
    {
        Color _color = Color.white;

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Dithering");
        }

        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);
            _color = EditorGUILayout.ColorField("Color", _color);
        }

        protected override void UpdateMaterialProperty()
        {
            _material!.SetColor(UIShaderProperty.color, _color);
        }
    }
}