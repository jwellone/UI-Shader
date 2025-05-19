using UnityEngine;
using UnityEditor;
using jwellone;

#nullable enable

namespace jwelloneEditor
{
    public sealed class RadialBlurPreviewGUI : UIShaderPreviewGUI
    {
        bool _useDither;
        int _sampleCount = 8;
        float _intensity = 0.2f;
        float _centerX = 0.5f;
        float _centerY = 0.5f;

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/RadialBlur");
        }

        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);

            _sampleCount = EditorGUILayout.IntSlider("Sample Count", _sampleCount, 1, 32);
            _intensity = EditorGUILayout.Slider("Intensity", _intensity, 0f, 1f);
            _centerX = EditorGUILayout.Slider("Center X", _centerX, 0f, 1f);
            _centerY = EditorGUILayout.Slider("Center Y", _centerY, 0f, 1f);
            _useDither = EditorGUILayout.Toggle("Use Dither", _useDither);
        }

        protected override void UpdateMaterialProperty()
        {
            _material.SetInt(UIShaderProperty.sampleCount, _sampleCount);
            _material.SetFloat(UIShaderProperty.intensity, _intensity);
            _material.SetFloat(UIShaderProperty.centerX, _centerX);
            _material.SetFloat(UIShaderProperty.centerY, _centerY);

            if (_useDither)
            {
                _material.EnableKeyword(UIShaderProperty.radialBlurUseDitherKeyword);
            }
            else
            {
                _material.DisableKeyword(UIShaderProperty.radialBlurUseDitherKeyword);
            }
        }
    }
}
