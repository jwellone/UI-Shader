using UnityEngine;
using UnityEditor;
using jwellone;

#nullable enable

namespace jwelloneEditor
{
    public sealed class HSVPreviewGUI : UIShaderPreviewGUI
    {
        float _hue = 1f;
        float _sat = 1f;
        float _val = 1f;

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/HSV");
        }

        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);
            _hue = EditorGUILayout.Slider("Hue", _hue, 0f, 1f);
            _sat = EditorGUILayout.Slider("Saturation", _sat, 0f, 1f);
            _val = EditorGUILayout.Slider("Value", _val, 0f, 1f);
        }

        protected override void UpdateMaterialProperty()
        {
            _material.SetFloat(UIShaderProperty.hue, _hue);
            _material.SetFloat(UIShaderProperty.sat, _sat);
            _material.SetFloat(UIShaderProperty.val, _val);
        }
    }
}
