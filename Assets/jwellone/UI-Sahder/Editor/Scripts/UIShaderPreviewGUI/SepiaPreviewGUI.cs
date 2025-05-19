using UnityEngine;
using UnityEditor;
using jwellone;

#nullable enable

namespace jwelloneEditor
{
    public sealed class SepiaPreviewGUI : UIShaderPreviewGUI
    {
        float _rate = 0;

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Sepia");
        }

        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);
            _rate = EditorGUILayout.Slider("Rate", _rate, 0f, 1f);
        }

        protected override void UpdateMaterialProperty()
        {
            _material.SetFloat(UIShaderProperty.rate, _rate);
        }
    }
}