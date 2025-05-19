using UnityEngine;
using UnityEditor;
using jwellone;

#nullable enable

namespace jwelloneEditor
{
    public sealed class FillPreviewGUI : UIShaderPreviewGUI
    {
        float _rate;
        Color _color = Color.white;

        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);

            _rate = EditorGUILayout.Slider("Rate", _rate, 0f, 1f);
            _color = EditorGUILayout.ColorField("Color", _color);
        }

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Fill");
        }

        protected override void UpdateMaterialProperty()
        {
            _material!.SetColor(UIShaderProperty.color, _color);
            _material!.SetFloat(UIShaderProperty.rate, _rate);
        }
    }
}
