using UnityEngine;
using UnityEditor;
using jwellone;

#nullable enable

namespace jwelloneEditor
{
    public class GrayscalePreviewGUI : UIShaderPreviewGUI
    {
        bool _isUseNTSC;
        float _rate = 0;

        protected override Shader? GetShader()
        {
            return Shader.Find("UI/Grayscale");
        }

        public override void OnGUI(EditorWindow parent)
        {
            base.OnGUI(parent);

            _isUseNTSC = EditorGUILayout.Toggle("Use NTSC", _isUseNTSC);
            _rate = EditorGUILayout.Slider("Rate", _rate, 0f, 1f);
        }

        protected override void UpdateMaterialProperty()
        {
            _material.SetFloat(UIShaderProperty.rate, _rate);

            if (_isUseNTSC)
            {
                _material.EnableKeyword(UIShaderProperty.grayscaleNtscKeyword);
            }
            else
            {
                _material.DisableKeyword(UIShaderProperty.grayscaleNtscKeyword);
            }
        }
    }
}