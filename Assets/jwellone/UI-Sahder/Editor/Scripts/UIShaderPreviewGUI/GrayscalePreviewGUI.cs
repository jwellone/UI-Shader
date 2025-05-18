using UnityEngine;
using UnityEditor;

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
            _material.SetFloat("_Rate", _rate);

            if (_isUseNTSC)
            {
                _material.EnableKeyword("GRAYSCALE_NTSC");
            }
            else
            {
                _material.DisableKeyword("GRAYSCALE_NTSC");
            }
        }
    }
}