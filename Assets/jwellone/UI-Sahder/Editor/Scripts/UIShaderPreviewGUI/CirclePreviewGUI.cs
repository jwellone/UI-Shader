using UnityEngine;
using UnityEditor;
using jwellone;

#nullable enable

namespace jwelloneEditor
{
    public sealed class CirclePreviewGUI : UIShaderPreviewGUI
    {
        float _radius = 0.4f;
        float _softness = 0.1f;
        float _centerX = 0.5f;
        float _centerY = 0.5f;

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Circle");
        }

        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);

            _radius = EditorGUILayout.Slider("Radius", _radius, 0f, 1f);
            _softness = EditorGUILayout.Slider("Softness", _softness, 0f, 1f);
            _centerX = EditorGUILayout.Slider("Center X", _centerX, 0f, 1f);
            _centerY = EditorGUILayout.Slider("Center Y", _centerY, 0f, 1f);
        }

        protected override void UpdateMaterialProperty()
        {
            _material.SetFloat(UIShaderProperty.radius, _radius);
            _material.SetFloat(UIShaderProperty.softness, _softness);
            _material.SetFloat(UIShaderProperty.centerX, _centerX);
            _material.SetFloat(UIShaderProperty.centerY, _centerY);
        }
    }
}