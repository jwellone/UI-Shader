using UnityEngine;
using UnityEditor;

#nullable enable

namespace jwelloneEditor
{
        public sealed class VignettePreviewGUI : UIShaderPreviewGUI
    {
        Color _vignetteColor = new Color(0, 0, 0, 1);
        float _radius = 0.4f;
        float _softness = 0.1f;
        float _offsetX = 0.5f;
        float _offsetY = 0.5f;


        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);

            _vignetteColor = EditorGUILayout.ColorField("Vignette Color", _vignetteColor);
            _radius = EditorGUILayout.Slider("Radius", _radius, 0f, 1f);
            _softness = EditorGUILayout.Slider("Softness", _softness, 0f, 1f);
            _offsetX = EditorGUILayout.Slider("Offset X", _offsetX, 0f, 1f);
            _offsetY = EditorGUILayout.Slider("Offset Y", _offsetY, 0f, 1f);
        }

        protected override void UpdateMaterialProperty()
        {
            _material.SetColor("_VignetteColor", _vignetteColor);
            _material.SetFloat("_Radius", _radius);
            _material.SetFloat("_Softness", _softness);
            _material.SetFloat("_OffsetX", _offsetX);
            _material.SetFloat("_OffsetY", _offsetY);
        }

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Vignette");
        }
    }
}
